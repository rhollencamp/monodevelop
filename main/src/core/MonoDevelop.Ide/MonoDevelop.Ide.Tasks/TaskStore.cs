// 
// TaskStore.cs
//  
// Author:
//       Lluis Sanchez Gual <lluis@novell.com>
// 
// Copyright (c) 2009 Novell, Inc (http://www.novell.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using MonoDevelop.Core;
using MonoDevelop.Projects;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Gui.Content;

namespace MonoDevelop.Ide.Tasks
{
	public class TaskStore: IEnumerable<Task>, ILocationList
	{
		int taskUpdateCount;
		List<Task> tasks = new List<Task> ();
		Dictionary<FilePath,Task[]> taskIndex = new Dictionary<FilePath, Task[]> ();
		
		public event TaskEventHandler TasksAdded;
		public event TaskEventHandler TasksRemoved;
		public event TaskEventHandler TasksChanged;
		
		List<Task> tasksAdded;
		List<Task> tasksRemoved;
		
		public TaskStore ()
		{
			IdeApp.Workspace.FileRenamedInProject += ProjectFileRenamed;
			IdeApp.Workspace.FileRemovedFromProject += ProjectFileRemoved;
			
			MonoDevelop.Projects.Text.TextFileService.CommitCountChanges += delegate (object sender, MonoDevelop.Projects.Text.TextFileEventArgs args) {
				foreach (Task task in GetFileTasks (args.TextFile.Name.FullPath))
					task.SavedLine = -1;
			};
			
			MonoDevelop.Projects.Text.TextFileService.ResetCountChanges += delegate (object sender, MonoDevelop.Projects.Text.TextFileEventArgs args) {
				Task[] ctasks = GetFileTasks (args.TextFile.Name.FullPath);
				foreach (Task task in ctasks) {
					if (task.SavedLine != -1) {
						task.Line = task.SavedLine;
						task.SavedLine = -1;
					}
				}
				NotifyTasksChanged (ctasks);
			};
			
			MonoDevelop.Projects.Text.TextFileService.LineCountChanged += delegate (object sender, MonoDevelop.Projects.Text.LineCountEventArgs args) {
				if (args.TextFile == null || args.TextFile.Name.IsNullOrEmpty)
					return;
				Task[] ctasks = GetFileTasks (args.TextFile.Name.FullPath);
				foreach (Task task in ctasks) {
					if (task.Line - 1 > args.LineNumber || (task.Line - 1 == args.LineNumber && task.Column - 1 >= args.Column)) {
						if (task.SavedLine == -1)
							task.SavedLine = task.Line;
						task.Line += args.LineCount;
					}
				}
				NotifyTasksChanged (ctasks);
			};
		}
		
		public void Add (Task task)
		{
			tasks.Add (task);
			OnTaskAdded (task);
		}
		
		public void AddRange (IEnumerable<Task> newTasks)
		{
			BeginTaskUpdates ();
			try {
				foreach (Task t in newTasks) {
					tasks.Add (t);
					OnTaskAdded (t);
				}
			} finally {
				EndTaskUpdates ();
			}
		}
		
		public void RemoveRange (IEnumerable<Task> tasks)
		{
			BeginTaskUpdates ();
			try {
				foreach (Task t in tasks) {
					if (this.tasks.Remove (t))
						OnTaskRemoved (t);
				}
			} finally {
				EndTaskUpdates ();
			}
		}
		
		public void RemoveItemTasks (IWorkspaceObject parent)
		{
			RemoveRange (new List<Task> (GetItemTasks (parent)));
		}
		
		public void RemoveItemTasks (IWorkspaceObject parent, bool checkHierarchy)
		{
			RemoveRange (new List<Task> (GetItemTasks (parent, checkHierarchy)));
		}
		
		public void RemoveFileTasks (FilePath file)
		{
			RemoveRange (new List<Task> (GetFileTasks (file)));
		}
		
		public void Remove (Task task)
		{
			if (tasks.Remove (task))
				OnTaskRemoved (task);
		}
		
		public void Clear ()
		{
			try {
				BeginTaskUpdates ();
				List<Task> toRemove = tasks;
				tasks = new List<Task> ();
				foreach (Task t in toRemove)
					OnTaskRemoved (t);
			} finally {
				EndTaskUpdates ();
			}
		}
		
		public void ClearByOwner (object owner)
		{
			try {
				BeginTaskUpdates ();
				List<Task> toRemove = new List<Task> (GetOwnerTasks (owner));
				foreach (Task t in toRemove)
					Remove (t);
			} finally {
				EndTaskUpdates ();
			}
		}
		
		public int Count {
			get { return tasks.Count; }
		}
		
		public IEnumerator<Task> GetEnumerator ()
		{
			return tasks.GetEnumerator ();
		}
		
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((IEnumerable)tasks).GetEnumerator ();
		}

		public IEnumerable<Task> GetOwnerTasks (object owner)
		{
			foreach (Task t in tasks) {
				if (t.Owner == owner)
					yield return t;
			}
		}

		public Task[] GetFileTasks (FilePath file)
		{
			Task[] ta;
			if (taskIndex.TryGetValue (file, out ta))
				return ta;
			else
				return new Task [0];
		}
		
		public IEnumerable<Task> GetItemTasks (IWorkspaceObject parent)
		{
			return GetItemTasks (parent, true);
		}
		
		public IEnumerable<Task> GetItemTasks (IWorkspaceObject parent, bool checkHierarchy)
		{
			foreach (Task t in tasks) {
				if (t.BelongsToItem (parent, checkHierarchy))
					yield return t;
			}
		}
		
		public void BeginTaskUpdates ()
		{
			if (taskUpdateCount++ != 0)
				return;
			tasksAdded = new List<Task> ();
			tasksRemoved = new List<Task> ();
		}
		
		public void EndTaskUpdates ()
		{
			if (--taskUpdateCount != 0)
				return;
			List<Task> oldAdded = tasksAdded;
			List<Task> oldRemoved = tasksRemoved;
			tasksAdded = null;
			tasksRemoved = null;
			if (oldRemoved.Count > 0)
				NotifyTasksRemoved (oldRemoved);
			if (oldAdded.Count > 0)
				NotifyTasksAdded (oldAdded);
		}
		
		void NotifyTasksAdded (IEnumerable<Task> ts)
		{
			try {
				if (TasksAdded != null)
					TasksAdded (null, new TaskEventArgs (ts));
			} catch (Exception ex) {
				LoggingService.LogError ("Error while notifying task changes", ex);
			}
		}
		
		void NotifyTasksChanged (IEnumerable<Task> ts)
		{
			try {
				if (TasksChanged != null)
					TasksChanged (null, new TaskEventArgs (ts));
			} catch (Exception ex) {
				LoggingService.LogError ("Error while notifying task changes", ex);
			}
		}
		
		void NotifyTasksRemoved (IEnumerable<Task> ts)
		{
			try {
				if (TasksRemoved != null)
					TasksRemoved (null, new TaskEventArgs (ts));
			} catch (Exception ex) {
				LoggingService.LogError ("Error while notifying task changes", ex);
			}
		}
		
		internal void OnTaskAdded (Task t)
		{
			if (t.FileName != FilePath.Null) {
				Task[] ta;
				if (taskIndex.TryGetValue (t.FileName, out ta)) {
					Array.Resize (ref ta, ta.Length + 1);
					ta [ta.Length - 1] = t;
				} else {
					ta = new Task [] { t };
				}
				taskIndex [t.FileName] = ta;
			}
			if (tasksAdded != null)
				tasksAdded.Add (t);
			else
				NotifyTasksAdded (new Task [] { t });
		}
		
		internal void OnTaskRemoved (Task t)
		{
			if (t.FileName != FilePath.Null) {
				Task[] ta;
				if (taskIndex.TryGetValue (t.FileName, out ta)) {
					if (ta.Length == 1) {
						if (ta [0] == t)
							taskIndex.Remove (t.FileName);
					} else {
						int i = Array.IndexOf (ta, t);
						if (i != -1) {
							Task[] newTa = new Task [ta.Length - 1];
							Array.Copy (ta, 0, newTa, 0, i);
							Array.Copy (ta, i+1, newTa, i, ta.Length - i - 1);
							taskIndex [t.FileName] = newTa;
						}
					}
				}
			}
			if (tasksRemoved != null)
				tasksRemoved.Add (t);
			else
				NotifyTasksRemoved (new Task [] { t });
		}
		
		void ProjectFileRemoved (object sender, ProjectFileEventArgs e)
		{
			BeginTaskUpdates ();
			try {
				foreach (Task curTask in new List<Task> (GetFileTasks (e.ProjectFile.FilePath))) {
					Remove (curTask);
				}
			} finally {
				EndTaskUpdates ();
			}
		}
		
		void ProjectFileRenamed (object sender, ProjectFileRenamedEventArgs e)
		{
			BeginTaskUpdates ();
			try {
				Task[] ctasks = GetFileTasks (e.OldName);
				foreach (Task curTask in ctasks)
					curTask.FileName = e.NewName;
				taskIndex.Remove (e.OldName);
				taskIndex [e.NewName] = ctasks;
				tasksAdded.AddRange (ctasks);
				tasksRemoved.AddRange (ctasks);
			} finally {
				EndTaskUpdates ();
			}
		}
		
		#region ILocationList implementation
		
		Task currentLocationTask;
		
		public event EventHandler CurrentLocationTaskChanged;
		
		public Task CurrentLocationTask {
			get { return currentLocationTask; }
			set { currentLocationTask = value; }
		}
		
		public NavigationPoint GetNextLocation ()
		{
			Task ct = null;
			if (currentLocationTask == null) {
				if (tasks.Count > 0)
					ct = tasks [0];
			}
			else {
				for (int n=0; n<tasks.Count; n++) {
					if (tasks [n] == currentLocationTask) {
						if (n < tasks.Count - 1)
							ct = tasks [n+1];
						break;
					}
				}
			}
			
			currentLocationTask = ct;
			if (CurrentLocationTaskChanged != null)
				CurrentLocationTaskChanged (this, EventArgs.Empty);
			
			if (currentLocationTask != null) {
				TaskService.ShowStatus (currentLocationTask);
				return new TextFileNavigationPoint (currentLocationTask.FileName, currentLocationTask.Line, currentLocationTask.Column);
			}
			else
				return null;
		}
		
		
		public NavigationPoint GetPreviousLocation ()
		{
			Task ct = null;
			if (currentLocationTask == null) {
				if (tasks.Count > 0)
					ct = tasks [tasks.Count - 1];
			}
			else {
				for (int n=0; n<tasks.Count; n++) {
					if (tasks [n] == currentLocationTask) {
						if (n > 0)
							ct = tasks [n-1];
						break;
					}
				}
			}
			
			currentLocationTask = ct;
			if (CurrentLocationTaskChanged != null)
				CurrentLocationTaskChanged (this, EventArgs.Empty);
			
			if (currentLocationTask != null) {
				TaskService.ShowStatus (currentLocationTask);
				return new TextFileNavigationPoint (currentLocationTask.FileName, currentLocationTask.Line, currentLocationTask.Column);
			}
			else
				return null;
		}
		
		public string ItemName {
			get; set;
		}
		
		#endregion
	}
		
	public delegate void TaskEventHandler (object sender, TaskEventArgs e);
	
	public class TaskEventArgs : EventArgs
	{
		IEnumerable<Task> tasks;
		
		public TaskEventArgs (Task task) : this (new Task[] { task })
		{
		}
		
		public TaskEventArgs (IEnumerable<Task> tasks)
		{
			this.tasks = tasks;
		}
		
		public IEnumerable<Task> Tasks
		{
			get { return tasks; }
		}
	}
}

// 
// IPadContainer.cs
//  
// Author:
//       Lluis Sanchez Gual <lluis@novell.com>
// 
// Copyright (c) 2010 Novell, Inc (http://www.novell.com)
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

using System;
using System.Drawing;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Codons;
using MonoDevelop.Core;
using MonoDevelop.Components.Docking;
using MonoDevelop.Components.Commands;
using Gtk;
using System.Collections.Generic;

namespace MonoDevelop.Ide.Gui
{
	public static class DockItemToolbarLoader
	{
		public static void Add (this DockItemToolbar toolbar, CommandEntrySet entrySet, Gtk.Widget commandTarget)
		{
			CommandDockBar dockBar = new CommandDockBar (toolbar, commandTarget);
			foreach (CommandEntry entry in entrySet) {
				dockBar.Add (entry);
			}
		}

	}
	
	class CommandDockBar: ICommandBar
	{
		List<ToolButtonStatus> buttons = new List<ToolButtonStatus> ();
		DockItemToolbar toolbar;
		object initialTarget;
		
		public CommandDockBar (DockItemToolbar toolbar, object initialTarget)
		{
			this.toolbar = toolbar;
			this.initialTarget = initialTarget;
			if (toolbar.DockItem.ContentVisible)
				IdeApp.CommandService.RegisterCommandBar (this);
			toolbar.DockItem.ContentVisibleChanged += HandleToolbarDockItemContentVisibleChanged;
		}

		void HandleToolbarDockItemContentVisibleChanged (object sender, EventArgs e)
		{
			if (toolbar.DockItem.ContentVisible)
				IdeApp.CommandService.RegisterCommandBar (this);
			else
				IdeApp.CommandService.UnregisterCommandBar (this);
		}
		
		public void Add (CommandEntry entry)
		{
			Widget w = CreateWidget (entry);
			if (w is Button) {
				buttons.Add (new ToolButtonStatus (entry.CommandId, (Gtk.Button) w));
				((Gtk.Button) w).Clicked += delegate {
					IdeApp.CommandService.DispatchCommand (entry.CommandId, null, initialTarget, CommandSource.MainToolbar);
				};
			}
			toolbar.Add (w);
		}
		
		public void Update (object activeTarget)
		{
			foreach (ToolButtonStatus bs in buttons)
				bs.Update (initialTarget ?? activeTarget);
		}
		
		public void SetEnabled (bool enabled)
		{
			toolbar.Sensitive = enabled;
		}
		
		static Gtk.Widget CreateWidget (CommandEntry entry)
		{
			if (entry.CommandId == Command.Separator)
				return new Gtk.SeparatorToolItem ();

			Command cmd = entry.GetCommand (IdeApp.CommandService);
			if (cmd == null)
				return new Gtk.Label ();
			
			if (cmd is CustomCommand) {
				Gtk.Widget ti = (Gtk.Widget) Activator.CreateInstance (((CustomCommand)cmd).WidgetType);
				if (cmd.Text != null && cmd.Text.Length > 0) {
					//strip "_" accelerators from tooltips
					string text = cmd.Text;
					while (true) {
						int underscoreIndex = text.IndexOf ('_');
						if (underscoreIndex > -1)
							text = text.Remove (underscoreIndex, 1);
						else
							break;
					}
					ti.TooltipText = text;
				}
				return ti;
			}
			
			ActionCommand acmd = cmd as ActionCommand;
			if (acmd == null)
				throw new InvalidOperationException ("Unknown cmd type.");

			if (acmd.CommandArray) {
				throw new InvalidOperationException ("Command arrays not supported.");
			}
			else if (acmd.ActionType == ActionType.Normal)
				return new Button ();
			else
				return new ToggleButton ();
		}
	}
	
	class ToolButtonStatus
	{
		string lastDesc;
		string stockId;
		Button button;
		object cmdId;
		
		public ToolButtonStatus (object cmdId, Button button)
		{
			this.cmdId = cmdId;
			this.button = button;
		}
		
		public void Update (object initialTarget)
		{
			CommandInfo cmdInfo = IdeApp.CommandService.GetCommandInfo (cmdId, initialTarget);
			
			if (lastDesc != cmdInfo.Description) {
				string toolTip;
				if (string.IsNullOrEmpty (cmdInfo.AccelKey)) {
					toolTip = cmdInfo.Description;
				} else {
					toolTip = cmdInfo.Description + " (" + KeyBindingManager.BindingToDisplayLabel (cmdInfo.AccelKey, false) + ")";
				}
				button.TooltipText = toolTip;
				lastDesc = cmdInfo.Description;
			}
			
			if (cmdInfo.Icon.IsNull && button.Label != cmdInfo.Text)
				button.Label = cmdInfo.Text;
			
			if (cmdInfo.Icon != stockId) {
				stockId = cmdInfo.Icon;
				button.Image = new Gtk.Image (cmdInfo.Icon, Gtk.IconSize.Menu);
			}
			if (cmdInfo.Enabled != button.Sensitive)
				button.Sensitive = cmdInfo.Enabled;
			if (cmdInfo.Visible != button.Visible)
				button.Visible = cmdInfo.Visible;
			
			ToggleButton toggle = button as ToggleButton;
			if (toggle != null && cmdInfo.Checked != toggle.Active)
				toggle.Active = cmdInfo.Checked;
				
			if (button.Image != null)
				button.Image.Show ();
		}
	}
}
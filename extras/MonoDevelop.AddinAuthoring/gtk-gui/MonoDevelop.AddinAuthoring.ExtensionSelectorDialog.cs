
// This file has been generated by the GUI designer. Do not modify.
namespace MonoDevelop.AddinAuthoring
{
	public partial class ExtensionSelectorDialog
	{
		private global::Gtk.VBox vbox5;

		private global::Gtk.Label label13;

		private global::Gtk.ScrolledWindow scrolledwindow5;

		private global::Gtk.TreeView tree;

		private global::Gtk.Button button785;

		private global::Gtk.Button button789;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MonoDevelop.AddinAuthoring.ExtensionSelectorDialog
			this.Events = ((global::Gdk.EventMask)(256));
			this.Name = "MonoDevelop.AddinAuthoring.ExtensionSelectorDialog";
			this.Title = global::Mono.Addins.AddinManager.CurrentLocalizer.GetString ("Extension Selector");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child MonoDevelop.AddinAuthoring.ExtensionSelectorDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Events = ((global::Gdk.EventMask)(256));
			w1.Name = "dialog_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog_VBox.Gtk.Box+BoxChild
			this.vbox5 = new global::Gtk.VBox ();
			this.vbox5.Name = "vbox5";
			this.vbox5.Spacing = 6;
			this.vbox5.BorderWidth = ((uint)(12));
			// Container child vbox5.Gtk.Box+BoxChild
			this.label13 = new global::Gtk.Label ();
			this.label13.Name = "label13";
			this.label13.Xalign = 0f;
			this.label13.LabelProp = global::Mono.Addins.AddinManager.CurrentLocalizer.GetString ("Select the extension points to be extended:");
			this.vbox5.Add (this.label13);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox5[this.label13]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox5.Gtk.Box+BoxChild
			this.scrolledwindow5 = new global::Gtk.ScrolledWindow ();
			this.scrolledwindow5.CanFocus = true;
			this.scrolledwindow5.Name = "scrolledwindow5";
			this.scrolledwindow5.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child scrolledwindow5.Gtk.Container+ContainerChild
			this.tree = new global::Gtk.TreeView ();
			this.tree.CanFocus = true;
			this.tree.Name = "tree";
			this.scrolledwindow5.Add (this.tree);
			this.vbox5.Add (this.scrolledwindow5);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox5[this.scrolledwindow5]));
			w4.Position = 1;
			w1.Add (this.vbox5);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(w1[this.vbox5]));
			w5.Position = 0;
			// Internal child MonoDevelop.AddinAuthoring.ExtensionSelectorDialog.ActionArea
			global::Gtk.HButtonBox w6 = this.ActionArea;
			w6.Name = "MonoDevelop.AddinAuthoring.ExtensionSelectorDialog_ActionArea";
			w6.Spacing = 6;
			w6.BorderWidth = ((uint)(5));
			w6.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child MonoDevelop.AddinAuthoring.ExtensionSelectorDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.button785 = new global::Gtk.Button ();
			this.button785.CanDefault = true;
			this.button785.CanFocus = true;
			this.button785.Name = "button785";
			this.button785.UseStock = true;
			this.button785.UseUnderline = true;
			this.button785.Label = "gtk-cancel";
			this.AddActionWidget (this.button785, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w7 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w6[this.button785]));
			w7.Expand = false;
			w7.Fill = false;
			// Container child MonoDevelop.AddinAuthoring.ExtensionSelectorDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.button789 = new global::Gtk.Button ();
			this.button789.CanDefault = true;
			this.button789.CanFocus = true;
			this.button789.Name = "button789";
			this.button789.UseStock = true;
			this.button789.UseUnderline = true;
			this.button789.Label = "gtk-ok";
			this.AddActionWidget (this.button789, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w8 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w6[this.button789]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 652;
			this.DefaultHeight = 526;
			this.Show ();
		}
	}
}

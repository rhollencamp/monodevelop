
// This file has been generated by the GUI designer. Do not modify.
namespace MonoDevelop.IPhone.Gui
{
	internal partial class IPhoneBuildOptionsWidget
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.Label label2;

		private global::Gtk.Alignment contentsAlignment;

		private global::Gtk.VBox vbox1;

		private global::Gtk.CheckButton debugCheck;

		private global::Gtk.Table table1;

		private global::MonoDevelop.Components.MenuButtonEntry extraArgsEntry;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TreeView i18nTreeView;

		private global::Gtk.Label label1;

		private global::Gtk.Label label3;

		private global::Gtk.Label label4;

		private global::Gtk.Label label5;

		private global::Gtk.Label label6;

		private global::Gtk.ComboBox linkCombo;

		private global::Gtk.ComboBoxEntry minOSComboEntry;

		private global::Gtk.ComboBoxEntry sdkComboEntry;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MonoDevelop.IPhone.Gui.IPhoneBuildOptionsWidget
			global::Stetic.BinContainer.Attach (this);
			this.Name = "MonoDevelop.IPhone.Gui.IPhoneBuildOptionsWidget";
			// Container child MonoDevelop.IPhone.Gui.IPhoneBuildOptionsWidget.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.Xalign = 0f;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>MonoTouch iPhone compiler</b>");
			this.label2.UseMarkup = true;
			this.vbox2.Add (this.label2);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.label2]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.contentsAlignment = new global::Gtk.Alignment (0f, 0.5f, 1f, 1f);
			this.contentsAlignment.Name = "contentsAlignment";
			this.contentsAlignment.LeftPadding = ((uint)(24));
			// Container child contentsAlignment.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.debugCheck = new global::Gtk.CheckButton ();
			this.debugCheck.CanFocus = true;
			this.debugCheck.Name = "debugCheck";
			this.debugCheck.Label = global::Mono.Unix.Catalog.GetString ("Build _debug-mode binaries");
			this.debugCheck.DrawIndicator = true;
			this.debugCheck.UseUnderline = true;
			this.vbox1.Add (this.debugCheck);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.debugCheck]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(5)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.extraArgsEntry = new global::MonoDevelop.Components.MenuButtonEntry ();
			this.extraArgsEntry.Name = "extraArgsEntry";
			this.table1.Add (this.extraArgsEntry);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.extraArgsEntry]));
			w3.TopAttach = ((uint)(3));
			w3.BottomAttach = ((uint)(4));
			w3.LeftAttach = ((uint)(1));
			w3.RightAttach = ((uint)(2));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.i18nTreeView = new global::Gtk.TreeView ();
			this.i18nTreeView.CanFocus = true;
			this.i18nTreeView.Name = "i18nTreeView";
			this.GtkScrolledWindow.Add (this.i18nTreeView);
			this.table1.Add (this.GtkScrolledWindow);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindow]));
			w5.TopAttach = ((uint)(4));
			w5.BottomAttach = ((uint)(5));
			w5.LeftAttach = ((uint)(1));
			w5.RightAttach = ((uint)(2));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.Xalign = 0f;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Extra _arguments:");
			this.label1.UseUnderline = true;
			this.table1.Add (this.label1);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w6.TopAttach = ((uint)(3));
			w6.BottomAttach = ((uint)(4));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.Xalign = 0f;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("_Linker behavior:");
			this.label3.UseUnderline = true;
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.label3]));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.Xalign = 0f;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("_SDK version:");
			this.label4.UseUnderline = true;
			this.table1.Add (this.label4);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1[this.label4]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.Xalign = 0f;
			this.label5.Yalign = 0f;
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("I18n assemblies:");
			this.label5.UseUnderline = true;
			this.table1.Add (this.label5);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.label5]));
			w9.TopAttach = ((uint)(4));
			w9.BottomAttach = ((uint)(5));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.Xalign = 0f;
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Minimum _OS version:");
			this.label6.UseUnderline = true;
			this.table1.Add (this.label6);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.label6]));
			w10.TopAttach = ((uint)(2));
			w10.BottomAttach = ((uint)(3));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.linkCombo = global::Gtk.ComboBox.NewText ();
			this.linkCombo.Name = "linkCombo";
			this.table1.Add (this.linkCombo);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1[this.linkCombo]));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.minOSComboEntry = global::Gtk.ComboBoxEntry.NewText ();
			this.minOSComboEntry.Name = "minOSComboEntry";
			this.table1.Add (this.minOSComboEntry);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1[this.minOSComboEntry]));
			w12.TopAttach = ((uint)(2));
			w12.BottomAttach = ((uint)(3));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.sdkComboEntry = global::Gtk.ComboBoxEntry.NewText ();
			this.sdkComboEntry.Name = "sdkComboEntry";
			this.table1.Add (this.sdkComboEntry);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1[this.sdkComboEntry]));
			w13.TopAttach = ((uint)(1));
			w13.BottomAttach = ((uint)(2));
			w13.LeftAttach = ((uint)(1));
			w13.RightAttach = ((uint)(2));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox1.Add (this.table1);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.table1]));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
			this.contentsAlignment.Add (this.vbox1);
			this.vbox2.Add (this.contentsAlignment);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.contentsAlignment]));
			w16.Position = 1;
			this.Add (this.vbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.label3.MnemonicWidget = this.linkCombo;
			this.label5.MnemonicWidget = this.i18nTreeView;
			this.Hide ();
		}
	}
}
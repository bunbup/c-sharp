
// This file has been generated by the GUI designer. Do not modify.
namespace PubnubMessagingExampleGTK
{
	public partial class AskChannelDialog
	{
		private global::Gtk.Entry entryChannel;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget PubnubMessagingExampleGTK.AskChannelDialog
			this.Name = "PubnubMessagingExampleGTK.AskChannelDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("Enter Channel Name");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child PubnubMessagingExampleGTK.AskChannelDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.entryChannel = new global::Gtk.Entry ();
			this.entryChannel.CanFocus = true;
			this.entryChannel.Name = "entryChannel";
			this.entryChannel.IsEditable = true;
			this.entryChannel.InvisibleChar = '•';
			w1.Add (this.entryChannel);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(w1 [this.entryChannel]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Internal child PubnubMessagingExampleGTK.AskChannelDialog.ActionArea
			global::Gtk.HButtonBox w3 = this.ActionArea;
			w3.Name = "dialog1_ActionArea";
			w3.Spacing = 10;
			w3.BorderWidth = ((uint)(5));
			w3.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w4 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w3 [this.buttonCancel]));
			w4.Expand = false;
			w4.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			w3.Add (this.buttonOk);
			global::Gtk.ButtonBox.ButtonBoxChild w5 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w3 [this.buttonOk]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 111;
			this.Show ();
			this.buttonOk.Clicked += new global::System.EventHandler (this.OkClicked);
		}
	}
}

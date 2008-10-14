namespace Cf.PassiveView.Source
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.ShowMessage = new System.Windows.Forms.Button();
            this.Message = new System.Windows.Forms.Label();
            this.ColourSelection = new System.Windows.Forms.ComboBox();
            this.SelectColourMessage = new System.Windows.Forms.Label();
            this.HideMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 36);
            this.label1.Text = "Demonstration of Passive View to test Compact Framework";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ShowMessage
            // 
            this.ShowMessage.Location = new System.Drawing.Point(4, 44);
            this.ShowMessage.Name = "ShowMessage";
            this.ShowMessage.Size = new System.Drawing.Size(121, 20);
            this.ShowMessage.TabIndex = 1;
            this.ShowMessage.Text = "Show Message";
            // 
            // Message
            // 
            this.Message.Location = new System.Drawing.Point(4, 71);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(233, 52);
            this.Message.Text = "This message is hidden or displayed to demonstrate testing visiblity of controls " +
                "in NUnit";
            this.Message.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ColourSelection
            // 
            this.ColourSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColourSelection.Location = new System.Drawing.Point(110, 127);
            this.ColourSelection.Name = "ColourSelection";
            this.ColourSelection.Size = new System.Drawing.Size(126, 22);
            this.ColourSelection.TabIndex = 4;
            // 
            // SelectColourMessage
            // 
            this.SelectColourMessage.Location = new System.Drawing.Point(4, 127);
            this.SelectColourMessage.Name = "SelectColourMessage";
            this.SelectColourMessage.Size = new System.Drawing.Size(100, 20);
            this.SelectColourMessage.Text = "Select a colour:";
            this.SelectColourMessage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // HideMessage
            // 
            this.HideMessage.Location = new System.Drawing.Point(4, 151);
            this.HideMessage.Name = "HideMessage";
            this.HideMessage.Size = new System.Drawing.Size(121, 20);
            this.HideMessage.TabIndex = 6;
            this.HideMessage.Text = "Hide message";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.HideMessage);
            this.Controls.Add(this.SelectColourMessage);
            this.Controls.Add(this.ColourSelection);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.ShowMessage);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "MainView";
            this.Text = "Passive View Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button ShowMessage;
        public System.Windows.Forms.ComboBox ColourSelection;
        public System.Windows.Forms.Button HideMessage;
        public System.Windows.Forms.Label Message;
        public System.Windows.Forms.Label SelectColourMessage;
    }
}
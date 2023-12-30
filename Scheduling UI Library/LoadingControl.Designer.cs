using System.ComponentModel;

namespace Scheduling_UI_Library
{
    partial class LoadingControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loadingPictureBox = new PictureBox();
            loadingLbl = new Label();
            ((ISupportInitialize)loadingPictureBox).BeginInit();
            SuspendLayout();
            // 
            // loadingPictureBox
            // 
            loadingPictureBox.Image = Properties.Resources.spinner_frame__1_;
            loadingPictureBox.Location = new Point(3, 3);
            loadingPictureBox.Name = "loadingPictureBox";
            loadingPictureBox.Size = new Size(75, 75);
            loadingPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            loadingPictureBox.TabIndex = 0;
            loadingPictureBox.TabStop = false;
            // 
            // loadingLbl
            // 
            loadingLbl.AutoSize = true;
            loadingLbl.Location = new Point(3, 81);
            loadingLbl.MinimumSize = new Size(75, 0);
            loadingLbl.Name = "loadingLbl";
            loadingLbl.Padding = new Padding(13, 0, 0, 0);
            loadingLbl.Size = new Size(75, 15);
            loadingLbl.TabIndex = 1;
            loadingLbl.Text = "Loading";
            loadingLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LoadingControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(loadingLbl);
            this.Controls.Add(loadingPictureBox);
            this.Name = "LoadingControl";
            this.Size = new Size(81, 134);
            this.Load += LoadingControl_Load;
            ((ISupportInitialize)loadingPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox loadingPictureBox;
        private Label loadingLbl;
    }
}

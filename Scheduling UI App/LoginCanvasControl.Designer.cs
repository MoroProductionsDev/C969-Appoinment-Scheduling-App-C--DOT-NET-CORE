using Scheduling_UI_Library;

namespace Scheduling_UI_App
{
    partial class LoginCanvasControl
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            errorLbl = new Label();
            loginControl = new LoginControl();
            loadingControl = new LoadingControl();
            locationLbl = new Label();
            languageComboBox = new ComboBox();
            SuspendLayout();
            // 
            // errorLbl
            // 
            errorLbl.AutoSize = true;
            errorLbl.ForeColor = Color.Firebrick;
            errorLbl.Location = new Point(52, 336);
            errorLbl.MinimumSize = new Size(300, 23);
            errorLbl.Name = "errorLbl";
            errorLbl.Size = new Size(300, 23);
            errorLbl.TabIndex = 2;
            errorLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // loginControl
            // 
            loginControl.Location = new Point(16, 145);
            loginControl.Name = "loginControl";
            loginControl.Size = new Size(382, 188);
            loginControl.TabIndex = 3;
            // 
            // loadingControl
            // 
            loadingControl.Location = new Point(161, 14);
            loadingControl.Name = "loadingControl";
            loadingControl.Size = new Size(81, 106);
            loadingControl.TabIndex = 4;
            // 
            // locationLbl
            // 
            locationLbl.Location = new Point(26, 15);
            locationLbl.Name = "locationLbl";
            locationLbl.Size = new Size(100, 23);
            locationLbl.TabIndex = 1;
            locationLbl.Text = "Location...";
            locationLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // languageComboBox
            // 
            languageComboBox.FormattingEnabled = true;
            languageComboBox.Location = new Point(268, 15);
            languageComboBox.Name = "languageComboBox";
            languageComboBox.Size = new Size(121, 23);
            languageComboBox.TabIndex = 0;
            languageComboBox.SelectedIndexChanged += OnComboBoxChanged;
            // 
            // LoginCanvasControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            this.Controls.Add(languageComboBox);
            this.Controls.Add(locationLbl);
            this.Controls.Add(errorLbl);
            this.Controls.Add(loginControl);
            this.Controls.Add(loadingControl);
            this.Name = "LoginCanvasControl";
            this.Size = new Size(414, 421);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LoginControl loginControl;
        private Label errorLbl;
        private LoadingControl loadingControl;
        private Label locationLbl;
        private ComboBox languageComboBox;
    }
}
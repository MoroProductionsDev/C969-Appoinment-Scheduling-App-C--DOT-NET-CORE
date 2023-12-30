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
            SuspendLayout();
            // 
            // errorLbl
            // 
            errorLbl.AutoSize = true;
            errorLbl.ForeColor = Color.Red;
            errorLbl.Location = new Point(118, 349);
            errorLbl.MaximumSize = new Size(300, 0);
            errorLbl.MinimumSize = new Size(300, 0);
            errorLbl.Name = "errorLbl";
            errorLbl.Size = new Size(300, 15);
            errorLbl.TabIndex = 0;
            errorLbl.TextAlign = ContentAlignment.TopCenter;
            // 
            // loginControl
            // 
            loginControl.Location = new Point(82, 131);
            loginControl.Name = "loginControl";
            loginControl.Size = new Size(382, 215);
            loginControl.TabIndex = 1;
            // 
            // loadingControl
            // 
            loadingControl.Location = new Point(227, 14);
            loadingControl.Name = "loadingControl";
            loadingControl.Size = new Size(81, 111);
            loadingControl.TabIndex = 2;
            // 
            // LoginCanvasControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            this.Controls.Add(errorLbl);
            this.Controls.Add(loginControl);
            this.Controls.Add(loadingControl);
            this.Name = "LoginCanvasControl";
            this.Size = new Size(554, 450);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LoginControl loginControl;
        private Label errorLbl;
        private LoadingControl loadingControl;
    }
}
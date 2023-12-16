using Scheduling_UI_Library;

namespace Scheduling_UI_App
{
    partial class LoginForm
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
            loginControl1 = new LoginControl();
            SuspendLayout();
            // 
            // loginControl1
            // 
            loginControl1.Location = new Point(86, 108);
            loginControl1.Name = "loginControl1";
            loginControl1.Size = new Size(382, 215);
            loginControl1.TabIndex = 0;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(554, 450);
            this.Controls.Add(loginControl1);
            this.Name = "LoginForm";
            this.Text = "Scheduling Application";
            ResumeLayout(false);
        }

        #endregion

        private LoginControl loginControl1;
    }
}
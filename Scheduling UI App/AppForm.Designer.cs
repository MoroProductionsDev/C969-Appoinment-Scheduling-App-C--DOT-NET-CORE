using Scheduling_UI_Library;

namespace Scheduling_UI_App
{
    partial class AppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppForm));
            this.appStateBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.loginCanvas = new LoginCanvasControl();
            SuspendLayout();
            // 
            // loginCanvas
            // 
            this.loginCanvas.BackColor = SystemColors.Control;
            this.loginCanvas.BackgroundImage = (Image)resources.GetObject("loginCanvas.BackgroundImage");
            this.loginCanvas.Location = new Point(0, 0);
            this.loginCanvas.Name = "loginCanvas";
            this.loginCanvas.Size = new Size(554, 450);
            this.loginCanvas.TabIndex = 0;
            this.loginCanvas.Visible = false;
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ButtonHighlight;
            this.ClientSize = new Size(554, 449);
            this.Controls.Add(this.loginCanvas);
            this.Name = "AppForm";
            this.Text = "Scheduling Application";
            this.Load += AppForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker appStateBackgroundWorker;
        private LoginCanvasControl loginCanvas;
    }
}
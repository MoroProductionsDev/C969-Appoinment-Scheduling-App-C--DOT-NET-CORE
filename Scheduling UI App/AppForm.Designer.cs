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
            this.appointmentCanvas = new AppointmentControl();
            this.loginCanvas = new LoginCanvasControl();
            SuspendLayout();
            // 
            // appointmentCanvas
            // 
            this.appointmentCanvas.Anchor = AnchorStyles.None;
            this.appointmentCanvas.BackColor = SystemColors.Control;
            this.appointmentCanvas.Location = new Point(0, 0);
            this.appointmentCanvas.Name = "appointmentCanvas";
            this.appointmentCanvas.Size = new Size(1141, 659);
            this.appointmentCanvas.TabIndex = 0;
            this.appointmentCanvas.Visible = false;
            // 
            // loginCanvas
            // 
            this.loginCanvas.Anchor = AnchorStyles.None;
            this.loginCanvas.BackColor = SystemColors.Control;
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
            this.BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            this.ClientSize = new Size(554, 450);
            this.Controls.Add(this.loginCanvas);
            this.Controls.Add(this.appointmentCanvas);
            this.MinimumSize = new Size(300, 300);
            this.Name = "AppForm";
            this.Text = "Scheduling Application";
            this.Load += AppForm_Load;
            this.Resize += ScaleBackgroundImage;
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker appStateBackgroundWorker;
        private System.ComponentModel.ComponentResourceManager resources;
        private LoginCanvasControl loginCanvas;
        private AppointmentControl appointmentCanvas;
    }
}
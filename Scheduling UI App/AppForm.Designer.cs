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
            this.components = new System.ComponentModel.Container();
            this.appStateBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            appStateBindingSource = new BindingSource(this.components);
            this.appointmentCanvas = new AppointmentControl();
            this.addCustomerControl = new AddCustomerControl();
            this.addAppointmentControl = new AddAppointmentControl();
            this.updateCustomerControl = new UpdateCustomerControl();
            this.updateAppointmentControl = new UpdateAppointmentControl();
            this.reportControl = new ReportControl();
            this.loginCanvas = new LoginCanvasControl();
            ((System.ComponentModel.ISupportInitialize)appStateBindingSource).BeginInit();
            SuspendLayout();
            // 
            // appStateBindingSource
            // 
            appStateBindingSource.DataSource = typeof(Scheduling_API.Controller.State.AppState);
            // 
            // appointmentCanvas
            // 
            this.appointmentCanvas.Anchor = AnchorStyles.None;
            this.appointmentCanvas.BackColor = SystemColors.Control;
            this.appointmentCanvas.Location = new Point(-200, 112);
            this.appointmentCanvas.Name = "appointmentCanvas";
            this.appointmentCanvas.Size = new Size(1381, 660);
            this.appointmentCanvas.TabIndex = 0;
            this.appointmentCanvas.Visible = false;
            // 
            // loginCanvas
            // 
            this.loginCanvas.Anchor = AnchorStyles.None;
            this.loginCanvas.BackColor = SystemColors.Control;
            this.loginCanvas.Location = new Point(250, 182);
            this.loginCanvas.Name = "loginCanvas";
            this.loginCanvas.Size = new Size(554, 450);
            this.loginCanvas.TabIndex = 0;
            // 
            // addCustomerControl
            // 
            this.addCustomerControl.Anchor = AnchorStyles.None;
            this.addCustomerControl.BackColor = SystemColors.Control;
            this.addCustomerControl.Location = new Point(270, 125);
            this.addCustomerControl.MinimumSize = new Size(270, 0);
            this.addCustomerControl.Name = "addCustomerControl";
            this.addCustomerControl.Size = new Size(1381, 660);
            this.addCustomerControl.TabIndex = 0;
            this.addCustomerControl.Visible = false;
            // 
            // addAppointmentControl
            // 
            this.addAppointmentControl.Anchor = AnchorStyles.None;
            this.addAppointmentControl.BackColor = SystemColors.Control;
            this.addAppointmentControl.Location = new Point(-30, 182);
            this.addAppointmentControl.Name = "addAppointmentControl";
            this.addAppointmentControl.Size = new Size(1381, 660);
            this.addAppointmentControl.TabIndex = 0;
            this.addAppointmentControl.Visible = false;
            // 
            // updateCustomerControl
            // 
            this.updateCustomerControl.Anchor = AnchorStyles.None;
            this.updateCustomerControl.BackColor = SystemColors.Control;
            this.updateCustomerControl.Location = new Point(230, 172);
            this.updateCustomerControl.Name = "updateCustomerControl";
            this.updateCustomerControl.Size = new Size(1381, 660);
            this.updateCustomerControl.TabIndex = 0;
            this.updateCustomerControl.Visible = false;
            // 
            // updateAppointmentControl
            // 
            this.updateAppointmentControl.Anchor = AnchorStyles.None;
            this.updateAppointmentControl.BackColor = SystemColors.Control;
            this.updateAppointmentControl.Location = new Point(200, 182);
            this.updateAppointmentControl.Name = "updateAppointmentControl";
            this.updateAppointmentControl.Size = new Size(1381, 660);
            this.updateAppointmentControl.TabIndex = 0;
            this.updateAppointmentControl.Visible = false;
            // 
            // reportControl
            // 
            this.reportControl.Anchor = AnchorStyles.None;
            this.reportControl.BackColor = SystemColors.Control;
            this.reportControl.Location = new Point(0, 182);
            this.reportControl.Name = "reportControl";
            this.reportControl.Size = new Size(1381, 660);
            this.reportControl.TabIndex = 0;
            this.reportControl.Visible = false;
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ButtonHighlight;
            this.ClientSize = new Size(915, 784);
            this.Controls.Add(this.loginCanvas);
            this.Controls.Add(this.appointmentCanvas);
            this.Controls.Add(this.addCustomerControl);
            this.Controls.Add(this.addAppointmentControl);
            this.Controls.Add(this.updateCustomerControl);
            this.Controls.Add(this.updateAppointmentControl);
            this.Controls.Add(this.reportControl);
            this.MinimumSize = new Size(300, 300);
            this.Name = "AppForm";
            this.Text = "Scheduling Application";
            this.Load += AppForm_Load;
            this.Resize += ScaleBackgroundImage;
            ((System.ComponentModel.ISupportInitialize)appStateBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker appStateBackgroundWorker;
        private LoginCanvasControl loginCanvas;
        private AppointmentControl appointmentCanvas;
        private AddCustomerControl addCustomerControl;
        private AddAppointmentControl addAppointmentControl;
        private UpdateCustomerControl updateCustomerControl;
        private UpdateAppointmentControl updateAppointmentControl;
        private ReportControl reportControl;
        private BindingSource appStateBindingSource;
    }
}
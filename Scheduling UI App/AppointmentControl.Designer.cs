namespace Scheduling_UI_App
{
    partial class AppointmentControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            customerDataGridView = new DataGridView();
            appStateBindingSource = new BindingSource(this.components);
            appoinmentDataGridView = new DataGridView();
            addRecordBtn = new Button();
            updateRecordBtn = new Button();
            deleteRecordBtn = new Button();
            customerRdBtn = new RadioButton();
            appointmentRdBtn = new RadioButton();
            custApptDescriptionLbl = new Label();
            customerViewLbl = new Label();
            appointmentViewLbl = new Label();
            errorLbl = new Label();
            reportBtn = new Button();
            OfficeHoursLbl = new Label();
            offficeHoursLocalTimeLbl = new Label();
            timeZoneLbl = new Label();
            userNameLbl = new Label();
            signOutLinkLbl = new LinkLabel();
            this.databaseStatusPanel = new Scheduling_UI_Library.DatabaseStatusPanel();
            ((System.ComponentModel.ISupportInitialize)customerDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)appStateBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)appoinmentDataGridView).BeginInit();
            SuspendLayout();
            // 
            // customerDataGridView
            // 
            customerDataGridView.AllowUserToAddRows = false;
            customerDataGridView.AllowUserToDeleteRows = false;
            customerDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            customerDataGridView.DataBindings.Add(new Binding("Tag", appStateBindingSource, "DbDataSet.DataSet", true));
            customerDataGridView.Location = new Point(33, 139);
            customerDataGridView.Name = "customerDataGridView";
            customerDataGridView.ReadOnly = true;
            customerDataGridView.RowTemplate.Height = 25;
            customerDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            customerDataGridView.Size = new Size(710, 233);
            customerDataGridView.TabIndex = 1;
            // 
            // appStateBindingSource
            // 
            appStateBindingSource.DataSource = typeof(Scheduling_API.Controller.State.AppState);
            // 
            // appoinmentDataGridView
            // 
            appoinmentDataGridView.AllowUserToAddRows = false;
            appoinmentDataGridView.AllowUserToDeleteRows = false;
            appoinmentDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            appoinmentDataGridView.DataBindings.Add(new Binding("Tag", appStateBindingSource, "DbDataSet.DataSet", true));
            appoinmentDataGridView.Location = new Point(33, 422);
            appoinmentDataGridView.Name = "appoinmentDataGridView";
            appoinmentDataGridView.ReadOnly = true;
            appoinmentDataGridView.RowTemplate.Height = 25;
            appoinmentDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            appoinmentDataGridView.Size = new Size(1247, 276);
            appoinmentDataGridView.TabIndex = 3;
            appoinmentDataGridView.SelectionChanged += AppoinmentDataGridView_SelectionChanged;
            // 
            // addRecordBtn
            // 
            addRecordBtn.Location = new Point(777, 218);
            addRecordBtn.Name = "addRecordBtn";
            addRecordBtn.Size = new Size(145, 27);
            addRecordBtn.TabIndex = 4;
            addRecordBtn.Text = "Add: ";
            addRecordBtn.UseVisualStyleBackColor = true;
            // 
            // updateRecordBtn
            // 
            updateRecordBtn.Location = new Point(928, 218);
            updateRecordBtn.Name = "updateRecordBtn";
            updateRecordBtn.Size = new Size(145, 27);
            updateRecordBtn.TabIndex = 5;
            updateRecordBtn.Text = "Update: ";
            updateRecordBtn.UseVisualStyleBackColor = true;
            // 
            // deleteRecordBtn
            // 
            deleteRecordBtn.Location = new Point(1079, 218);
            deleteRecordBtn.Name = "deleteRecordBtn";
            deleteRecordBtn.Size = new Size(145, 27);
            deleteRecordBtn.TabIndex = 6;
            deleteRecordBtn.Text = "Delete: ";
            deleteRecordBtn.UseVisualStyleBackColor = true;
            // 
            // customerRdBtn
            // 
            customerRdBtn.AutoSize = true;
            customerRdBtn.Location = new Point(897, 180);
            customerRdBtn.Name = "customerRdBtn";
            customerRdBtn.Size = new Size(77, 19);
            customerRdBtn.TabIndex = 7;
            customerRdBtn.TabStop = true;
            customerRdBtn.Text = "Customer";
            customerRdBtn.UseVisualStyleBackColor = true;
            customerRdBtn.CheckedChanged += CustomerRdBtn_CheckedChanged;
            // 
            // appointmentRdBtn
            // 
            appointmentRdBtn.AutoSize = true;
            appointmentRdBtn.Location = new Point(999, 180);
            appointmentRdBtn.Name = "appointmentRdBtn";
            appointmentRdBtn.Size = new Size(96, 19);
            appointmentRdBtn.TabIndex = 8;
            appointmentRdBtn.TabStop = true;
            appointmentRdBtn.Text = "Appointment";
            appointmentRdBtn.UseVisualStyleBackColor = true;
            appointmentRdBtn.CheckedChanged += AppointmentRdBtn_CheckedChanged;
            // 
            // custApptDescriptionLbl
            // 
            custApptDescriptionLbl.AutoSize = true;
            custApptDescriptionLbl.Location = new Point(865, 135);
            custApptDescriptionLbl.Name = "custApptDescriptionLbl";
            custApptDescriptionLbl.Size = new Size(249, 15);
            custApptDescriptionLbl.TabIndex = 9;
            custApptDescriptionLbl.Text = "Choose the [Type] you want to take action on.";
            // 
            // customerViewLbl
            // 
            customerViewLbl.AutoSize = true;
            customerViewLbl.Location = new Point(286, 121);
            customerViewLbl.Name = "customerViewLbl";
            customerViewLbl.Size = new Size(130, 15);
            customerViewLbl.TabIndex = 10;
            customerViewLbl.Text = "Customer(s) Table View";
            // 
            // appointmentViewLbl
            // 
            appointmentViewLbl.AutoSize = true;
            appointmentViewLbl.Location = new Point(594, 404);
            appointmentViewLbl.Name = "appointmentViewLbl";
            appointmentViewLbl.Size = new Size(149, 15);
            appointmentViewLbl.TabIndex = 11;
            appointmentViewLbl.Text = "Appointment(s) Table View";
            // 
            // errorLbl
            // 
            errorLbl.AutoSize = true;
            errorLbl.ForeColor = Color.Firebrick;
            errorLbl.Location = new Point(777, 274);
            errorLbl.MinimumSize = new Size(450, 0);
            errorLbl.Name = "errorLbl";
            errorLbl.Size = new Size(450, 15);
            errorLbl.TabIndex = 12;
            // 
            // reportBtn
            // 
            reportBtn.Location = new Point(1156, 326);
            reportBtn.Name = "reportBtn";
            reportBtn.Size = new Size(68, 42);
            reportBtn.TabIndex = 13;
            reportBtn.Text = "Reports";
            reportBtn.UseVisualStyleBackColor = true;
            // 
            // OfficeHoursLbl
            // 
            OfficeHoursLbl.AutoSize = true;
            OfficeHoursLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            OfficeHoursLbl.Location = new Point(640, 21);
            OfficeHoursLbl.Name = "OfficeHoursLbl";
            OfficeHoursLbl.Size = new Size(97, 20);
            OfficeHoursLbl.TabIndex = 22;
            OfficeHoursLbl.Text = "Office Hours";
            // 
            // offficeHoursLocalTimeLbl
            // 
            offficeHoursLocalTimeLbl.AutoSize = true;
            offficeHoursLocalTimeLbl.Location = new Point(637, 53);
            offficeHoursLocalTimeLbl.MinimumSize = new Size(100, 0);
            offficeHoursLocalTimeLbl.Name = "offficeHoursLocalTimeLbl";
            offficeHoursLocalTimeLbl.Size = new Size(100, 15);
            offficeHoursLocalTimeLbl.TabIndex = 23;
            // 
            // timeZoneLbl
            // 
            timeZoneLbl.AutoSize = true;
            timeZoneLbl.Location = new Point(637, 81);
            timeZoneLbl.MaximumSize = new Size(50, 0);
            timeZoneLbl.MinimumSize = new Size(100, 0);
            timeZoneLbl.Name = "timeZoneLbl";
            timeZoneLbl.Size = new Size(100, 15);
            timeZoneLbl.TabIndex = 24;
            // 
            // userNameLbl
            // 
            userNameLbl.AutoSize = true;
            userNameLbl.Location = new Point(33, 26);
            userNameLbl.Name = "userNameLbl";
            userNameLbl.Size = new Size(66, 15);
            userNameLbl.TabIndex = 25;
            userNameLbl.Text = "Username: ";
            // 
            // signOutLinkLbl
            // 
            signOutLinkLbl.AutoSize = true;
            signOutLinkLbl.Location = new Point(33, 70);
            signOutLinkLbl.Name = "signOutLinkLbl";
            signOutLinkLbl.Size = new Size(53, 15);
            signOutLinkLbl.TabIndex = 26;
            signOutLinkLbl.TabStop = true;
            signOutLinkLbl.Text = "Sign Out";
            // 
            // databaseStatusPanel
            // 
            this.databaseStatusPanel.Location = new Point(1038, 26);
            this.databaseStatusPanel.Name = "databaseStatusPanel";
            this.databaseStatusPanel.Size = new Size(242, 45);
            this.databaseStatusPanel.TabIndex = 27;
            // 
            // AppointmentControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.databaseStatusPanel);
            this.Controls.Add(signOutLinkLbl);
            this.Controls.Add(userNameLbl);
            this.Controls.Add(timeZoneLbl);
            this.Controls.Add(offficeHoursLocalTimeLbl);
            this.Controls.Add(OfficeHoursLbl);
            this.Controls.Add(reportBtn);
            this.Controls.Add(errorLbl);
            this.Controls.Add(appointmentViewLbl);
            this.Controls.Add(customerViewLbl);
            this.Controls.Add(custApptDescriptionLbl);
            this.Controls.Add(appointmentRdBtn);
            this.Controls.Add(customerRdBtn);
            this.Controls.Add(deleteRecordBtn);
            this.Controls.Add(updateRecordBtn);
            this.Controls.Add(addRecordBtn);
            this.Controls.Add(customerDataGridView);
            this.Controls.Add(appoinmentDataGridView);
            this.Name = "AppointmentControl";
            this.Size = new Size(1381, 726);
            this.Load += AppointmentControl_Load;
            ((System.ComponentModel.ISupportInitialize)customerDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)appStateBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)appoinmentDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView customerDataGridView;
        private BindingSource appStateBindingSource;
        private DataGridView appoinmentDataGridView;
        private Button addRecordBtn;
        private Button updateRecordBtn;
        private Button deleteRecordBtn;
        private RadioButton customerRdBtn;
        private RadioButton appointmentRdBtn;
        private Label custApptDescriptionLbl;
        private Label customerViewLbl;
        private Label appointmentViewLbl;
        private Label errorLbl;
        private Button reportBtn;
        private Label OfficeHoursLbl;
        private Label offficeHoursLocalTimeLbl;
        private Label timeZoneLbl;
        private Label userNameLbl;
        private LinkLabel signOutLinkLbl;
        private Scheduling_UI_Library.DatabaseStatusPanel databaseStatusPanel;
    }
}

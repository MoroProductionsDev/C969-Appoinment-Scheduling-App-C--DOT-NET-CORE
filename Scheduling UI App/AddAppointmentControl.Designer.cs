namespace Scheduling_UI_App
{
    partial class AddAppointmentControl
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
            customerNameLbl = new Label();
            appointmentTitleLbl = new Label();
            appointmentTitleTxtBox = new TextBox();
            appointmentLocationLbl = new Label();
            appointmentLocationTxtBox = new TextBox();
            appointmentContactLbl = new Label();
            appointmentContactTxtBox = new TextBox();
            appointmentTypeLbl = new Label();
            appointmentTypeTxtBox = new TextBox();
            this.appStartDateScheduleDateTimePickerControl = new Scheduling_UI_Library.ScheduleDateTimePickerControl();
            appointmentUrlLb = new Label();
            appointmentUrlTxtBox = new TextBox();
            customerNameComboBox = new ComboBox();
            appointmentDescriptionRichTxtBox = new RichTextBox();
            appointmentDescriptionLbl = new Label();
            this.appEndDateScheduleDateTimePickerControl = new Scheduling_UI_Library.ScheduleDateTimePickerControl();
            returnBtn = new Button();
            addBtn = new Button();
            startDateTimeLbl = new Label();
            endDateTimeLbl = new Label();
            controlNameLbl = new Label();
            requiredMsgLbl = new Label();
            errorLbl = new Label();
            this.databaseStatusPanel = new Scheduling_UI_Library.DatabaseStatusPanel();
            SuspendLayout();
            // 
            // customerNameLbl
            // 
            customerNameLbl.AutoSize = true;
            customerNameLbl.Location = new Point(28, 66);
            customerNameLbl.Name = "customerNameLbl";
            customerNameLbl.Size = new Size(47, 15);
            customerNameLbl.TabIndex = 2;
            customerNameLbl.Text = "* Name";
            // 
            // appointmentTitleLbl
            // 
            appointmentTitleLbl.AutoSize = true;
            appointmentTitleLbl.Location = new Point(28, 136);
            appointmentTitleLbl.Name = "appointmentTitleLbl";
            appointmentTitleLbl.Size = new Size(29, 15);
            appointmentTitleLbl.TabIndex = 4;
            appointmentTitleLbl.Text = "Title";
            // 
            // appointmentTitleTxtBox
            // 
            appointmentTitleTxtBox.Location = new Point(28, 165);
            appointmentTitleTxtBox.Name = "appointmentTitleTxtBox";
            appointmentTitleTxtBox.Size = new Size(128, 23);
            appointmentTitleTxtBox.TabIndex = 3;
            // 
            // appointmentLocationLbl
            // 
            appointmentLocationLbl.AutoSize = true;
            appointmentLocationLbl.Location = new Point(356, 136);
            appointmentLocationLbl.Name = "appointmentLocationLbl";
            appointmentLocationLbl.Size = new Size(53, 15);
            appointmentLocationLbl.TabIndex = 6;
            appointmentLocationLbl.Text = "Location";
            // 
            // appointmentLocationTxtBox
            // 
            appointmentLocationTxtBox.Location = new Point(356, 165);
            appointmentLocationTxtBox.Name = "appointmentLocationTxtBox";
            appointmentLocationTxtBox.Size = new Size(128, 23);
            appointmentLocationTxtBox.TabIndex = 5;
            // 
            // appointmentContactLbl
            // 
            appointmentContactLbl.AutoSize = true;
            appointmentContactLbl.Location = new Point(28, 203);
            appointmentContactLbl.Name = "appointmentContactLbl";
            appointmentContactLbl.Size = new Size(49, 15);
            appointmentContactLbl.TabIndex = 8;
            appointmentContactLbl.Text = "Contact";
            // 
            // appointmentContactTxtBox
            // 
            appointmentContactTxtBox.Location = new Point(28, 232);
            appointmentContactTxtBox.Name = "appointmentContactTxtBox";
            appointmentContactTxtBox.Size = new Size(128, 23);
            appointmentContactTxtBox.TabIndex = 7;
            // 
            // appointmentTypeLbl
            // 
            appointmentTypeLbl.AutoSize = true;
            appointmentTypeLbl.Location = new Point(191, 136);
            appointmentTypeLbl.Name = "appointmentTypeLbl";
            appointmentTypeLbl.Size = new Size(31, 15);
            appointmentTypeLbl.TabIndex = 10;
            appointmentTypeLbl.Text = "Type";
            // 
            // appointmentTypeTxtBox
            // 
            appointmentTypeTxtBox.Location = new Point(191, 165);
            appointmentTypeTxtBox.Name = "appointmentTypeTxtBox";
            appointmentTypeTxtBox.Size = new Size(128, 23);
            appointmentTypeTxtBox.TabIndex = 9;
            // 
            // appStartDateScheduleDateTimePickerControl
            // 
            this.appStartDateScheduleDateTimePickerControl.Location = new Point(624, 97);
            this.appStartDateScheduleDateTimePickerControl.Name = "appStartDateScheduleDateTimePickerControl";
            this.appStartDateScheduleDateTimePickerControl.Size = new Size(425, 121);
            this.appStartDateScheduleDateTimePickerControl.TabIndex = 0;
            // 
            // appointmentUrlLb
            // 
            appointmentUrlLb.AutoSize = true;
            appointmentUrlLb.Location = new Point(191, 203);
            appointmentUrlLb.Name = "appointmentUrlLb";
            appointmentUrlLb.Size = new Size(28, 15);
            appointmentUrlLb.TabIndex = 12;
            appointmentUrlLb.Text = "URL";
            // 
            // appointmentUrlTxtBox
            // 
            appointmentUrlTxtBox.Location = new Point(191, 232);
            appointmentUrlTxtBox.Name = "appointmentUrlTxtBox";
            appointmentUrlTxtBox.Size = new Size(128, 23);
            appointmentUrlTxtBox.TabIndex = 11;
            // 
            // customerNameComboBox
            // 
            customerNameComboBox.FormattingEnabled = true;
            customerNameComboBox.Location = new Point(28, 97);
            customerNameComboBox.Name = "customerNameComboBox";
            customerNameComboBox.Size = new Size(128, 23);
            customerNameComboBox.TabIndex = 13;
            // 
            // appointmentDescriptionRichTxtBox
            // 
            appointmentDescriptionRichTxtBox.Location = new Point(356, 232);
            appointmentDescriptionRichTxtBox.Name = "appointmentDescriptionRichTxtBox";
            appointmentDescriptionRichTxtBox.Size = new Size(235, 90);
            appointmentDescriptionRichTxtBox.TabIndex = 14;
            appointmentDescriptionRichTxtBox.Text = "";
            // 
            // appointmentDescriptionLbl
            // 
            appointmentDescriptionLbl.AutoSize = true;
            appointmentDescriptionLbl.Location = new Point(356, 203);
            appointmentDescriptionLbl.Name = "appointmentDescriptionLbl";
            appointmentDescriptionLbl.Size = new Size(67, 15);
            appointmentDescriptionLbl.TabIndex = 15;
            appointmentDescriptionLbl.Text = "Description";
            // 
            // appEndDateScheduleDateTimePickerControl
            // 
            this.appEndDateScheduleDateTimePickerControl.Location = new Point(625, 270);
            this.appEndDateScheduleDateTimePickerControl.Name = "appEndDateScheduleDateTimePickerControl";
            this.appEndDateScheduleDateTimePickerControl.Size = new Size(413, 121);
            this.appEndDateScheduleDateTimePickerControl.TabIndex = 16;
            // 
            // returnBtn
            // 
            returnBtn.Location = new Point(28, 430);
            returnBtn.Name = "returnBtn";
            returnBtn.Size = new Size(75, 23);
            returnBtn.TabIndex = 17;
            returnBtn.Text = "Return";
            returnBtn.UseVisualStyleBackColor = true;
            // 
            // addBtn
            // 
            addBtn.Location = new Point(919, 430);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(75, 23);
            addBtn.TabIndex = 18;
            addBtn.Text = "Add";
            addBtn.UseVisualStyleBackColor = true;
            addBtn.Click += AddBtn_Click;
            addBtn.LostFocus += AddBtn_LostFocus;
            // 
            // startDateTimeLbl
            // 
            startDateTimeLbl.AutoSize = true;
            startDateTimeLbl.Location = new Point(625, 69);
            startDateTimeLbl.Name = "startDateTimeLbl";
            startDateTimeLbl.Size = new Size(111, 15);
            startDateTimeLbl.TabIndex = 19;
            startDateTimeLbl.Text = "* Start [Date / Time]";
            // 
            // endDateTimeLbl
            // 
            endDateTimeLbl.AutoSize = true;
            endDateTimeLbl.Location = new Point(624, 240);
            endDateTimeLbl.Name = "endDateTimeLbl";
            endDateTimeLbl.Size = new Size(107, 15);
            endDateTimeLbl.TabIndex = 20;
            endDateTimeLbl.Text = "* End [Date / Time]";
            // 
            // controlNameLbl
            // 
            controlNameLbl.AutoSize = true;
            controlNameLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            controlNameLbl.Location = new Point(28, 18);
            controlNameLbl.Name = "controlNameLbl";
            controlNameLbl.Size = new Size(103, 20);
            controlNameLbl.TabIndex = 21;
            controlNameLbl.Text = "Appointment";
            // 
            // requiredMsgLbl
            // 
            requiredMsgLbl.AutoSize = true;
            requiredMsgLbl.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            requiredMsgLbl.Location = new Point(909, 21);
            requiredMsgLbl.Name = "requiredMsgLbl";
            requiredMsgLbl.Size = new Size(85, 17);
            requiredMsgLbl.TabIndex = 22;
            requiredMsgLbl.Text = "* = Required";
            // 
            // errorLbl
            // 
            errorLbl.AutoSize = true;
            errorLbl.ForeColor = Color.Firebrick;
            errorLbl.Location = new Point(191, 434);
            errorLbl.MinimumSize = new Size(640, 0);
            errorLbl.Name = "errorLbl";
            errorLbl.Size = new Size(640, 15);
            errorLbl.TabIndex = 23;
            // 
            // databaseStatusPanel
            // 
            this.databaseStatusPanel.Location = new Point(427, 3);
            this.databaseStatusPanel.Name = "databaseStatusPanel";
            this.databaseStatusPanel.Size = new Size(242, 45);
            this.databaseStatusPanel.TabIndex = 24;
            // 
            // AddAppointmentControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.databaseStatusPanel);
            this.Controls.Add(errorLbl);
            this.Controls.Add(requiredMsgLbl);
            this.Controls.Add(controlNameLbl);
            this.Controls.Add(endDateTimeLbl);
            this.Controls.Add(startDateTimeLbl);
            this.Controls.Add(addBtn);
            this.Controls.Add(returnBtn);
            this.Controls.Add(this.appEndDateScheduleDateTimePickerControl);
            this.Controls.Add(appointmentDescriptionLbl);
            this.Controls.Add(appointmentDescriptionRichTxtBox);
            this.Controls.Add(customerNameComboBox);
            this.Controls.Add(appointmentUrlLb);
            this.Controls.Add(appointmentUrlTxtBox);
            this.Controls.Add(appointmentTypeLbl);
            this.Controls.Add(appointmentTypeTxtBox);
            this.Controls.Add(appointmentContactLbl);
            this.Controls.Add(appointmentContactTxtBox);
            this.Controls.Add(appointmentLocationLbl);
            this.Controls.Add(appointmentLocationTxtBox);
            this.Controls.Add(appointmentTitleLbl);
            this.Controls.Add(appointmentTitleTxtBox);
            this.Controls.Add(customerNameLbl);
            this.Controls.Add(this.appStartDateScheduleDateTimePickerControl);
            this.Name = "AddAppointmentControl";
            this.Size = new Size(1083, 504);
            this.Load += AddAppointmentControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label customerNameLbl;
        private Label appointmentTitleLbl;
        private TextBox appointmentTitleTxtBox;
        private Label appointmentLocationLbl;
        private TextBox appointmentLocationTxtBox;
        private Label appointmentContactLbl;
        private TextBox appointmentContactTxtBox;
        private Label appointmentTypeLbl;
        private TextBox appointmentTypeTxtBox;
        private Scheduling_UI_Library.ScheduleDateTimePickerControl appStartDateScheduleDateTimePickerControl;
        private Label appointmentUrlLb;
        private TextBox appointmentUrlTxtBox;
        private ComboBox customerNameComboBox;
        private RichTextBox appointmentDescriptionRichTxtBox;
        private Label appointmentDescriptionLbl;
        private Scheduling_UI_Library.ScheduleDateTimePickerControl appEndDateScheduleDateTimePickerControl;
        private Button returnBtn;
        private Button addBtn;
        private Label startDateTimeLbl;
        private Label endDateTimeLbl;
        private Label controlNameLbl;
        private Label requiredMsgLbl;
        private Label errorLbl;
        private Scheduling_UI_Library.DatabaseStatusPanel databaseStatusPanel;
    }
}

namespace Scheduling_UI_App
{
    partial class UpdateAppointmentControl
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
            currentValueLbl = new Label();
            newValueLbl = new Label();
            currentValueTxtBox = new TextBox();
            newValueTxtBox = new TextBox();
            updateBtn = new Button();
            columnComboBox = new ComboBox();
            columnLbl = new Label();
            returnBtn = new Button();
            controlNameLbl = new Label();
            this.scheduleDateTimePickerControl = new Scheduling_UI_Library.ScheduleDateTimePickerControl();
            this.scheduleDateTimePickerControl_ReadOnly = new Scheduling_UI_Library.ScheduleDateTimePickerControl_ReadOnly();
            errorLbl = new Label();
            customerNameComboBox = new ComboBox();
            newAppointmentDescriptionRichTxtBox = new RichTextBox();
            currentAppointmentDescriptionRichTxtBox = new RichTextBox();
            this.databaseStatusPanel = new Scheduling_UI_Library.DatabaseStatusPanel();
            SuspendLayout();
            // 
            // currentValueLbl
            // 
            currentValueLbl.AutoSize = true;
            currentValueLbl.Location = new Point(174, 66);
            currentValueLbl.Name = "currentValueLbl";
            currentValueLbl.Size = new Size(78, 15);
            currentValueLbl.TabIndex = 0;
            currentValueLbl.Text = "Current Value";
            // 
            // newValueLbl
            // 
            newValueLbl.AutoSize = true;
            newValueLbl.Location = new Point(323, 66);
            newValueLbl.Name = "newValueLbl";
            newValueLbl.Size = new Size(62, 15);
            newValueLbl.TabIndex = 1;
            newValueLbl.Text = "New Value";
            // 
            // currentValueTxtBox
            // 
            currentValueTxtBox.Location = new Point(174, 95);
            currentValueTxtBox.MaxLength = 250;
            currentValueTxtBox.Name = "currentValueTxtBox";
            currentValueTxtBox.ReadOnly = true;
            currentValueTxtBox.Size = new Size(121, 23);
            currentValueTxtBox.TabIndex = 2;
            // 
            // newValueTxtBox
            // 
            newValueTxtBox.Location = new Point(323, 95);
            newValueTxtBox.MaxLength = 250;
            newValueTxtBox.Name = "newValueTxtBox";
            newValueTxtBox.Size = new Size(121, 23);
            newValueTxtBox.TabIndex = 3;
            // 
            // updateBtn
            // 
            updateBtn.Location = new Point(369, 282);
            updateBtn.Name = "updateBtn";
            updateBtn.Size = new Size(75, 23);
            updateBtn.TabIndex = 4;
            updateBtn.Text = "Update";
            updateBtn.UseVisualStyleBackColor = true;
            updateBtn.Click += UpdateBtn_Click;
            updateBtn.LostFocus += UpdateBtn_LostFocus;
            // 
            // columnComboBox
            // 
            columnComboBox.FormattingEnabled = true;
            columnComboBox.Location = new Point(27, 95);
            columnComboBox.Name = "columnComboBox";
            columnComboBox.Size = new Size(121, 23);
            columnComboBox.TabIndex = 5;
            columnComboBox.SelectedValueChanged += ColumnComboBox_ValueChanged;
            // 
            // columnLbl
            // 
            columnLbl.AutoSize = true;
            columnLbl.Location = new Point(27, 66);
            columnLbl.Name = "columnLbl";
            columnLbl.Size = new Size(50, 15);
            columnLbl.TabIndex = 6;
            columnLbl.Text = "Column";
            // 
            // returnBtn
            // 
            returnBtn.Location = new Point(27, 282);
            returnBtn.Name = "returnBtn";
            returnBtn.Size = new Size(75, 23);
            returnBtn.TabIndex = 7;
            returnBtn.Text = "Return";
            returnBtn.UseVisualStyleBackColor = true;
            // 
            // controlNameLbl
            // 
            controlNameLbl.AutoSize = true;
            controlNameLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            controlNameLbl.Location = new Point(28, 19);
            controlNameLbl.Name = "controlNameLbl";
            controlNameLbl.Size = new Size(103, 20);
            controlNameLbl.TabIndex = 22;
            controlNameLbl.Text = "Appointment";
            // 
            // scheduleDateTimePickerControl
            // 
            this.scheduleDateTimePickerControl.Location = new Point(307, 59);
            this.scheduleDateTimePickerControl.Name = "scheduleDateTimePickerControl";
            this.scheduleDateTimePickerControl.Size = new Size(378, 177);
            this.scheduleDateTimePickerControl.TabIndex = 24;
            this.scheduleDateTimePickerControl.Visible = false;
            // 
            // scheduleDateTimePickerControl_ReadOnly
            // 
            this.scheduleDateTimePickerControl_ReadOnly.Location = new Point(158, 58);
            this.scheduleDateTimePickerControl_ReadOnly.Name = "scheduleDateTimePickerControl_ReadOnly";
            this.scheduleDateTimePickerControl_ReadOnly.Size = new Size(157, 147);
            this.scheduleDateTimePickerControl_ReadOnly.TabIndex = 23;
            this.scheduleDateTimePickerControl_ReadOnly.Visible = false;
            // 
            // errorLbl
            // 
            errorLbl.AutoSize = true;
            errorLbl.ForeColor = Color.Firebrick;
            errorLbl.Location = new Point(31, 239);
            errorLbl.MinimumSize = new Size(640, 0);
            errorLbl.Name = "errorLbl";
            errorLbl.Size = new Size(640, 15);
            errorLbl.TabIndex = 25;
            // 
            // customerNameComboBox
            // 
            customerNameComboBox.FormattingEnabled = true;
            customerNameComboBox.Location = new Point(323, 95);
            customerNameComboBox.Name = "customerNameComboBox";
            customerNameComboBox.Size = new Size(121, 23);
            customerNameComboBox.TabIndex = 26;
            // 
            // newAppointmentDescriptionRichTxtBox
            // 
            newAppointmentDescriptionRichTxtBox.Location = new Point(321, 95);
            newAppointmentDescriptionRichTxtBox.Name = "newAppointmentDescriptionRichTxtBox";
            newAppointmentDescriptionRichTxtBox.Size = new Size(121, 96);
            newAppointmentDescriptionRichTxtBox.TabIndex = 27;
            newAppointmentDescriptionRichTxtBox.Text = "";
            newAppointmentDescriptionRichTxtBox.Visible = false;
            // 
            // currentAppointmentDescriptionRichTxtBox
            // 
            currentAppointmentDescriptionRichTxtBox.Location = new Point(174, 95);
            currentAppointmentDescriptionRichTxtBox.Name = "currentAppointmentDescriptionRichTxtBox";
            currentAppointmentDescriptionRichTxtBox.Size = new Size(121, 96);
            currentAppointmentDescriptionRichTxtBox.TabIndex = 28;
            currentAppointmentDescriptionRichTxtBox.Text = "";
            currentAppointmentDescriptionRichTxtBox.Visible = false;
            // 
            // databaseStatusPanel
            // 
            this.databaseStatusPanel.Location = new Point(236, 7);
            this.databaseStatusPanel.Name = "databaseStatusPanel";
            this.databaseStatusPanel.Size = new Size(242, 45);
            this.databaseStatusPanel.TabIndex = 29;
            // 
            // UpdateAppointmentControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.databaseStatusPanel);
            this.Controls.Add(customerNameComboBox);
            this.Controls.Add(errorLbl);
            this.Controls.Add(controlNameLbl);
            this.Controls.Add(returnBtn);
            this.Controls.Add(columnLbl);
            this.Controls.Add(columnComboBox);
            this.Controls.Add(updateBtn);
            this.Controls.Add(newValueTxtBox);
            this.Controls.Add(currentValueTxtBox);
            this.Controls.Add(newValueLbl);
            this.Controls.Add(currentValueLbl);
            this.Controls.Add(this.scheduleDateTimePickerControl);
            this.Controls.Add(this.scheduleDateTimePickerControl_ReadOnly);
            this.Controls.Add(newAppointmentDescriptionRichTxtBox);
            this.Controls.Add(currentAppointmentDescriptionRichTxtBox);
            this.Name = "UpdateAppointmentControl";
            this.Size = new Size(718, 362);
            this.Load += UpdateAppointmentControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label currentValueLbl;
        private Label newValueLbl;
        private TextBox currentValueTxtBox;
        private TextBox newValueTxtBox;
        private Button updateBtn;
        private ComboBox columnComboBox;
        private Label columnLbl;
        private Button returnBtn;
        private Label controlNameLbl;
        private Scheduling_UI_Library.ScheduleDateTimePickerControl scheduleDateTimePickerControl;
        private Scheduling_UI_Library.ScheduleDateTimePickerControl_ReadOnly scheduleDateTimePickerControl_ReadOnly;
        private Label errorLbl;
        private ComboBox customerNameComboBox;
        private RichTextBox newAppointmentDescriptionRichTxtBox;
        private RichTextBox currentAppointmentDescriptionRichTxtBox;
        private Scheduling_UI_Library.DatabaseStatusPanel databaseStatusPanel;
    }
}
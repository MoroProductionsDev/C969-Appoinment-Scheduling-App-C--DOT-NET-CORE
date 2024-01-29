using Scheduling_API.Controller.State;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Scheduling_UI_Library
{
    partial class ScheduleDateTimePickerControl
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
            datePicker = new DateTimePicker();
            timePicker = new DateTimePicker();
            utcDateTxtBox = new TextBox();
            utcTimeTxtBox = new TextBox();
            utcDateLbl = new Label();
            utcTimeLbl = new Label();
            localDateLbl = new Label();
            localTimeLbl = new Label();
            SuspendLayout();
            // 
            // datePicker
            // 
            datePicker.Location = new Point(31, 36);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(200, 23);
            datePicker.TabIndex = 0;
            datePicker.Value = new DateTime(2024, 1, 25, 0, 0, 0, 0);
            datePicker.ValueChanged += DatePicker_ValueChanged;
            // 
            // timePicker
            // 
            timePicker.CustomFormat = "hh:mm tt";
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.Location = new Point(155, 88);
            timePicker.Name = "timePicker";
            timePicker.ShowUpDown = true;
            timePicker.Size = new Size(76, 23);
            timePicker.TabIndex = 2;
            timePicker.Value = new DateTime(2024, 1, 25, 8, 0, 0, 0);
            timePicker.ValueChanged += TimePicker_ValueChanged;
            // 
            // utcDateTxtBox
            // 
            utcDateTxtBox.Location = new Point(253, 36);
            utcDateTxtBox.MaxLength = 250;
            utcDateTxtBox.Name = "utcDateTxtBox";
            utcDateTxtBox.ReadOnly = true;
            utcDateTxtBox.Size = new Size(115, 23);
            utcDateTxtBox.TabIndex = 1;
            // 
            // utcTimeTxtBox
            // 
            utcTimeTxtBox.Location = new Point(253, 91);
            utcTimeTxtBox.MaxLength = 250;
            utcTimeTxtBox.Name = "utcTimeTxtBox";
            utcTimeTxtBox.ReadOnly = true;
            utcTimeTxtBox.Size = new Size(115, 23);
            utcTimeTxtBox.TabIndex = 4;
            // 
            // utcDateLbl
            // 
            utcDateLbl.AutoSize = true;
            utcDateLbl.Location = new Point(253, 9);
            utcDateLbl.Name = "utcDateLbl";
            utcDateLbl.Size = new Size(66, 15);
            utcDateLbl.TabIndex = 3;
            utcDateLbl.Text = "Date (UTC) ";
            // 
            // utcTimeLbl
            // 
            utcTimeLbl.AutoSize = true;
            utcTimeLbl.Location = new Point(253, 68);
            utcTimeLbl.Name = "utcTimeLbl";
            utcTimeLbl.Size = new Size(68, 15);
            utcTimeLbl.TabIndex = 4;
            utcTimeLbl.Text = "Time (UTC) ";
            // 
            // localDateLbl
            // 
            localDateLbl.AutoSize = true;
            localDateLbl.Location = new Point(155, 9);
            localDateLbl.Name = "localDateLbl";
            localDateLbl.Size = new Size(73, 15);
            localDateLbl.TabIndex = 5;
            localDateLbl.Text = "Date (Local) ";
            // 
            // localTimeLbl
            // 
            localTimeLbl.AutoSize = true;
            localTimeLbl.Location = new Point(155, 68);
            localTimeLbl.Name = "localTimeLbl";
            localTimeLbl.Size = new Size(75, 15);
            localTimeLbl.TabIndex = 6;
            localTimeLbl.Text = "Time (Local) ";
            // 
            // ScheduleDateTimePickerControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(localTimeLbl);
            this.Controls.Add(localDateLbl);
            this.Controls.Add(utcTimeLbl);
            this.Controls.Add(utcDateLbl);
            this.Controls.Add(utcTimeTxtBox);
            this.Controls.Add(utcDateTxtBox);
            this.Controls.Add(datePicker);
            this.Controls.Add(timePicker);
            this.Name = "ScheduleDateTimePickerControl";
            this.Size = new Size(405, 133);
            this.Load += ScheduleDateTimePickerControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker datePicker;
        private DateTimePicker timePicker;
        private TextBox utcDateTxtBox;
        private TextBox utcTimeTxtBox;
        private Label utcDateLbl;
        private Label utcTimeLbl;
        private Label localDateLbl;
        private Label localTimeLbl;
    }
}

using System.Windows.Forms;

namespace Scheduling_UI_Library
{
    partial class ScheduleDateTimePickerControl_ReadOnly
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
            localDateTxtBox = new TextBox();
            localTimeTxtBox = new TextBox();
            localDateLbl = new Label();
            localTimeLbl = new Label();
            SuspendLayout();
            // 
            // localDateTxtBox
            // 
            localDateTxtBox.Location = new Point(23, 37);
            localDateTxtBox.MaxLength = 250;
            localDateTxtBox.Name = "localDateTxtBox";
            localDateTxtBox.ReadOnly = true;
            localDateTxtBox.Size = new Size(115, 23);
            localDateTxtBox.TabIndex = 1;
            // 
            // locaTimeTxtBox
            // 
            localTimeTxtBox.Location = new Point(23, 92);
            localTimeTxtBox.MaxLength = 250;
            localTimeTxtBox.Name = "localTimeTxtBox";
            localTimeTxtBox.ReadOnly = true;
            localTimeTxtBox.Size = new Size(115, 23);
            localTimeTxtBox.TabIndex = 2;
            // 
            // localDateLbl
            // 
            localDateLbl.AutoSize = true;
            localDateLbl.Location = new Point(23, 10);
            localDateLbl.Name = "localDateLbl";
            localDateLbl.Size = new Size(31, 15);
            localDateLbl.TabIndex = 3;
            localDateLbl.Text = "Date";
            // 
            // localTimeLbl
            // 
            localTimeLbl.AutoSize = true;
            localTimeLbl.Location = new Point(23, 69);
            localTimeLbl.Name = "localTimeLbl";
            localTimeLbl.Size = new Size(33, 15);
            localTimeLbl.TabIndex = 4;
            localTimeLbl.Text = "Time";
            // 
            // ScheduleDateTimePickerControl_ReadOnly
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(localTimeLbl);
            this.Controls.Add(localDateLbl);
            this.Controls.Add(localTimeTxtBox);
            this.Controls.Add(localDateTxtBox);
            this.Name = "ScheduleDateTimePickerControl_ReadOnly";
            this.Size = new Size(165, 151);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox localDateTxtBox;
        private TextBox localTimeTxtBox;
        private Label localDateLbl;
        private Label localTimeLbl;
    }
}

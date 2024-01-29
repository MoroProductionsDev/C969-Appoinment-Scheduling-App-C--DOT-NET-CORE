using System.Windows.Forms;

namespace Scheduling_UI_App
{
    partial class ReportControl
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
            reportDataGridView = new DataGridView();
            reportComboBox = new ComboBox();
            reportNameLbl = new Label();
            returnBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)reportDataGridView).BeginInit();
            SuspendLayout();
            // 
            // reportDataGridView
            // 
            reportDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            reportDataGridView.Location = new Point(206, 58);
            reportDataGridView.Name = "reportDataGridView";
            reportDataGridView.RowTemplate.Height = 25;
            reportDataGridView.Size = new Size(684, 409);
            reportDataGridView.TabIndex = 0;
            reportDataGridView.AllowUserToAddRows = false;
            reportDataGridView.AllowUserToDeleteRows = false;
            reportDataGridView.ReadOnly = true;
            // 
            // reportComboBox
            // 
            reportComboBox.FormattingEnabled = true;
            reportComboBox.Location = new Point(22, 58);
            reportComboBox.Name = "reportComboBox";
            reportComboBox.Size = new Size(160, 23);
            reportComboBox.TabIndex = 1;
            reportComboBox.SelectedValueChanged += ReportComboBox_ValueChanged;
            // 
            // reportNameLbl
            // 
            reportNameLbl.AutoSize = true;
            reportNameLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            reportNameLbl.Location = new Point(22, 16);
            reportNameLbl.Name = "reportNameLbl";
            reportNameLbl.Size = new Size(57, 20);
            reportNameLbl.TabIndex = 23;
            reportNameLbl.Text = "Report";
            // 
            // returnBtn
            // 
            returnBtn.Location = new Point(22, 496);
            returnBtn.Name = "returnBtn";
            returnBtn.Size = new Size(75, 23);
            returnBtn.TabIndex = 24;
            returnBtn.Text = "Return";
            returnBtn.UseVisualStyleBackColor = true;
            // 
            // ReportControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(returnBtn);
            this.Controls.Add(reportNameLbl);
            this.Controls.Add(reportComboBox);
            this.Controls.Add(reportDataGridView);
            this.Name = "ReportControl";
            this.Size = new Size(919, 541);
            this.Load += ReportControl_Load;
            ((System.ComponentModel.ISupportInitialize)reportDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView reportDataGridView;
        private ComboBox reportComboBox;
        private Label reportNameLbl;
        private Button returnBtn;
    }
}

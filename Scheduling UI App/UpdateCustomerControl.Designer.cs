namespace Scheduling_UI_App
{
    partial class UpdateCustomerControl
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
            this.databaseStatusPanel = new Scheduling_UI_Library.DatabaseStatusPanel();
            SuspendLayout();
            // 
            // currentValueLbl
            // 
            currentValueLbl.AutoSize = true;
            currentValueLbl.Location = new Point(174, 65);
            currentValueLbl.Name = "currentValueLbl";
            currentValueLbl.Size = new Size(57, 15);
            currentValueLbl.TabIndex = 0;
            currentValueLbl.Text = "Current Value";
            // 
            // newValueLbl
            // 
            newValueLbl.AutoSize = true;
            newValueLbl.Location = new Point(323, 65);
            newValueLbl.Name = "newValueLbl";
            newValueLbl.Size = new Size(62, 15);
            newValueLbl.TabIndex = 1;
            newValueLbl.Text = "New Value";
            // 
            // currentValueTxtBox
            // 
            currentValueTxtBox.Location = new Point(174, 94);
            currentValueTxtBox.MaxLength = 250;
            currentValueTxtBox.Name = "currentValueTxtBox";
            currentValueTxtBox.ReadOnly = true;
            currentValueTxtBox.Size = new Size(121, 23);
            currentValueTxtBox.TabIndex = 2;
            // 
            // newValueTxtBox
            // 
            newValueTxtBox.Location = new Point(323, 94);
            newValueTxtBox.MaxLength = 250;
            newValueTxtBox.Name = "newValueTxtBox";
            newValueTxtBox.Size = new Size(121, 23);
            newValueTxtBox.TabIndex = 3;
            newValueTxtBox.GotFocus += OnTxtBoxFocus;
            newValueTxtBox.LostFocus += OnTxtBoxLostFocus;
            // 
            // updateBtn
            // 
            updateBtn.Location = new Point(371, 282);
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
            columnComboBox.Location = new Point(27, 94);
            columnComboBox.Name = "columnComboBox";
            columnComboBox.Size = new Size(121, 23);
            columnComboBox.TabIndex = 5;
            columnComboBox.SelectedValueChanged += ColumnComboBox_ValueChanged;
            // 
            // columnLbl
            // 
            columnLbl.AutoSize = true;
            columnLbl.Location = new Point(27, 65);
            columnLbl.Name = "columnLbl";
            columnLbl.Size = new Size(50, 15);
            columnLbl.TabIndex = 6;
            columnLbl.Text = "Column";
            // 
            // returnBtn
            // 
            returnBtn.Location = new Point(29, 282);
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
            controlNameLbl.Location = new Point(27, 19);
            controlNameLbl.Name = "controlNameLbl";
            controlNameLbl.Size = new Size(77, 20);
            controlNameLbl.TabIndex = 23;
            controlNameLbl.Text = "Customer";
            // 
            // databaseStatusPanel
            // 
            this.databaseStatusPanel.Location = new Point(137, 10);
            this.databaseStatusPanel.Name = "databaseStatusPanel";
            this.databaseStatusPanel.Size = new Size(201, 45);
            this.databaseStatusPanel.TabIndex = 24;
            // 
            // UpdateCustomerControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.databaseStatusPanel);
            this.Controls.Add(controlNameLbl);
            this.Controls.Add(returnBtn);
            this.Controls.Add(columnLbl);
            this.Controls.Add(columnComboBox);
            this.Controls.Add(updateBtn);
            this.Controls.Add(newValueTxtBox);
            this.Controls.Add(currentValueTxtBox);
            this.Controls.Add(newValueLbl);
            this.Controls.Add(currentValueLbl);
            this.Name = "UpdateCustomerControl";
            this.Size = new Size(482, 330);
            this.Load += UpdateCustomerControl_Load;
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
        private Scheduling_UI_Library.DatabaseStatusPanel databaseStatusPanel;
    }
}

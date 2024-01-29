namespace Scheduling_UI_App
{
    partial class AddCustomerControl
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
            customerNameTxt = new TextBox();
            customerNameLbl = new Label();
            customerPhoneLbl = new Label();
            customerPhoneTxtBox = new TextBox();
            customerAddressLbl = new Label();
            customerAddressTxtBox = new TextBox();
            customerCityLbl = new Label();
            customerCityTxtBox = new TextBox();
            customerCountryLbl = new Label();
            customerCountryTxtBox = new TextBox();
            controlNameLbl = new Label();
            addBtn = new Button();
            returnBtn = new Button();
            address2Lbl = new Label();
            customerAddress2TxtBox = new TextBox();
            requiredMsgLbl = new Label();
            errorLbl = new Label();
            this.databaseStatusPanel = new Scheduling_UI_Library.DatabaseStatusPanel();
            SuspendLayout();
            // 
            // customerNameTxt
            // 
            customerNameTxt.Location = new Point(29, 135);
            customerNameTxt.Name = "customerNameTxt";
            customerNameTxt.Size = new Size(128, 23);
            customerNameTxt.TabIndex = 3;
            customerNameTxt.GotFocus += OnTxtBoxFocus;
            customerNameTxt.LostFocus += OnTxtBoxLostFocus;
            // 
            // customerNameLbl
            // 
            customerNameLbl.AutoSize = true;
            customerNameLbl.Location = new Point(29, 106);
            customerNameLbl.Name = "customerNameLbl";
            customerNameLbl.Size = new Size(47, 15);
            customerNameLbl.TabIndex = 2;
            customerNameLbl.Text = "* Name";
            // 
            // customerPhoneLbl
            // 
            customerPhoneLbl.AutoSize = true;
            customerPhoneLbl.Location = new Point(180, 106);
            customerPhoneLbl.Name = "customerPhoneLbl";
            customerPhoneLbl.Size = new Size(49, 15);
            customerPhoneLbl.TabIndex = 4;
            customerPhoneLbl.Text = "* Phone";
            // 
            // customerPhoneTxtBox
            // 
            customerPhoneTxtBox.Location = new Point(180, 135);
            customerPhoneTxtBox.Name = "customerPhoneTxtBox";
            customerPhoneTxtBox.Size = new Size(128, 23);
            customerPhoneTxtBox.TabIndex = 5;
            customerPhoneTxtBox.GotFocus += OnTxtBoxFocus;
            customerPhoneTxtBox.LostFocus += OnTxtBoxLostFocus;
            // 
            // customerAddressLbl
            // 
            customerAddressLbl.AutoSize = true;
            customerAddressLbl.Location = new Point(29, 172);
            customerAddressLbl.Name = "customerAddressLbl";
            customerAddressLbl.Size = new Size(57, 15);
            customerAddressLbl.TabIndex = 6;
            customerAddressLbl.Text = "* Address";
            // 
            // customerAddressTxtBox
            // 
            customerAddressTxtBox.Location = new Point(29, 201);
            customerAddressTxtBox.Name = "customerAddressTxtBox";
            customerAddressTxtBox.Size = new Size(279, 23);
            customerAddressTxtBox.TabIndex = 7;
            customerAddressTxtBox.GotFocus += OnTxtBoxFocus;
            customerAddressTxtBox.LostFocus += OnTxtBoxLostFocus;
            // 
            // customerCityLbl
            // 
            customerCityLbl.AutoSize = true;
            customerCityLbl.Location = new Point(29, 310);
            customerCityLbl.Name = "customerCityLbl";
            customerCityLbl.Size = new Size(36, 15);
            customerCityLbl.TabIndex = 10;
            customerCityLbl.Text = "* City";
            // 
            // customerCityTxtBox
            // 
            customerCityTxtBox.Location = new Point(29, 339);
            customerCityTxtBox.Name = "customerCityTxtBox";
            customerCityTxtBox.Size = new Size(128, 23);
            customerCityTxtBox.TabIndex = 11;
            customerCityTxtBox.GotFocus += OnTxtBoxFocus;
            customerCityTxtBox.LostFocus += OnTxtBoxLostFocus;
            // 
            // customerCountryLbl
            // 
            customerCountryLbl.AutoSize = true;
            customerCountryLbl.Location = new Point(180, 310);
            customerCountryLbl.Name = "customerCountryLbl";
            customerCountryLbl.Size = new Size(58, 15);
            customerCountryLbl.TabIndex = 12;
            customerCountryLbl.Text = "* Country";
            // 
            // customerCountryTxtBox
            // 
            customerCountryTxtBox.Location = new Point(180, 339);
            customerCountryTxtBox.Name = "customerCountryTxtBox";
            customerCountryTxtBox.Size = new Size(128, 23);
            customerCountryTxtBox.TabIndex = 13;
            customerCountryTxtBox.GotFocus += OnTxtBoxFocus;
            customerCountryTxtBox.LostFocus += OnTxtBoxLostFocus;
            // 
            // controlNameLbl
            // 
            controlNameLbl.AutoSize = true;
            controlNameLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            controlNameLbl.Location = new Point(29, 60);
            controlNameLbl.Name = "controlNameLbl";
            controlNameLbl.Size = new Size(77, 20);
            controlNameLbl.TabIndex = 0;
            controlNameLbl.Text = "Customer";
            // 
            // addBtn
            // 
            addBtn.Location = new Point(233, 402);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(75, 23);
            addBtn.TabIndex = 14;
            addBtn.Text = "Add";
            addBtn.UseVisualStyleBackColor = true;
            addBtn.Click += AddBtn_Click;
            addBtn.LostFocus += UpdateBtn_LostFocus;
            // 
            // returnBtn
            // 
            returnBtn.Location = new Point(29, 402);
            returnBtn.Name = "returnBtn";
            returnBtn.Size = new Size(75, 23);
            returnBtn.TabIndex = 15;
            returnBtn.Text = "Return";
            returnBtn.UseVisualStyleBackColor = true;
            // 
            // address2Lbl
            // 
            address2Lbl.AutoSize = true;
            address2Lbl.Location = new Point(29, 240);
            address2Lbl.Name = "address2Lbl";
            address2Lbl.Size = new Size(58, 15);
            address2Lbl.TabIndex = 8;
            address2Lbl.Text = "Address 2";
            // 
            // customerAddress2TxtBox
            // 
            customerAddress2TxtBox.Location = new Point(29, 269);
            customerAddress2TxtBox.Name = "customerAddress2TxtBox";
            customerAddress2TxtBox.Size = new Size(279, 23);
            customerAddress2TxtBox.TabIndex = 9;
            // 
            // requiredMsgLbl
            // 
            requiredMsgLbl.AutoSize = true;
            requiredMsgLbl.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            requiredMsgLbl.Location = new Point(223, 63);
            requiredMsgLbl.Name = "requiredMsgLbl";
            requiredMsgLbl.Size = new Size(85, 17);
            requiredMsgLbl.TabIndex = 1;
            requiredMsgLbl.Text = "* = Required";
            // 
            // errorLbl
            // 
            errorLbl.AutoSize = true;
            errorLbl.ForeColor = Color.Firebrick;
            errorLbl.Location = new Point(29, 452);
            errorLbl.MinimumSize = new Size(280, 0);
            errorLbl.Name = "errorLbl";
            errorLbl.Size = new Size(280, 15);
            errorLbl.TabIndex = 16;
            // 
            // databaseStatusPanel
            // 
            this.databaseStatusPanel.Location = new Point(47, 8);
            this.databaseStatusPanel.Name = "databaseStatusPanel";
            this.databaseStatusPanel.Size = new Size(242, 45);
            this.databaseStatusPanel.TabIndex = 17;
            // 
            // AddCustomerControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.databaseStatusPanel);
            this.Controls.Add(errorLbl);
            this.Controls.Add(requiredMsgLbl);
            this.Controls.Add(address2Lbl);
            this.Controls.Add(customerAddress2TxtBox);
            this.Controls.Add(returnBtn);
            this.Controls.Add(addBtn);
            this.Controls.Add(controlNameLbl);
            this.Controls.Add(customerCountryLbl);
            this.Controls.Add(customerCountryTxtBox);
            this.Controls.Add(customerCityLbl);
            this.Controls.Add(customerCityTxtBox);
            this.Controls.Add(customerAddressLbl);
            this.Controls.Add(customerAddressTxtBox);
            this.Controls.Add(customerPhoneLbl);
            this.Controls.Add(customerPhoneTxtBox);
            this.Controls.Add(customerNameLbl);
            this.Controls.Add(customerNameTxt);
            this.MinimumSize = new Size(270, 0);
            this.Name = "AddCustomerControl";
            this.Size = new Size(340, 478);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox customerNameTxt;
        private Label customerNameLbl;
        private Label customerPhoneLbl;
        private TextBox customerPhoneTxtBox;
        private Label customerAddressLbl;
        private TextBox customerAddressTxtBox;
        private Label customerCityLbl;
        private TextBox customerCityTxtBox;
        private Label customerCountryLbl;
        private TextBox customerCountryTxtBox;
        private Label controlNameLbl;
        private Button addBtn;
        private Button returnBtn;
        private Label address2Lbl;
        private TextBox customerAddress2TxtBox;
        private Label requiredMsgLbl;
        private Label errorLbl;
        private Scheduling_UI_Library.DatabaseStatusPanel databaseStatusPanel;
    }
}

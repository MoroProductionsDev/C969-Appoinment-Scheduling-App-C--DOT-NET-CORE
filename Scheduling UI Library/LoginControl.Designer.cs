namespace Scheduling_UI_Library
{
    partial class LoginControl
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
            userNameLbl = new Label();
            userNameTxtBox = new TextBox();
            passwordLbl = new Label();
            paswordTxtBox = new TextBox();
            signInBtn = new Button();
            SuspendLayout();
            // 
            // userNameLbl
            // 
            userNameLbl.AutoSize = true;
            userNameLbl.Location = new Point(41, 13);
            userNameLbl.Name = "userNameLbl";
            userNameLbl.Size = new Size(30, 15);
            userNameLbl.TabIndex = 0;
            userNameLbl.Text = "User";
            // 
            // userNameTxtBox
            // 
            userNameTxtBox.Location = new Point(7, 31);
            userNameTxtBox.MaxLength = 36;
            userNameTxtBox.Name = "userNameTxtBox";
            userNameTxtBox.Size = new Size(100, 23);
            userNameTxtBox.TabIndex = 1;
            userNameTxtBox.LostFocus += OnTxtBoxLostFocus;
            // 
            // passwordLbl
            // 
            passwordLbl.AutoSize = true;
            passwordLbl.Location = new Point(28, 63);
            passwordLbl.Name = "passwordLbl";
            passwordLbl.Size = new Size(57, 15);
            passwordLbl.TabIndex = 2;
            passwordLbl.Text = "Password";
            // 
            // paswordTxtBox
            // 
            paswordTxtBox.Location = new Point(7, 81);
            paswordTxtBox.MaxLength = 254;
            paswordTxtBox.Name = "paswordTxtBox";
            paswordTxtBox.Size = new Size(100, 23);
            paswordTxtBox.TabIndex = 3;
            // 
            // signInBtn
            // 
            signInBtn.Location = new Point(30, 110);
            signInBtn.Name = "signInBtn";
            signInBtn.Size = new Size(52, 23);
            signInBtn.TabIndex = 4;
            signInBtn.Text = "Sign In";
            signInBtn.UseVisualStyleBackColor = true;
            // 
            // LoginControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(signInBtn);
            this.Controls.Add(paswordTxtBox);
            this.Controls.Add(passwordLbl);
            this.Controls.Add(userNameTxtBox);
            this.Controls.Add(userNameLbl);
            this.Name = "LoginControl";
            this.Size = new Size(116, 159);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label userNameLbl;
        private TextBox userNameTxtBox;
        private Label passwordLbl;
        private TextBox paswordTxtBox;
        private Button signInBtn;
    }
}
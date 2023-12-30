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
            this.components = new System.ComponentModel.Container();
            userNameLbl = new Label();
            userNameTxtBox = new TextBox();
            passwordLbl = new Label();
            passwordTxtBox = new TextBox();
            signInBtn = new Button();
            userNameToolTip = new ToolTip(this.components);
            passwordToolTip = new ToolTip(this.components);
            SuspendLayout();
            // 
            // userNameLbl
            // 
            userNameLbl.AutoSize = true;
            userNameLbl.Location = new Point(156, 38);
            userNameLbl.MinimumSize = new Size(57, 15);
            userNameLbl.Name = "userNameLbl";
            userNameLbl.Size = new Size(57, 15);
            userNameLbl.TabIndex = 0;
            userNameLbl.Text = "User";
            userNameLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // userNameTxtBox
            // 
            userNameTxtBox.Location = new Point(134, 56);
            userNameTxtBox.MaxLength = 36;
            userNameTxtBox.Name = "userNameTxtBox";
            userNameTxtBox.Size = new Size(100, 23);
            userNameTxtBox.TabIndex = 1;
            userNameTxtBox.LostFocus += OnTxtBoxLostFocus;
            // 
            // passwordLbl
            // 
            passwordLbl.AutoSize = true;
            passwordLbl.Location = new Point(156, 89);
            passwordLbl.Name = "passwordLbl";
            passwordLbl.Size = new Size(57, 15);
            passwordLbl.TabIndex = 2;
            passwordLbl.Text = "Password";
            passwordLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // passwordTxtBox
            // 
            passwordTxtBox.Location = new Point(134, 107);
            passwordTxtBox.MaxLength = 254;
            passwordTxtBox.Name = "passwordTxtBox";
            passwordTxtBox.PasswordChar = '•';
            passwordTxtBox.Size = new Size(100, 23);
            passwordTxtBox.TabIndex = 3;
            passwordTxtBox.LostFocus += OnTxtBoxLostFocus;
            // 
            // signInBtn
            // 
            signInBtn.Location = new Point(154, 141);
            signInBtn.Name = "signInBtn";
            signInBtn.Size = new Size(60, 25);
            signInBtn.TabIndex = 4;
            signInBtn.Text = "Sign In";
            signInBtn.UseVisualStyleBackColor = true;
            signInBtn.Click += signInBtn_Click;
            // 
            // userNameToolTip
            // 
            userNameToolTip.ToolTipIcon = ToolTipIcon.Warning;
            // 
            // passwordToolTip
            // 
            passwordToolTip.ToolTipIcon = ToolTipIcon.Warning;
            // 
            // LoginControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(signInBtn);
            this.Controls.Add(passwordTxtBox);
            this.Controls.Add(passwordLbl);
            this.Controls.Add(userNameTxtBox);
            this.Controls.Add(userNameLbl);
            this.Name = "LoginControl";
            this.Size = new Size(382, 215);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label userNameLbl;
        private TextBox userNameTxtBox;
        private Label passwordLbl;
        private TextBox passwordTxtBox;
        private Button signInBtn;
        private ToolTip userNameToolTip;
        private ToolTip passwordToolTip;
    }
}

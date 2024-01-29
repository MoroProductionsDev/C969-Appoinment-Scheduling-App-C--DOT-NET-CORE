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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginControl));
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
            resources.ApplyResources(userNameLbl, "userNameLbl");
            userNameLbl.Name = "userNameLbl";
            userNameToolTip.SetToolTip(userNameLbl, resources.GetString("userNameLbl.ToolTip"));
            passwordToolTip.SetToolTip(userNameLbl, resources.GetString("userNameLbl.ToolTip1"));
            // 
            // userNameTxtBox
            // 
            resources.ApplyResources(userNameTxtBox, "userNameTxtBox");
            userNameTxtBox.Name = "userNameTxtBox";
            userNameToolTip.SetToolTip(userNameTxtBox, resources.GetString("userNameTxtBox.ToolTip"));
            passwordToolTip.SetToolTip(userNameTxtBox, resources.GetString("userNameTxtBox.ToolTip1"));
            userNameTxtBox.LostFocus += OnTxtBoxLostFocus;
            // 
            // passwordLbl
            // 
            resources.ApplyResources(passwordLbl, "passwordLbl");
            passwordLbl.Name = "passwordLbl";
            userNameToolTip.SetToolTip(passwordLbl, resources.GetString("passwordLbl.ToolTip"));
            passwordToolTip.SetToolTip(passwordLbl, resources.GetString("passwordLbl.ToolTip1"));
            // 
            // passwordTxtBox
            // 
            resources.ApplyResources(passwordTxtBox, "passwordTxtBox");
            passwordTxtBox.Name = "passwordTxtBox";
            userNameToolTip.SetToolTip(passwordTxtBox, resources.GetString("passwordTxtBox.ToolTip"));
            passwordToolTip.SetToolTip(passwordTxtBox, resources.GetString("passwordTxtBox.ToolTip1"));
            passwordTxtBox.LostFocus += OnTxtBoxLostFocus;
            // 
            // signInBtn
            // 
            resources.ApplyResources(signInBtn, "signInBtn");
            signInBtn.Name = "signInBtn";
            userNameToolTip.SetToolTip(signInBtn, resources.GetString("signInBtn.ToolTip"));
            passwordToolTip.SetToolTip(signInBtn, resources.GetString("signInBtn.ToolTip1"));
            signInBtn.UseVisualStyleBackColor = true;
            signInBtn.Click += SignInBtn_Click;
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
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(signInBtn);
            this.Controls.Add(passwordTxtBox);
            this.Controls.Add(passwordLbl);
            this.Controls.Add(userNameTxtBox);
            this.Controls.Add(userNameLbl);
            this.Name = "LoginControl";
            userNameToolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            passwordToolTip.SetToolTip(this, resources.GetString("$this.ToolTip1"));
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

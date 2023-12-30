using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scheduling_UI_Library.UI_Validator;
using Scheduling_API.Controller;
using Scheduling_API.Controller.State;
using System.Net.NetworkInformation;


namespace Scheduling_UI_Library
{
    public partial class LoginControl : UserControl
    {
        public const string ControlName = nameof(LoginControl);

        const int ToolTipTimeDisplayed = 5000;
        private AppState? _appState;

        public LoginControl()
        {
            // 1) Create Components
            InitializeComponent();
        }

        public AppState? AppState
        {
            set
            {
                if (value is not null && _appState is null)
                {
                    this._appState = value;
                }
            }
            get
            {
                return _appState;
            }
        }

        // https://stackoverflow.com/questions/42535141/winform-tooltip-location-setting
        protected void OnTxtBoxLostFocus(object sender, EventArgs args)
        {
            TextBox txtBox = (TextBox)sender;
            string errorMsg = LoginControlValidator.ValidateUserNameTxtBox(txtBox);

            if (txtBox.Name.Equals(userNameTxtBox.Name))
            {
                userNameToolTip.Show(errorMsg, txtBox, new Point(105), ToolTipTimeDisplayed);
            }
            else if (txtBox.Name.Equals(passwordTxtBox.Name))
            {
                passwordToolTip.Show(errorMsg, txtBox, new Point(105), ToolTipTimeDisplayed);
            }
        }

        private void signInBtn_Click(object sender, EventArgs e)
        {
            AppState.StaticValidateAppStateForNull(this._appState);

            if (!String.IsNullOrEmpty(userNameTxtBox.Text) ||
                !String.IsNullOrEmpty(passwordTxtBox.Text))
            {
                this._appState!.AppData.UserRecord.UserName = userNameTxtBox.Text.Trim();
                this._appState!.AppData.UserRecord.Password = passwordTxtBox.Text.Trim();

                AppController.AuthenticateLogIn(this._appState);

                // This will be change to trigger a new controller
                userNameTxtBox.Text = this._appState.Authenticated.ToString();
            }
        }
    }
}

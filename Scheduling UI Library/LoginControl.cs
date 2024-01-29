using Scheduling_API.Controller;
using Scheduling_API.Controller.State;
using Scheduling_UI_App.UI_Process;
using Scheduling_UI_Library.UI_Validator;

namespace Scheduling_UI_Library
{
    // The loging control for the user to log.
    public partial class LoginControl : UserControl
    {
        public const string SignInBtnName = nameof(signInBtn);
        public const string UserNameTxtBoxName = nameof(userNameTxtBox);
        public const string PasswordTxtBoxName = nameof(passwordTxtBox);
        public bool UI_AuthenticationAttempt { private set; get; } = false;

        const int ToolTipTimeDisplayed = 5000;

        public LoginControl()
        {
            // 1) Create Components
            InitializeComponent();
        }

        public void InitComponents()
        {
            userNameToolTip.Dispose();
            passwordToolTip.Dispose();
            InitializeComponent();
        }

        // https://stackoverflow.com/questions/42535141/winform-tooltip-location-setting
        protected void OnTxtBoxLostFocus(object sender, EventArgs args)
        {
            TextBox txtBox = (TextBox)sender;
            string errorMsg = ControlValidator.ValidateTxtBox(txtBox);

            if (txtBox.Name.Equals(userNameTxtBox.Name))
            {
                userNameToolTip.Show(errorMsg, txtBox, new Point(105), ToolTipTimeDisplayed);
            }
            else if (txtBox.Name.Equals(passwordTxtBox.Name))
            {
                passwordToolTip.Show(errorMsg, txtBox, new Point(105), ToolTipTimeDisplayed);
            }
        }

        private void SignInBtn_Click(object sender, EventArgs e)
        {
            AppState.StaticValidateAppStateForNull(UIState.State);
            UI_AuthenticationAttempt = false;

            if (String.IsNullOrEmpty(userNameTxtBox.Text) ||
                String.IsNullOrEmpty(passwordTxtBox.Text))
            {
                return;
            }

            UIState.State!.AppData.UserRecord.UserName = userNameTxtBox.Text.Trim();
            UIState.State!.AppData.UserRecord.Password = passwordTxtBox.Text.Trim();

            AppController.AuthenticateLogIn(UIState.State);
            UI_AuthenticationAttempt = true;
        }
    }
}

using Scheduling_UI_App.UI_Process;
using Scheduling_UI_App.UI_StyleUtility;
using Scheduling_UI_Library;

namespace Scheduling_UI_App
{
    // Login Control/Form for the user.
    internal partial class LoginCanvasControl : UserControl
    {
        public const string ErrorLblName = nameof(errorLbl);
        public const string LocationLblName = nameof(locationLbl);
        public const string LanguageComboBoxName = nameof(languageComboBox);

        public const string InvalidCredentialMsg_EN = "The username and password did not match.";
        public const string InvalidCredentialMsg_ES = "El nombre de usuario y la contraseña no coincidían.";
        public const string InvalidCredentialMsg_ZH = "使用者名和密碼不匹配。";

        public event EventHandler? OnChangeCulture;

        internal LoginCanvasControl()
        {
            // 1) Create Components
            InitializeComponent();

            languageComboBox.Items.AddRange(new string[] { "English", "Spanish", "Chinese" });

            // Event subscription
            loginControl.Controls[LoginControl.SignInBtnName].LostFocus += OnLostFocus_SignInButton!;
        }

        ~LoginCanvasControl()
        {
            // Event unsubscription
            loginControl.Controls[LoginControl.SignInBtnName].LostFocus -= OnLostFocus_SignInButton!;
        }

        public void ClearCanvas()
        {
            LangTranslator.SetLangCode(LangTranslator.EN);
            OnChangeCulture?.Invoke(this, EventArgs.Empty);

            var userNameTxtBox = (TextBox)this.Controls.Find(LoginControl.UserNameTxtBoxName, true)[0];
            var passwordTxtBox = (TextBox)this.Controls.Find(LoginControl.PasswordTxtBoxName, true)[0];

            userNameTxtBox.ResetText();
            passwordTxtBox.ResetText();

            this.languageComboBox.SelectionStart = 0;
        }

        internal void ShowException(AppForm appform)
        {
            DialogResult result;
            using (new CenterWinDialog(this))
            {
                result = MessageBox.Show(appform.Exception?.Message, "Exception",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
            }

            // If the no button was pressed ...
            if (result == DialogResult.OK)
            {
                Thread.Sleep(2000);
                // cancel the closure of the form.
                Program.Abort();
            }
            errorLbl.Text = appform.Exception?.Message;
        }

        private void OnLostFocus_SignInButton(object sender, EventArgs e)
        {
            errorLbl.ResetText();
        }

        private void OnComboBoxChanged(object sender, EventArgs e)
        {
            if (languageComboBox.SelectedIndex == 0)
            {
                LangTranslator.SetLangCode(LangTranslator.EN);

                if (UIState.State!.Authenticated is false)
                {
                    UIState.State!.NewUnauthorizedAccessException(message: InvalidCredentialMsg_EN);
                }
            }
            else if (languageComboBox.SelectedIndex == 1)
            {
                LangTranslator.SetLangCode(LangTranslator.ES);

                if (UIState.State!.Authenticated is false)
                {
                    UIState.State!.NewUnauthorizedAccessException(message: InvalidCredentialMsg_ES);
                }
            }
            else if (languageComboBox.SelectedIndex == 2)
            {
                LangTranslator.SetLangCode(LangTranslator.ZH);

                if (UIState.State!.Authenticated is false)
                {
                    UIState.State!.NewUnauthorizedAccessException(message: InvalidCredentialMsg_ZH);
                }
            }

            this.loginControl.Invoke(() =>
            {
                this.loginControl.Controls.Clear();
                this.loginControl.InitComponents();
            });

            OnChangeCulture?.Invoke(this, EventArgs.Empty);
        }
    }
}
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
using Scheduling_UI_Library.UI_EventHandler;
using Scheduling_UI_Library.UI_Validator;

namespace Scheduling_UI_Library
{
    public partial class LoginControl : UserControl
    {
        const int ToolTipTimeDisplayed = 5000;
        public LoginControl()
        {
            InitializeComponent();
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

        }
    }
}

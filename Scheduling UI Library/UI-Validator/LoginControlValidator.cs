using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_UI_Library.UI_Validator
{
    internal static class LoginControlValidator
    {
        internal static string ValidateUserNameTxtBox(TextBox? textbox)
        {
            if (textbox?.Text == string.Empty) {
                return "Please fill out this field.";
            }

            return String.Empty;
        }
    }
}

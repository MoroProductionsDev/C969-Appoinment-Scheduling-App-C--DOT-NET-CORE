using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_UI_Library.UI_Validator
{
    internal static class LoginControlValidator
    {
        internal static void ValidateUserNameTxtBox(TextBox textbox)
        {
            if (textbox == null)
            {
                return;
            }

            if (textbox.Text == string.Empty) {
                throw new ArgumentException($"{nameof(textbox)} cannot bet empty.");
            }
        }
    }
}

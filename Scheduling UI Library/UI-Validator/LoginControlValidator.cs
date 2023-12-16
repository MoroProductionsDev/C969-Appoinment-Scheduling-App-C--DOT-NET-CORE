using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling_UI_Library.UI_EventHandler;

namespace Scheduling_UI_Library.UI_Validator
{
    internal static class LoginControlValidator
    {
        internal static string ValidateUserNameTxtBox(TextBox? textbox)
        {
            if (textbox?.Text == string.Empty) {
                return "Please fill out this field.";
                //textbox.Height += textbox.Height;
                //textbox.PlaceholderText = $"Empty {textbox.Name}";
                //throw new ArgumentException($"{nameof(textbox)} cannot bet empty.");
            }

            return String.Empty;
        }
    }
}

namespace Scheduling_UI_Library.UI_Validator
{
    // Validates specific UI controls
    public static class ControlValidator
    {
        public const string EmptyFieldMsg_EN = "Please fill out this field.";
        public const string EmptyFieldMsg_ES = "Por favor, rellene este campo.";
        public const string EmptyFieldMsg_ZH = "請填寫此欄位。";
        public static string ValidateTxtBox(TextBox? textbox)
        {
            if (string.IsNullOrWhiteSpace(textbox?.Text) || string.IsNullOrEmpty(textbox?.Text))
            {

                if (LangTranslator.GetLangCode().Equals(LangTranslator.EN))
                {
                    return EmptyFieldMsg_EN;
                }
                else if (LangTranslator.GetLangCode().Equals(LangTranslator.ES))
                {
                    return EmptyFieldMsg_ES;
                }
                else if (LangTranslator.GetLangCode().Equals(LangTranslator.ZH))
                {
                    return EmptyFieldMsg_ZH;
                }
            }

            return String.Empty;
        }
    }
}

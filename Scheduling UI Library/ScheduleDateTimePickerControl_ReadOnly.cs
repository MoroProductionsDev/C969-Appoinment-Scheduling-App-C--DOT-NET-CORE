namespace Scheduling_UI_Library
{
    // Readonly Date and time information for the user for updates
    public partial class ScheduleDateTimePickerControl_ReadOnly : UserControl
    {
        public const string LocalDateTxtBoxName = nameof(localDateTxtBox);
        public const string LocalTimeTxtBoxName = nameof(localTimeTxtBox);
        public ScheduleDateTimePickerControl_ReadOnly()
        {
            InitializeComponent();
        }
    }
}

using Scheduling_API.Controller.State;

namespace Scheduling_UI_Library
{
    // Date and time picker control for the user
    public partial class ScheduleDateTimePickerControl : UserControl
    {
        public const string DatePickerName = nameof(datePicker);
        public const string TimePickerName = nameof(timePicker);
        public const string UtcDateTxtBoxName = nameof(utcDateTxtBox);
        public const string UtcTimeTxtBoxName = nameof(utcTimeTxtBox);
        public ScheduleDateTimePickerControl()
        {
            InitializeComponent();
        }

        private void ScheduleDateTimePickerControl_Load(object sender, EventArgs e)
        {
            datePicker.Value = DateTime.Today;
            timePicker.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                                AppData.BusinessOpeningHour, 0, 0);

            utcDateTxtBox.Text = datePicker.Value.ToUniversalTime().ToShortDateString();
            utcTimeTxtBox.Text = timePicker.Value.ToUniversalTime().ToShortTimeString();
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime senderDateTime = ((DateTimePicker)sender).Value;

            timePicker.Value = new DateTime(senderDateTime.Year, senderDateTime.Month, senderDateTime.Day,
                         AppData.BusinessOpeningHour, 0, 0);
            utcDateTxtBox.Text = senderDateTime.ToUniversalTime().ToShortDateString(); // *
            utcTimeTxtBox.Text = timePicker.Value.ToUniversalTime().ToShortTimeString();
        }

        private void TimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime senderDateTime = ((DateTimePicker)sender).Value;

            datePicker.Value = senderDateTime.Date;
            utcDateTxtBox.Text = senderDateTime.ToUniversalTime().ToShortDateString();
            utcTimeTxtBox.Text = senderDateTime.ToUniversalTime().ToShortTimeString(); // *
        }
    }
}

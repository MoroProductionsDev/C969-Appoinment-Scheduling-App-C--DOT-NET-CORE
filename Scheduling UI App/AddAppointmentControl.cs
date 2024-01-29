using Scheduling_API.Controller;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Structure;
using Scheduling_UI_App.UI_Process;
using Scheduling_UI_Library;
using System.Data;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;
using Validator = Scheduling_API.Controller.Validate.Validator;

namespace Scheduling_UI_App
{
    // The add appoinment's record control
    public partial class AddAppointmentControl : UserControl
    {
        public const string returnBtnName = nameof(returnBtn);
        private bool appointmentStartDateTimePickerValueChanged = false;
        private bool appointmentEndDateTimePickerValueChanged = false;

        public AddAppointmentControl()
        {
            InitializeComponent();

            // Event subscriptions
            ((DateTimePicker)this.appStartDateScheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.DatePickerName]).ValueChanged += ValidateAppointmentsDate!;
            ((DateTimePicker)this.appEndDateScheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.DatePickerName]).ValueChanged += ValidateAppointmentsDate!;
            ((DateTimePicker)this.appStartDateScheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.TimePickerName]).ValueChanged += ValidateAppointmentsTime!;
            ((DateTimePicker)this.appEndDateScheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.TimePickerName]).ValueChanged += ValidateAppointmentsTime!;
        }

        ~AddAppointmentControl()
        {
            // Event subscriptions
            ((DateTimePicker)this.appStartDateScheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.DatePickerName]).ValueChanged -= ValidateAppointmentsDate!;
            ((DateTimePicker)this.appEndDateScheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.DatePickerName]).ValueChanged -= ValidateAppointmentsDate!;
            ((DateTimePicker)this.appStartDateScheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.TimePickerName]).ValueChanged -= ValidateAppointmentsTime!;
            ((DateTimePicker)this.appEndDateScheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.TimePickerName]).ValueChanged -= ValidateAppointmentsTime!;
        }

        private void AddAppointmentControl_Load(object sender, EventArgs e)
        {
            FillCustomerNameComboBox();
        }

        private void FillCustomerNameComboBox()
        {
            DataTable customerTable = UIState.State!.DbDataSet.DataSet.Tables[TableName.Customer]!;
            DataRowCollection customerNames = customerTable.Rows;

            for (int idx = 0; idx < customerNames.Count; ++idx)
            {
                customerNameComboBox.Items.Add(customerNames[idx][CustomerColumnName.CustomerName]);
            }
        }

        private void ValidateAppointmentsDate(object sender, EventArgs e)
        {
            DateTimePicker appointmentStartDatePicker = ((DateTimePicker)this.appStartDateScheduleDateTimePickerControl
                    .Controls[ScheduleDateTimePickerControl.DatePickerName]);
            DateTimePicker appointmentEndDatePicker = ((DateTimePicker)this.appEndDateScheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.DatePickerName]);

            errorLbl.Text = String.Empty;

            if (((DateTimePicker)sender).Parent.Name.Equals(nameof(appStartDateScheduleDateTimePickerControl)))
            {
                this.appointmentStartDateTimePickerValueChanged = true;
            }
            else if (((DateTimePicker)sender).Parent.Name.Equals(nameof(appEndDateScheduleDateTimePickerControl)))
            {
                this.appointmentEndDateTimePickerValueChanged = true;
            }

            if (appointmentStartDateTimePickerValueChanged && appointmentEndDateTimePickerValueChanged)
            {
                DateTime appointmentStartDate = appointmentStartDatePicker.Value.Date;
                DateTime appointmentEndDate = appointmentEndDatePicker.Value.Date;

                if (appointmentStartDate.CompareTo(appointmentEndDate) > 0)
                {
                    errorLbl.Text = $"Appointment's starting date {appointmentStartDate.ToShortDateString()} " +
                                    $"must be earlier or equal date than appointment's ending date {appointmentEndDate.ToShortDateString()}.";
                }
            }
        }

        private void ValidateAppointmentsTime(object sender, EventArgs e)
        {
            DateTimePicker appointmentStartTimePicker = ((DateTimePicker)this.appStartDateScheduleDateTimePickerControl
                    .Controls[ScheduleDateTimePickerControl.TimePickerName]);
            DateTimePicker appointmentEndTimePicker = ((DateTimePicker)this.appEndDateScheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.TimePickerName]);

            DateTime appointmentStartTime = appointmentStartTimePicker.Value;
            DateTime appointmentEndTime = appointmentEndTimePicker.Value;

            errorLbl.Text = string.Empty;

            ValidateAppointmentsDate(sender, EventArgs.Empty);

            if (Validator.ValidateOutsideBusinessHours(appointmentStartTime))
            {
                errorLbl.Text = $"Appointment's 'starting time' {appointmentStartTime.ToString("hh:mm tt")} " +
                                $"must be equal or later than office opening time [{AppData.BusinessOpeningHour}:00 AM]\n " +
                                $"or earlier than office closing time [{AppData.BusinessClosingHour - AppData.HoursDifferenceMilitaryToStandard}:00 PM]";
            }
            else if (Validator.ValidateOutsideBusinessHours(appointmentEndTime))
            {
                errorLbl.Text = $"Appointment's 'ending time' {appointmentEndTime.ToString("hh:mm tt")} " +
                                $"must be equal or later than office opening time [{AppData.BusinessOpeningHour}:00 AM]\n " +
                                $"or earlier than office closing time [{AppData.BusinessClosingHour - AppData.HoursDifferenceMilitaryToStandard}:00 PM]";
            }
            else if (Validator.ValidateOverlappingAppointment(UIState.State!, appointmentStartTime, out DateTime? overlappedDateTime, out int overlappedId))
            {
                if (overlappedDateTime is not null)
                {
                    errorLbl.Text = $"Appointment's 'starting time' {appointmentStartTime.ToString("MM/dd/yyyy hh:mm tt")} " +
                                $"is overlapping with this appoinment with id [{overlappedId}] date: {((DateTime)overlappedDateTime!).Date.ToShortDateString()}, " +
                                $"hour: {((DateTime)overlappedDateTime).ToShortTimeString()}.";
                }
            }
            else if (Validator.ValidateOverlappingAppointment(UIState.State!, appointmentEndTime, out overlappedDateTime, out overlappedId))
            {
                if (overlappedDateTime is not null)
                {
                    errorLbl.Text = $"Appointment's 'ending time' {appointmentEndTime.ToString("MM/dd/yyyy hh:mm tt")} " +
                                $"is overlapping with this appointment with id [{overlappedId}] date: {((DateTime)overlappedDateTime!).ToShortDateString()}, " +
                                $"hour: {((DateTime)overlappedDateTime).ToShortTimeString()}.";
                }
            }
            else if (appointmentStartTime.TimeOfDay.CompareTo(appointmentEndTime.TimeOfDay) > 0)
            {
                errorLbl.Text = $"Appointment's 'starting time' {appointmentStartTime.ToString("hh:mm tt")} " +
                            $"must be earlier than appointment's ending time {appointmentEndTime.ToString("hh:mm tt")}.";
            }
        }

        private void AddBtn_LostFocus(object sender, EventArgs e)
        {
            databaseStatusPanel.Visible = false;
            errorLbl.ResetText();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            // Check whether any errors has been triggered
            if (errorLbl.Text.Equals(string.Empty) && customerNameComboBox.SelectedItem is not null)
            {
                databaseStatusPanel.Visible = true;
                databaseStatusPanel.Processing();

                DateTimePicker appointmentStartDatePicker = ((DateTimePicker)this.appStartDateScheduleDateTimePickerControl
                    .Controls[ScheduleDateTimePickerControl.DatePickerName]);
                DateTimePicker appointmentStartTimePicker = ((DateTimePicker)this.appStartDateScheduleDateTimePickerControl
                    .Controls[ScheduleDateTimePickerControl.TimePickerName]);

                DateTimePicker appointmentEndDatePicker = ((DateTimePicker)this.appEndDateScheduleDateTimePickerControl
                    .Controls[ScheduleDateTimePickerControl.DatePickerName]);
                DateTimePicker appointmentEndTimePicker = ((DateTimePicker)this.appEndDateScheduleDateTimePickerControl
                    .Controls[ScheduleDateTimePickerControl.TimePickerName]);

                DateTime newAppointmentStartLocalUTCDateTime = new(appointmentStartDatePicker.Value.Date.Year, appointmentStartDatePicker.Value.Date.Month, appointmentStartDatePicker.Value.Date.Day,
                                    appointmentStartTimePicker.Value.Hour, appointmentStartTimePicker.Value.Minute, appointmentStartTimePicker.Value.Second);


                DateTime newAppointmentEndLocalUTCDateTime = new(appointmentEndDatePicker.Value.Date.Year, appointmentEndDatePicker.Value.Date.Month, appointmentEndDatePicker.Value.Date.Day,
                                    appointmentEndTimePicker.Value.Hour, appointmentEndTimePicker.Value.Minute, appointmentEndTimePicker.Value.Second);

                FetchIdDbMetaData<string> fetchCustomerIdDbMetaData =
                    new(
                        ClientScheduleDbSchema._dbName,
                        TableName.Customer,
                        CustomerColumnName.CustomerName,
                        customerNameComboBox.SelectedItem!.ToString()!,
                        CustomerColumnName.CustomerId);

                UIState.State!.AppData.CustomerRecord.CustomerId = UIState.State!.DbDataSet.GetRowId(fetchCustomerIdDbMetaData);
                UIState.State!.AppData.AppointmentRecord.Title = appointmentTitleTxtBox.Text;
                UIState.State!.AppData.AppointmentRecord.Type = appointmentTypeTxtBox.Text;
                UIState.State!.AppData.AppointmentRecord.Location = appointmentLocationTxtBox.Text;
                UIState.State!.AppData.AppointmentRecord.Contact = appointmentContactTxtBox.Text;
                UIState.State!.AppData.AppointmentRecord.Url = appointmentUrlTxtBox.Text;
                UIState.State!.AppData.AppointmentRecord.Description = appointmentDescriptionRichTxtBox.Text;
                UIState.State!.AppData.AppointmentRecord.Start = newAppointmentStartLocalUTCDateTime;
                UIState.State!.AppData.AppointmentRecord.End = newAppointmentEndLocalUTCDateTime;

                try
                {
                    AppController.AddAppointmentRecord(UIState.State);
                }
                catch (Exception)
                {
                    databaseStatusPanel.Error();
                }

                databaseStatusPanel.Success();
            }
            else
            {
                errorLbl.Text = "Select the required fields.";
            }
        }
    }
}

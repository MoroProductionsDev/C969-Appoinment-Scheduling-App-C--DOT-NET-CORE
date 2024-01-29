using Scheduling_API.Controller;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Structure;
using Scheduling_UI_App.UI_Process;
using Scheduling_UI_Library;
using System.Data;
using System.Globalization;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;
using Validator = Scheduling_API.Controller.Validate.Validator;

namespace Scheduling_UI_App
{
    // The update appointment's record control
    public partial class UpdateAppointmentControl : UserControl
    {
        const int firstColIdx = 1; // ignore the primaryKey
        const int idxSeperationBtwDateAndTime = 1;
        const int idxSeperationBtwDateAndDate = 2;
        const int CustomerNameComboBoxIdx = 0;
        const int DescriptionIdx = 2;
        public const string returnBtnName = nameof(returnBtn);

        bool displayDateTimePicker = false;
        bool displayCustomerNameComboBox = false;
        bool displayDescriptionRichTxtBox = false;
        private DataGridViewRow? selectedRow;
        private bool isColumnComboBoxFilled = false;
        private int customerCount = 0;
        private int appointmentSelectedRowIdx = -1;
        private static readonly DataTable CustomerTableRef = UIState.State!.DbDataSet.DataSet.Tables[TableName.Customer]!;

        public UpdateAppointmentControl()
        {
            InitializeComponent();

            // Event subscriptions
            ((DateTimePicker)this.scheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.DatePickerName]).ValueChanged += ValidateAppointmentsDate!;
            ((DateTimePicker)this.scheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.TimePickerName]).ValueChanged += ValidateAppointmentsTime!;
        }

        ~UpdateAppointmentControl()
        {
            // Event subscriptions
            ((DateTimePicker)this.scheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.DatePickerName]).ValueChanged -= ValidateAppointmentsDate!;
            ((DateTimePicker)this.scheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.TimePickerName]).ValueChanged -= ValidateAppointmentsTime!;
        }

        private void UpdateAppointmentControl_Load(object sender, EventArgs e)
        {
            if (!this.isColumnComboBoxFilled)
            {
                FillColumnComboBox();
            }
        }
        public void UpdateControls(DataGridViewRow selectedRow)
        {
            if (this.selectedRow == selectedRow) // don't proceed if it is the same selected row reference
            {
                return;
            }

            this.selectedRow = selectedRow;
            this.appointmentSelectedRowIdx = selectedRow.Index;
            this.currentValueTxtBox.Text = this.selectedRow.Cells[firstColIdx].Value.ToString();

            if (this.customerCount != CustomerTableRef.Rows.Count)
            {
                FillCustomerNameComboBox();
            }

            if (!this.isColumnComboBoxFilled)
            {
                FillColumnComboBox();
            }
            else
            {
                UpdateComponent();
            }
        }

        private void FillColumnComboBox()
        {
            DataGridViewColumnCollection selectedColumns = this.selectedRow!.DataGridView!.Columns;

            for (int idx = firstColIdx; idx < selectedColumns.Count; ++idx)
            {
                if (idx == selectedColumns.Count - idxSeperationBtwDateAndTime ||
                    idx == selectedColumns.Count - 2 - idxSeperationBtwDateAndTime)
                {
                    continue;
                }
                this.columnComboBox.Items.Add(selectedColumns[idx].Name);
            }

            this.columnComboBox.SelectedIndex = 0;
            this.isColumnComboBoxFilled = true;
        }

        private void FillCustomerNameComboBox()
        {
            DataTable customerTable = UIState.State!.DbDataSet.DataSet.Tables[TableName.Customer]!;
            DataRowCollection customerNames = customerTable.Rows;

            for (int idx = 0; idx < customerNames.Count; ++idx)
            {
                customerNameComboBox.Items.Add(customerNames[idx][CustomerColumnName.CustomerName]);
            }

            this.customerCount = customerNames.Count;
        }

        private void ColumnComboBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateComponent();
        }

        private void UpdateComponent()
        {
            this.displayDateTimePicker = false;
            this.displayCustomerNameComboBox = false;
            this.displayDescriptionRichTxtBox = false;

            errorLbl.Text = String.Empty;

            // Below it shows specific controls based on the user's selection
            if (this.selectedRow is not null)
            {
                if (columnComboBox.Items.Count - 1 == columnComboBox.SelectedIndex ||  // start date
                    columnComboBox.Items.Count - 2 == columnComboBox.SelectedIndex)  // end date
                {
                    displayDateTimePicker = true;
                }
                else if (columnComboBox.SelectedIndex == CustomerNameComboBoxIdx)
                {
                    displayCustomerNameComboBox = true;
                }
                else if (columnComboBox.SelectedIndex == DescriptionIdx)
                {
                    displayDescriptionRichTxtBox = true;
                }
            }

            if (displayDateTimePicker)
            {
                int cellIdx = columnComboBox.SelectedItem.Equals($"{AppointmentColumnName.Start} date") ?
                    (selectedRow!.Cells.Count - 4) :
                    (selectedRow!.Cells.Count - 2);

                this.currentValueLbl.Location = new Point(238, 68);
                this.newValueLbl.Location = new Point(390, 68);

                this.currentValueTxtBox.Visible = false;
                this.newValueTxtBox.Visible = false;
                this.customerNameComboBox.Visible = false;
                this.currentAppointmentDescriptionRichTxtBox.Visible = false;
                this.newAppointmentDescriptionRichTxtBox.Visible = false;

                var oldDateTxtBox = scheduleDateTimePickerControl_ReadOnly.Controls[ScheduleDateTimePickerControl_ReadOnly.LocalDateTxtBoxName];
                var oldTimeTxtBox = scheduleDateTimePickerControl_ReadOnly.Controls[ScheduleDateTimePickerControl_ReadOnly.LocalTimeTxtBoxName];

                oldDateTxtBox.Text = ((DateTime)selectedRow!.Cells[cellIdx].Value).Date.ToShortDateString();
                oldTimeTxtBox.Text = (string)selectedRow!.Cells[cellIdx + idxSeperationBtwDateAndTime].Value.ToString()!;

                scheduleDateTimePickerControl_ReadOnly.Visible = true;
                scheduleDateTimePickerControl.Visible = true;
            }
            else if (displayCustomerNameComboBox)
            {
                int cellIdx = columnComboBox.SelectedIndex + firstColIdx;

                this.newValueTxtBox.Visible = false;
                this.scheduleDateTimePickerControl_ReadOnly.Visible = false;
                this.scheduleDateTimePickerControl.Visible = false;
                this.currentAppointmentDescriptionRichTxtBox.Visible = false;
                this.newAppointmentDescriptionRichTxtBox.Visible = false;

                this.currentValueTxtBox.Text = selectedRow!.Cells[cellIdx].Value.ToString();

                this.currentValueTxtBox.Visible = true;
                this.customerNameComboBox.Visible = true;
            }
            else if (displayDescriptionRichTxtBox)
            {
                int cellIdx = columnComboBox.SelectedIndex + firstColIdx;

                this.currentValueTxtBox.Visible = false;
                this.newValueTxtBox.Visible = false;
                this.customerNameComboBox.Visible = false;
                this.scheduleDateTimePickerControl_ReadOnly.Visible = false;
                this.scheduleDateTimePickerControl.Visible = false;

                this.currentAppointmentDescriptionRichTxtBox.Text = selectedRow!.Cells[cellIdx].Value.ToString();

                this.newAppointmentDescriptionRichTxtBox.Visible = true;
                this.currentAppointmentDescriptionRichTxtBox.Visible = true;
            }
            else  // standartTxtBox
            {
                int cellIdx = columnComboBox.SelectedIndex + firstColIdx;

                this.currentValueLbl.Location = new Point(174, 60);
                this.newValueLbl.Location = new Point(323, 66);

                this.scheduleDateTimePickerControl_ReadOnly.Visible = false;
                this.scheduleDateTimePickerControl.Visible = false;
                this.customerNameComboBox.Visible = false;
                this.currentAppointmentDescriptionRichTxtBox.Visible = false;
                this.newAppointmentDescriptionRichTxtBox.Visible = false;

                this.currentValueTxtBox.Text = selectedRow!.Cells[cellIdx].Value.ToString();

                this.currentValueTxtBox.Visible = true;
                this.newValueTxtBox.Visible = true;
            }
        }

        private void ValidateAppointmentsDate(object sender, EventArgs e)
        {
            bool operatingOnStartDate = columnComboBox.SelectedItem.Equals($"{AppointmentColumnName.Start} date");
            int appoimentDateIdx = operatingOnStartDate ?
                (selectedRow!.Cells.Count - 4) :
                (selectedRow!.Cells.Count - 2);
            int otherAppoitmentDateIdx = operatingOnStartDate ?
                (selectedRow!.Cells.Count - 2) : (selectedRow!.Cells.Count - 4);

            DateTime newAppointmentDate = ((DateTimePicker)sender).Value.Date;
            DateTime otherCompareDate = ((DateTime)selectedRow!.Cells[otherAppoitmentDateIdx].Value).Date;

            string appoinmentStartingOrEndingTimeVerb = operatingOnStartDate ? "starting" : "ending";
            string appoinmentOppesiteStartingOrEndingTimeVerb = operatingOnStartDate ? "ending" : "starting";
            string appoinmentDateTimeEarlierOrLaterAdjective = operatingOnStartDate ? "earlier" : "later";

            errorLbl.Text = String.Empty;

            if (appoimentDateIdx == selectedRow!.Cells.Count - 4 && newAppointmentDate.CompareTo(otherCompareDate) > 0)
            {
                this.errorLbl.Text = $"Appointment's {appoinmentStartingOrEndingTimeVerb} date {newAppointmentDate.ToShortDateString()} " +
                                $"must be {appoinmentDateTimeEarlierOrLaterAdjective} or equal date than appointment's {appoinmentOppesiteStartingOrEndingTimeVerb} date {otherCompareDate.ToShortDateString()}.";
            }
        }

        private void ValidateAppointmentsTime(object sender, EventArgs e)
        {
            DateTimePicker appointmentTimePicker = ((DateTimePicker)this.scheduleDateTimePickerControl
                .Controls[ScheduleDateTimePickerControl.TimePickerName]);

            bool operatingOnStartDate = columnComboBox.SelectedItem.Equals($"{AppointmentColumnName.Start} date");
            int otherAppoitmentDateIdx = operatingOnStartDate ?
                (selectedRow!.Cells.Count - 2) : (selectedRow!.Cells.Count - 4);


            string dateString = ((DateTime)selectedRow!.Cells[otherAppoitmentDateIdx].Value).ToShortDateString();
            string timeString = (string)selectedRow!.Cells[otherAppoitmentDateIdx + idxSeperationBtwDateAndTime].Value;
            CultureInfo provider = new("en-US");
            string[] validformats = new[] { "M/dd/yyyy", "yyyy/M/dd", "M/dd/yyyy HH:mm:ss",
                                        "M/dd/yyyy h:mm tt", "MM/dd/yyyy hh:mm tt", "yyyy-MM-dd HH:mm:ss, fff" };

            DateTime newAppointmentDateTime = appointmentTimePicker.Value;
            DateTime otherCompareDateTime = DateTime.ParseExact($"{dateString} {timeString}", validformats, provider);

            string appoinmentStartingOrEndingTimeVerb = operatingOnStartDate ? "starting" : "ending";
            string appoinmentOppesiteStartingOrEndingTimeVerb = operatingOnStartDate ? "ending" : "starting";
            string appoinmentDateTimeEarlierOrLaterAdjective = operatingOnStartDate ? "earlier" : "later";

            errorLbl.Text = String.Empty;

            ValidateAppointmentsDate(sender, EventArgs.Empty);

            if (Validator.ValidateOutsideBusinessHours(newAppointmentDateTime))
            {

                errorLbl.Text = $"Appointment's '{appoinmentStartingOrEndingTimeVerb} time' {newAppointmentDateTime.ToString("hh:mm tt")} " +
                                $"must be equal or later than office opening time [{AppData.BusinessOpeningHour}:00 AM]\n " +
                                $"or earlier than office closing time [{AppData.BusinessClosingHour - AppData.HoursDifferenceMilitaryToStandard}:00 PM]";
            }
            else if (Validator.ValidateOverlappingAppointment(UIState.State!, newAppointmentDateTime, out DateTime? overlappedDateTime, out int overlappedId))
            {
                if (overlappedDateTime is not null)
                {
                    errorLbl.Text = $"Appointment's {appoinmentStartingOrEndingTimeVerb} time' {newAppointmentDateTime.ToString("MM/dd/yyyy hh:mm tt")} " +
                                $"is overlapping with this appointment with id [{overlappedId}] date: {((DateTime)overlappedDateTime!).Date.ToShortDateString()}, " +
                                $"hour: {((DateTime)overlappedDateTime).ToShortTimeString()}.";
                }
            }
            else if (newAppointmentDateTime.TimeOfDay.CompareTo(otherCompareDateTime.TimeOfDay) > 0)
            {
                // Do not validate further if it is not the same date.
                if (newAppointmentDateTime.Date.CompareTo(otherCompareDateTime.Date) != 0)
                {
                    return;
                }

                errorLbl.Text = $"Appointment's {appoinmentStartingOrEndingTimeVerb} time {newAppointmentDateTime.ToString("hh:mm tt")} " +
                                $"must be {appoinmentDateTimeEarlierOrLaterAdjective} than appointment's {appoinmentOppesiteStartingOrEndingTimeVerb} time {otherCompareDateTime.ToString("hh:mm tt")}.";
            }
        }

        private void UpdateBtn_LostFocus(object sender, EventArgs e)
        {
            databaseStatusPanel.Visible = false;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            // Check whether any errors has been triggered
            if (errorLbl.Text.Equals(String.Empty))
            {
                UpdateProcessing();
            }
        }

        private void UpdateProcessing()
        {
            databaseStatusPanel.Visible = true;
            databaseStatusPanel.Processing();

            int rowIdx = appointmentSelectedRowIdx; // index is off by 1
            string columnName = columnComboBox.SelectedItem.ToString()!;

            UpdateDbMetaData<string>? updateDbMetaData;

            if (this.displayCustomerNameComboBox)
            {
                int customerIdFK = (int)UIState.State!.DbDataSet.DataSet.Tables[TableName.Appointment]!.Rows[rowIdx][AppointmentColumnName.CustomerId];

                if (customerNameComboBox.SelectedItem is null)
                {
                    errorLbl.Text = "Select an item value in the combo box.";
                    return;
                }

                updateDbMetaData = new(
                    _dbName: ClientScheduleDbSchema._dbName,
                    _tableName: TableName.Customer,
                    _valueColumnName: columnName,
                    _currentValue: this.currentValueTxtBox.Text,
                    _newValue: customerNameComboBox.SelectedItem.ToString()!,
                    _idColumnName: AppointmentColumnName.CustomerId,
                    _idValue: customerIdFK);

                try
                {
                    AppController.UpdateRecord(UIState.State, updateDbMetaData);
                }
                catch (Exception)
                {
                    databaseStatusPanel.Error();
                }

                // Update UI
                this.currentValueTxtBox.Text = columnComboBox.SelectedItem.ToString();
                this.newValueTxtBox.ResetText();
                // select row not needed because it is a column box.
            }
            else if (this.displayDescriptionRichTxtBox)
            {
                int appointmentIdPK = (int)UIState.State!.DbDataSet.DataSet.Tables[TableName.Appointment]!.Rows[rowIdx][AppointmentColumnName.AppointmentId];

                updateDbMetaData = new(
                        _dbName: ClientScheduleDbSchema._dbName,
                        _tableName: TableName.Appointment,
                        _valueColumnName: columnName,
                        _currentValue: this.currentAppointmentDescriptionRichTxtBox.Text,
                        _newValue: this.newAppointmentDescriptionRichTxtBox.Text,
                        _idColumnName: AppointmentColumnName.AppointmentId,
                        _idValue: appointmentIdPK);

                try
                {
                    AppController.UpdateRecord(UIState.State, updateDbMetaData);
                }
                catch (Exception)
                {
                    databaseStatusPanel.Error();
                }

                // Update UI
                selectedRow!.Cells[0].Selected = false;
                this.currentAppointmentDescriptionRichTxtBox.Text = newAppointmentDescriptionRichTxtBox.Text;
                this.selectedRow!.Cells[columnComboBox.SelectedIndex + firstColIdx].Value = newAppointmentDescriptionRichTxtBox.Text;
                this.newAppointmentDescriptionRichTxtBox.ResetText();
            }
            else if (this.displayDateTimePicker)
            {
                var names = columnName.Split(); // split the 'start date' column name just use the first
                columnName = names[0];
                int appointmentIdPK = (int)UIState.State!.DbDataSet.DataSet.Tables[TableName.Appointment]!.Rows[rowIdx][AppointmentColumnName.AppointmentId];

                DateTimePicker appointmentDatePicker = ((DateTimePicker)this.scheduleDateTimePickerControl
                    .Controls[ScheduleDateTimePickerControl.DatePickerName]);
                DateTimePicker appointmentTimePicker = ((DateTimePicker)this.scheduleDateTimePickerControl
                    .Controls[ScheduleDateTimePickerControl.TimePickerName]);

                DateTime currentLocalDateTime = ((DateTime)UIState.State.DbDataSet.DataSet
                                               .Tables[TableName.Appointment]!.Rows[this.appointmentSelectedRowIdx][columnName]); // Already in UTC time

                DateTime newLocalDateTime = new(appointmentDatePicker.Value.Date.Year, appointmentDatePicker.Value.Date.Month, appointmentDatePicker.Value.Date.Day,
                                    appointmentTimePicker.Value.Hour, appointmentTimePicker.Value.Minute, appointmentTimePicker.Value.Second);

                TextBox currentDate = ((TextBox)this.scheduleDateTimePickerControl_ReadOnly.Controls[ScheduleDateTimePickerControl_ReadOnly.LocalDateTxtBoxName]);
                TextBox currentTime = ((TextBox)this.scheduleDateTimePickerControl_ReadOnly.Controls[ScheduleDateTimePickerControl_ReadOnly.LocalTimeTxtBoxName]);

                TextBox utcDate = ((TextBox)this.scheduleDateTimePickerControl.Controls[ScheduleDateTimePickerControl.UtcDateTxtBoxName]);
                TextBox utcTime = ((TextBox)this.scheduleDateTimePickerControl.Controls[ScheduleDateTimePickerControl.UtcTimeTxtBoxName]);


                UpdateDbMetaData<DateTime> updateDbMetaData_DateTime = new(
                    _dbName: ClientScheduleDbSchema._dbName,
                    _tableName: TableName.Appointment,
                    _valueColumnName: columnName,
                    _currentValue: currentLocalDateTime,
                    _newValue: newLocalDateTime,
                    _idColumnName: AppointmentColumnName.AppointmentId,
                    _idValue: appointmentIdPK);

                try
                {
                    AppController.UpdateRecord(UIState.State, updateDbMetaData_DateTime);
                }
                catch (Exception)
                {
                    databaseStatusPanel.Error();
                }

                // Update UI
                utcDate.ResetText();
                utcTime.ResetText();

                appointmentDatePicker.Value = DateTime.Today;
                appointmentTimePicker.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                                    AppData.BusinessOpeningHour, 0, 0);

                currentDate.Text = newLocalDateTime.Date.ToShortDateString();
                currentTime.Text = newLocalDateTime.ToShortTimeString();

                selectedRow!.Cells[0].Selected = false;
                this.selectedRow!.Cells[columnComboBox.SelectedIndex + idxSeperationBtwDateAndDate].Value = newLocalDateTime.Date.ToShortDateString();
                this.selectedRow!.Cells[columnComboBox.SelectedIndex + idxSeperationBtwDateAndDate + idxSeperationBtwDateAndTime].Value = newLocalDateTime.ToShortTimeString();
            }
            else
            {
                int AppointmentPK = (int)UIState.State!.DbDataSet.DataSet.Tables[TableName.Appointment]!.Rows[rowIdx][AppointmentColumnName.AppointmentId];

                updateDbMetaData = new(
                                    _dbName: ClientScheduleDbSchema._dbName,
                                    _tableName: TableName.Appointment,
                                    _valueColumnName: columnName,
                                    _currentValue: this.currentValueTxtBox.Text,
                                    _newValue: this.newValueTxtBox.Text,
                                    _idColumnName: AppointmentColumnName.AppointmentId,
                                    _idValue: AppointmentPK);

                try
                {
                    AppController.UpdateRecord(UIState.State, updateDbMetaData);
                }
                catch (Exception)
                {
                    databaseStatusPanel.Error();
                }

                // Update UI
                selectedRow!.Cells[0].Selected = false;
                this.selectedRow!.Cells[columnComboBox.SelectedIndex + firstColIdx].Value = newValueTxtBox.Text;
                this.currentValueTxtBox.Text = newValueTxtBox.Text;
                this.newValueTxtBox.ResetText();
            }

            databaseStatusPanel.Success();
        }
    }
}

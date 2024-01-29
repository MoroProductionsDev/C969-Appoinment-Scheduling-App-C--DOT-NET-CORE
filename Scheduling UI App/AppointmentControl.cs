using Scheduling_API.Controller;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Structure;
using Scheduling_UI_App.UI_Process;
using Scheduling_UI_Library.UI_Process;

namespace Scheduling_UI_App
{
    // The overall main form control where the user will take most part exploring and taking actions.
    public partial class AppointmentControl : UserControl
    {
        public const string AddRecordBtnName = nameof(addRecordBtn);
        public const string UpdateRecordBtnName = nameof(updateRecordBtn);
        public const string DeleteRecordBtnName = nameof(deleteRecordBtn);
        public const string CeportBtnName = nameof(reportBtn);
        public const string CustomerRdBtnName = nameof(customerRdBtn);
        public const string AppointmentRdBtnName = nameof(appointmentRdBtn);
        public const string CustomerDataGridViewName = nameof(customerDataGridView);
        public const string AppoinmentDataGridViewName = nameof(appoinmentDataGridView);
        public const string AppStateBindingSourceName = nameof(customerDataGridView);
        public const string ErrorLblName = nameof(errorLbl);
        public const string SignOutLinkLblName = nameof(signOutLinkLbl);
        public const string DatabaseStatusPanelName = nameof(databaseStatusPanel);

        public const string ErrorMsgCustomerRowNotSelected = "[Error] You must select a row in the 'customer' view table!";
        public const string ErrorMsgCustomerEmptyRowSelected = "[Error] (Empty Row) You must select a valid row in the 'customer' view table!";
        public const string ErrorMsgAppointmentRowNotSelected = "[Error] You must select a row in the 'appointment' view table!";
        public const string ErrorMsgAppointmentEmptyRowSelected = "[Error] (Empty Row) You must select a row in the 'appointment' view table!";

        private readonly int userNameAppointmentColumnIdx = 1;
        public AppointmentControl()
        {
            InitializeComponent();
        }

        private void AppointmentControl_Load(object sender, EventArgs e)
        {
            UIState.BindAppStateBindingSource(this.appStateBindingSource);

            CheckForRemainders();

            customerDataGridView.DataSource = ((AppState)appStateBindingSource.DataSource).AppDataView.CustomerTableView;
            appoinmentDataGridView.DataSource = ((AppState)appStateBindingSource.DataSource).AppDataView.AppointmentTableView;

            customerDataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
            appoinmentDataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);

            offficeHoursLocalTimeLbl.Text = $"{AppData.BusinessOpeningHour}:00 AM - {AppData.BusinessClosingHour - AppData.HoursDifferenceMilitaryToStandard}:00 PM";
            timeZoneLbl.Text = $"TimeZone: {UIState.State!.TimeZone}";
            userNameLbl.Text += $" {UIState.State.AppData.UserRecord.UserName}\n[id : {UIState.State.AppData.UserRecord.UserId}]";
        }
        private void CustomerRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            addRecordBtn.Text = $"Add: {((RadioButton)sender).Text}";
            updateRecordBtn.Text = $"Update: {((RadioButton)sender).Text}";
            deleteRecordBtn.Text = $"Delete: {((RadioButton)sender).Text}";
        }

        private void AppointmentRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            addRecordBtn.Text = $"Add: {((RadioButton)sender).Text}";
            updateRecordBtn.Text = $"Update: {((RadioButton)sender).Text}";
            deleteRecordBtn.Text = $"Delete: {((RadioButton)sender).Text}";
        }

        private void AppoinmentDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (appoinmentDataGridView.SelectedCells.Count > 0)
            {
                customerDataGridView.ClearSelection();

                int appointmentRowIdx = appoinmentDataGridView.SelectedCells[0].RowIndex;
                DataGridViewCell appointmentCell = appoinmentDataGridView.Rows[appointmentRowIdx].Cells[userNameAppointmentColumnIdx];

                // Check if an empty cell has been selected
                if (appointmentCell.Value.GetType().Equals(typeof(System.DBNull)))
                {
                    return;
                }

                string userName = (string)appointmentCell.Value;

                AppController.GetRecordID(
                        (AppState)appStateBindingSource.DataSource,
                        new FetchIdDbMetaData<string>(
                            ClientScheduleDbSchema._dbName,
                            ClientScheduleDbSchema.TableName.Customer,
                            ClientScheduleDbSchema.CustomerColumnName.CustomerName,
                            userName,
                            ClientScheduleDbSchema.CustomerColumnName.CustomerId)
                        );

                int customerId = UIState.State!.SelectedId;

                this.customerDataGridView.ClearSelection();
                UIComponent.SelectRowOnTableIdx(customerDataGridView, customerId);
            }
        }

        private static void CheckForRemainders()
        {
            if (UIState.State!.UpcomingAppointmentIds.Count > 0)
            {
                for (int idx = 0; idx < UIState.State!.UpcomingAppointmentIds.Count; ++idx)
                {
                    int id = UIState.State!.UpcomingAppointmentIds[idx];
                    double remainingMinutes = UIState.State!.UpcomingAppointmentRemainingMinutes[idx];

                    MessageBox.Show($"Appointment id [{id}] will start soon.\n{remainingMinutes} minutes remaining.",
                        "Appointment Remainder",
                         MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                }
            }
        }
    }
}

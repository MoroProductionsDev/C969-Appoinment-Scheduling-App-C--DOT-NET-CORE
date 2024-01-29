using Scheduling_API.Controller;
using Scheduling_API.Controller.State;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Structure;
using Scheduling_UI_App.UI_Process;
using Scheduling_UI_Library;
using Scheduling_UI_Library.UI_Process;
using System.Data.Common;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;

// https://stackoverflow.com/questions/142003/cross-thread-operation-not-valid-control-accessed-from-a-thread-other-than-the
namespace Scheduling_UI_App
{
    // The only form [AppForm] that redirects the user from one control to another based on
    // the user's actions.
    internal partial class AppForm : Form
    {
        // Private
        private int customerSelectedRowIdx;
        private int appointmentSelectedRowIdx;
        private event EventHandler OnException;

        // Public/Internal
        internal Exception? Exception { private set; get; }

        // Constructor
        public AppForm()
        {
            // 1) Create components
            InitializeComponent();

            // 2) Events subscription
            this.appStateBackgroundWorker.DoWork += AppStateBackgroundWorker_DoWork!;
            this.appStateBackgroundWorker.RunWorkerCompleted += AppStateBackgroundWorker_RunWorkerCompleted!;
            this.OnException += HandleException!;
            this.loginCanvas.OnChangeCulture += ReSubscribeSignInBtn!;
            this.loginCanvas.Controls[nameof(LoginControl)].Controls[LoginControl.SignInBtnName].Click += OnClickRedirect_AppointmentControl!;
            this.appointmentCanvas.Controls[AppointmentControl.AddRecordBtnName].Click += OnClickRedirect_AddControl!;
            this.appointmentCanvas.Controls[AppointmentControl.UpdateRecordBtnName].Click += OnClickRedirect_UpdateControl!;
            this.appointmentCanvas.Controls[AppointmentControl.UpdateRecordBtnName].LostFocus += UpdateBtn_LostFocus!;
            this.appointmentCanvas.Controls[AppointmentControl.DeleteRecordBtnName].Click += OnClickRedirect_DeleteControl!;
            this.appointmentCanvas.Controls[AppointmentControl.DeleteRecordBtnName].LostFocus += DeleteBtn_LostFocus!;
            this.appointmentCanvas.Controls[AppointmentControl.CeportBtnName].Click += OnClickRedirect_ReportControl!;
            ((LinkLabel)this.appointmentCanvas.Controls[AppointmentControl.SignOutLinkLblName]).LinkClicked += OnClickRedirectBack_LoginCanvasControl;
            this.addCustomerControl.Controls[AddCustomerControl.returnBtnName].Click += OnClickRedirectBack_AppointmentControl!;
            this.addAppointmentControl.Controls[AddAppointmentControl.returnBtnName].Click += OnClickRedirectBack_AppointmentControl!;
            this.updateCustomerControl.Controls[UpdateCustomerControl.returnBtnName].Click += OnClickRedirectBack_AppointmentControl!;
            this.updateAppointmentControl.Controls[UpdateAppointmentControl.returnBtnName].Click += OnClickRedirectBack_AppointmentControl!;
            this.reportControl.Controls[ReportControl.returnBtnName].Click += OnClickRedirectBack_AppointmentControl!;
        }

        // Destructor
        ~AppForm()
        {
            // Events unsubscription
            this.appStateBackgroundWorker.DoWork -= AppStateBackgroundWorker_DoWork!;
            this.appStateBackgroundWorker.RunWorkerCompleted -= AppStateBackgroundWorker_RunWorkerCompleted!;
            this.OnException -= HandleException!;
            this.loginCanvas.Controls[nameof(LoginControl)].Controls[LoginControl.SignInBtnName].Click -= OnClickRedirect_AppointmentControl!;
            this.appointmentCanvas.Controls[AppointmentControl.AddRecordBtnName].Click -= OnClickRedirect_AddControl!;
            this.appointmentCanvas.Controls[AppointmentControl.UpdateRecordBtnName].Click -= OnClickRedirect_UpdateControl!;
            this.appointmentCanvas.Controls[AppointmentControl.UpdateRecordBtnName].LostFocus -= UpdateBtn_LostFocus!;
            this.appointmentCanvas.Controls[AppointmentControl.DeleteRecordBtnName].Click -= OnClickRedirect_DeleteControl!;
            this.appointmentCanvas.Controls[AppointmentControl.DeleteRecordBtnName].LostFocus -= DeleteBtn_LostFocus!;
            this.appointmentCanvas.Controls[AppointmentControl.CeportBtnName].Click -= OnClickRedirect_ReportControl!;
            ((LinkLabel)this.appointmentCanvas.Controls[AppointmentControl.SignOutLinkLblName]).LinkClicked -= OnClickRedirectBack_LoginCanvasControl;
            this.addCustomerControl.Controls[AddCustomerControl.returnBtnName].Click -= OnClickRedirectBack_AppointmentControl!;
            this.addAppointmentControl.Controls[AddAppointmentControl.returnBtnName].Click -= OnClickRedirectBack_AppointmentControl!;
            this.updateCustomerControl.Controls[UpdateCustomerControl.returnBtnName].Click -= OnClickRedirectBack_AppointmentControl!;
            this.updateAppointmentControl.Controls[UpdateCustomerControl.returnBtnName].Click -= OnClickRedirectBack_AppointmentControl!;
            this.reportControl.Controls[ReportControl.returnBtnName].Click -= OnClickRedirectBack_AppointmentControl!;
        }

        private void ReSubscribeSignInBtn(object sender, EventArgs e)
        {
            this.loginCanvas.Controls[nameof(LoginControl)].Controls[LoginControl.SignInBtnName].Click += OnClickRedirect_AppointmentControl!;
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = new Bitmap(Properties.AppBackgroundImages._3d_background_with_white_cubes_1_1,
                            this.ClientSize.Width,
                            this.ClientSize.Height);

            UIComponent.EnableControl(this, loginCanvas.Controls[nameof(LoginControl)], false);
            UIComponent.ChangeBackgroundColor(this, this.Controls);

            // Run worker asynchronously
            appStateBackgroundWorker.RunWorkerAsync();
        }

        private void AppStateBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                UIAnimation.LoadingAnimation(this, loginCanvas, true);

                // 1) Initialiaze state
                UIState.Configure(AppInfo.MySqlClientNameSpace, AppInfo.ClientScheduleDbName);
                UIState.BindAppStateBindingSource(this.appStateBindingSource);

                UIComponent.EnableControl(this, loginCanvas.Controls[nameof(LoginControl)], true); // enable all the controls
            }
            catch (DbException dbExcp)
            {
                // Stored the specific exception on the form's expection property
                this.Exception = dbExcp;
                this.OnException.Invoke(sender, new EventArgs());   // invoke the onException event handler
            }
            catch (InvalidOperationException invldOpExcp)
            {
                // Stored the specific exception on the form's expection property
                this.Exception = invldOpExcp;
                this.OnException.Invoke(sender, new EventArgs());   // invoke the onException event handler
            }
            finally
            {
                UIAnimation.LoadingAnimation(this, loginCanvas, false);
            }
        }

        private void AppStateBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            UIComponent.SetBindingSources(loginCanvas.Controls[LoginCanvasControl.LocationLblName], nameof(UIState.State.Location),
                                        appStateBindingSource);

            UIState.State!.NewUnauthorizedAccessException(message: LoginCanvasControl.InvalidCredentialMsg_EN);

            UpdateLocationLabel();
        }

        private void OnClickRedirect_AppointmentControl(object sender, EventArgs e)
        {
            if (false == ((LoginControl)loginCanvas.Controls[nameof(LoginControl)]).UI_AuthenticationAttempt)
            {
                return;
            }

            if (false == UIState.State!.Authenticated)
            {
                this.loginCanvas.Controls[LoginCanvasControl.ErrorLblName].Text = UIState.State!.AppException?.Message;
                return;
            }

            LangTranslator.SetLangCode(LangTranslator.EN);
            UIFlow.CanvasRedirection(this, loginCanvas, appointmentCanvas);
        }

        private void OnClickRedirectBack_AppointmentControl(object sender, EventArgs e)
        {
            DataGridView customerDataGridView = (DataGridView)appointmentCanvas.Controls[AppointmentControl.CustomerDataGridViewName];
            customerDataGridView.DataSource = UIState.State!.AppDataView.CustomerTableView;

            DataGridView appointmentDataGridView = (DataGridView)appointmentCanvas.Controls[AppointmentControl.AppoinmentDataGridViewName];
            appointmentDataGridView.DataSource = UIState.State!.AppDataView.AppointmentTableView;

            // Refresh Customer's DataGridView
            customerDataGridView.Refresh();
            // Refresh Appointment's DataGridView
            appointmentDataGridView.Refresh();

            appointmentDataGridView.Rows[0].Selected = false;

            customerDataGridView.Rows[this.customerSelectedRowIdx].Selected = true;
            appointmentDataGridView.Rows[this.appointmentSelectedRowIdx].Selected = true;


            UIFlow.CanvasRedirection(this, ((Control)sender).Parent, appointmentCanvas);
        }

        private void OnClickRedirectBack_LoginCanvasControl(object sender, EventArgs e)
        {
            AppController.UnAutenticate(UIState.State);

            UIFlow.CanvasRedirection(this, ((Control)sender).Parent, loginCanvas);
            loginCanvas.ClearCanvas();
        }

        private void OnClickRedirect_AddControl(object sender, EventArgs e)
        {
            bool customerRdBtnChecked = ((RadioButton)appointmentCanvas.Controls[AppointmentControl.CustomerRdBtnName]).Checked;
            bool appointmentRdBtnChecked = ((RadioButton)appointmentCanvas.Controls[AppointmentControl.AppointmentRdBtnName]).Checked;

            if (customerRdBtnChecked)
            {
                UIFlow.CanvasRedirection(this, appointmentCanvas, addCustomerControl);
            }
            else if (appointmentRdBtnChecked)
            {
                UIFlow.CanvasRedirection(this, appointmentCanvas, addAppointmentControl);
            }
        }

        private void OnClickRedirect_UpdateControl(object sender, EventArgs e)
        {
            bool customerRdBtnChecked = ((RadioButton)appointmentCanvas.Controls[AppointmentControl.CustomerRdBtnName]).Checked;
            bool appointmentRdBtnChecked = ((RadioButton)appointmentCanvas.Controls[AppointmentControl.AppointmentRdBtnName]).Checked;
            Label errorLbl = (Label)appointmentCanvas.Controls[AppointmentControl.ErrorLblName];

            errorLbl.Text = string.Empty;
            if (customerRdBtnChecked)
            {
                DataGridViewSelectedRowCollection selectedRows =
                    ((DataGridView)appointmentCanvas.Controls[AppointmentControl.CustomerDataGridViewName]).SelectedRows;

                if (selectedRows.Count == 0)
                {
                    errorLbl.Text = AppointmentControl.ErrorMsgCustomerRowNotSelected;
                    return;
                }

                var selectedRow = selectedRows[0];
                this.customerSelectedRowIdx = selectedRow.Index;

                if (selectedRow.Cells[0].Value is null)
                {
                    errorLbl.Text = AppointmentControl.ErrorMsgCustomerEmptyRowSelected;
                    return;
                }

                updateCustomerControl.UpdateControls(selectedRow);

                UIFlow.CanvasRedirection(this, appointmentCanvas, updateCustomerControl);
            }
            else if (appointmentRdBtnChecked)
            {
                DataGridViewSelectedRowCollection selectedRows =
                    ((DataGridView)appointmentCanvas.Controls[AppointmentControl.AppoinmentDataGridViewName]).SelectedRows;

                if (selectedRows.Count == 0)
                {
                    errorLbl.Text = AppointmentControl.ErrorMsgAppointmentRowNotSelected;
                    return;
                }

                var selectedRow = selectedRows[0];
                this.appointmentSelectedRowIdx = selectedRow.Index;

                if (selectedRow.Cells[0].Value is null)
                {
                    errorLbl.Text = AppointmentControl.ErrorMsgAppointmentEmptyRowSelected;
                    return;
                }

                updateAppointmentControl.UpdateControls(selectedRow);

                UIFlow.CanvasRedirection(this, appointmentCanvas, updateAppointmentControl);
            }
        }


        private void OnClickRedirect_DeleteControl(object sender, EventArgs e)
        {
            bool customerRdBtnChecked = ((RadioButton)appointmentCanvas.Controls[AppointmentControl.CustomerRdBtnName]).Checked;
            bool appointmentRdBtnChecked = ((RadioButton)appointmentCanvas.Controls[AppointmentControl.AppointmentRdBtnName]).Checked;
            Label errorLbl = (Label)appointmentCanvas.Controls[AppointmentControl.ErrorLblName];

            errorLbl.Text = string.Empty;
            if (customerRdBtnChecked)
            {
                DataGridViewSelectedRowCollection selectedRows =
                    ((DataGridView)appointmentCanvas.Controls[AppointmentControl.CustomerDataGridViewName]).SelectedRows;

                if (selectedRows.Count == 0)
                {
                    errorLbl.Text = AppointmentControl.ErrorMsgCustomerRowNotSelected;
                    return;
                }

                var selectedRow = selectedRows[0];
                this.customerSelectedRowIdx = 0; // Reset

                int customerId = (int)selectedRow.Cells[0].Value;

                DialogResult result = MessageBox.Show($"Are you sure you want to delete this 'customer' record?\n[customerId : {customerId}]", "Deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (!result.Equals(DialogResult.OK))
                {
                    return;
                }

                DatabaseStatusPanel dbStatusPanel = (DatabaseStatusPanel)this.appointmentCanvas.Controls[AppointmentControl.DatabaseStatusPanelName];

                dbStatusPanel.Visible = true;
                dbStatusPanel.Processing();

                try
                {
                    AppController.DeleteRecord(UIState.State,
                        new DeleteDbMetaData(
                            ClientScheduleDbSchema._dbName,
                            TableName.Customer,
                            CustomerColumnName.CustomerId,
                            customerId)
                        );

                    dbStatusPanel.Success();

                    DataGridView customerDataGridView = (DataGridView)appointmentCanvas.Controls[AppointmentControl.CustomerDataGridViewName];
                    customerDataGridView.DataSource = UIState.State!.AppDataView.CustomerTableView;
                    customerDataGridView.Refresh();
                }
                catch (Exception)
                {
                    dbStatusPanel.Error();
                }
            }
            else if (appointmentRdBtnChecked)
            {
                DataGridViewSelectedRowCollection selectedRows =
                    ((DataGridView)appointmentCanvas.Controls[AppointmentControl.AppoinmentDataGridViewName]).SelectedRows;

                if (selectedRows.Count == 0)
                {
                    errorLbl.Text = AppointmentControl.ErrorMsgAppointmentRowNotSelected;
                    return;
                }

                var selectedRow = selectedRows[0];
                this.appointmentSelectedRowIdx = 0;

                int appointmentId = (int)selectedRow.Cells[0].Value;
                DialogResult result = MessageBox.Show($"Are you sure you want to delete this 'appointment' record?\n[appointmentId : {appointmentId}]", "Deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (!result.Equals(DialogResult.OK))
                {
                    return;
                }

                DatabaseStatusPanel dbStatusPanel = (DatabaseStatusPanel)this.appointmentCanvas.Controls[AppointmentControl.DatabaseStatusPanelName];

                dbStatusPanel.Visible = true;
                dbStatusPanel.Processing();

                try
                {
                    AppController.DeleteRecord(UIState.State,
                        new DeleteDbMetaData(
                            ClientScheduleDbSchema._dbName,
                            TableName.Appointment,
                            AppointmentColumnName.AppointmentId,
                            appointmentId)
                        );

                    dbStatusPanel.Success();

                    DataGridView appointmentDataGridView = (DataGridView)appointmentCanvas.Controls[AppointmentControl.AppoinmentDataGridViewName];
                    appointmentDataGridView.DataSource = UIState.State!.AppDataView.AppointmentTableView;
                    appointmentDataGridView.Refresh();
                }
                catch (Exception)
                {
                    dbStatusPanel.Error();
                }
            }
        }

        private void OnClickRedirect_ReportControl(object sender, EventArgs e)
        {
            UIFlow.CanvasRedirection(this, appointmentCanvas, reportControl);
        }

        private void UpdateBtn_LostFocus(object sender, EventArgs e)
        {
            Label errorLbl = (Label)appointmentCanvas.Controls[AppointmentControl.ErrorLblName];
            errorLbl.Text = String.Empty;
        }

        private void DeleteBtn_LostFocus(object sender, EventArgs e)
        {
            Label errorLbl = (Label)appointmentCanvas.Controls[AppointmentControl.ErrorLblName];
            errorLbl.Text = String.Empty;

            ((DatabaseStatusPanel)this.appointmentCanvas.Controls[AppointmentControl.DatabaseStatusPanelName]).Visible = false;
        }

        private void HandleException(object sender, EventArgs e)
        {
            if (sender is not null && sender.GetType().Equals(typeof(LoginCanvasControl)))
            {
                ((LoginCanvasControl)sender).ShowException(this);
            }
        }

        private void UpdateLocationLabel()
        {
            AppController.FetchLocationAndTimeZone(UIState.State);
            this.loginCanvas.Controls[LoginCanvasControl.LocationLblName].Text = UIState.State!.Location;
        }

        private void ScaleBackgroundImage(object sender, EventArgs e)
        {
            if (this.Width * 1 == this.Height * 1)
            {
                this.BackgroundImage = new Bitmap(Properties.AppBackgroundImages._3d_background_with_white_cubes_1_1, this.Width, this.Height);
            }
            else if (this.Width * 2 < this.Height * 3 && this.Width > this.Height)
            {
                this.BackgroundImage = new Bitmap(Properties.AppBackgroundImages._3d_background_with_white_cubes_3_2, this.Width, this.Height);
            }
            else if (this.Height * 3 > this.Width * 2 && this.Height > this.Width)
            {
                this.BackgroundImage = new Bitmap(Properties.AppBackgroundImages._3d_background_with_white_cubes_2_3, this.Width, this.Height);
            }
            else if (this.Height * 5 > this.Width * 4 && this.Height > this.Width)
            {
                this.BackgroundImage = new Bitmap(Properties.AppBackgroundImages._3d_background_with_white_cubes_4_5, this.Width, this.Height);
            }
            else if (this.Width * 3 < this.Height * 4 && this.Width > this.Height)
            {
                this.BackgroundImage = new Bitmap(Properties.AppBackgroundImages._3d_background_with_white_cubes_4_3, this.Width, this.Height);
            }
            else if (this.Width * 9 < this.Height * 16 && this.Width > this.Height)
            {
                this.BackgroundImage = new Bitmap(Properties.AppBackgroundImages._3d_background_with_white_cubes_16_9, this.Width, this.Height);
            }
            else if (this.Height * 16 > this.Width * 9 && this.Height > this.Width)
            {
                this.BackgroundImage = new Bitmap(Properties.AppBackgroundImages._3d_background_with_white_cubes_9_16, this.Width, this.Height);
            }
            else
            {
                this.BackgroundImage = this.BackgroundImage = new Bitmap(Properties.AppBackgroundImages._3d_background_with_white_cubes_16_9, this.Width, this.Height);
            }
        }
    }
}
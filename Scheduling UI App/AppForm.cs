using Scheduling_API.Controller.State;
using Scheduling_UI_App.UI_State;
using Scheduling_UI_Library;
using System.Data.Common;

// https://stackoverflow.com/questions/142003/cross-thread-operation-not-valid-control-accessed-from-a-thread-other-than-the
namespace Scheduling_UI_App
{
    internal partial class AppForm : Form
    {
        // Private
        private event EventHandler onException;
        private AppState? appState;

        // Public/Internal
        internal Exception? Exception { private set; get; }

        // Constructor
        public AppForm()
        {
            // 1) Create components
            InitializeComponent();

            // 2) Events subscription
            this.appStateBackgroundWorker.DoWork += AppStateBackgroundWorker_DoWork;
            this.appStateBackgroundWorker.RunWorkerCompleted += AppStateBackgroundWorker_RunWorkerCompleted;
            this.onException += handleException;
        }

        // Destructor
        ~AppForm()
        {
            // Events unsubscription
            this.appStateBackgroundWorker.DoWork -= AppStateBackgroundWorker_DoWork;
            this.appStateBackgroundWorker.RunWorkerCompleted -= AppStateBackgroundWorker_RunWorkerCompleted;
            this.onException -= handleException;
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            ChangeBackgroundColor(this.Controls);

            // Run worker asynchronously
            appStateBackgroundWorker.RunWorkerAsync();
        }

        private void AppStateBackgroundWorker_DoWork(object? sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // Execute UI updates on the UI thread
                loginCanvas.Invoke(() =>
                {
                    ShowControls(this.Controls, true); // show all the controls
                    ((LoadingControl)loginCanvas.Controls[LoadingControl.ControlName]).LoadingAnimation(true);
                });

                // 1) Initialiaze state
                this.appState = new UIState(AppDbInfo.MySqlClient, AppDbInfo.ClientScheduleDbName).appState;

                // assign this app state to the login control
                ((LoginControl)loginCanvas.Controls[LoginControl.ControlName]).AppState = this.appState;
            }
            catch (DbException dbExcp)
            {
                // Stored the specific exception on the form's expection property
                this.Exception = dbExcp;
                this.onException.Invoke(sender, new EventArgs());   // invoke the onException event handler
            }
            catch (InvalidOperationException invldOpExcp)
            {
                // Stored the specific exception on the form's expection property
                this.Exception = invldOpExcp;
                this.onException.Invoke(sender, new EventArgs());   // invoke the onException event handler
            }
            finally
            {
                // Execute UI updates on the UI thread
                loginCanvas.Invoke(() =>
                {
                    ((LoadingControl)loginCanvas.Controls[LoadingControl.ControlName]).LoadingAnimation(false);
                });
            }
        }

        private void AppStateBackgroundWorker_RunWorkerCompleted(object? sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }

        private void handleException(object? sender, EventArgs e)
        {
            if (sender is not null && sender.GetType().Equals(typeof(LoginCanvasControl)))
            {
                ((LoginCanvasControl)sender).ShowException(this);
            }
        }

        private void ShowControls(Control.ControlCollection controls, bool show)
        {
            foreach (Control control in controls)
            {
                if (show)
                {
                    control.Visible = show;  // true
                    control.Enabled = true;
                }
                else
                {
                    control.Visible = !show; // false
                    control.Enabled = false;
                }
            }
        }

        private void ChangeBackgroundColor(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.BackColor = Color.Transparent;
            }
        }
    }
}
using Scheduling_API.Controller.State;
using Scheduling_UI_App.Properties;
using Scheduling_UI_App.UI_State;
using Scheduling_UI_Library;
using System.Data.Common;
using System.Resources;

// https://stackoverflow.com/questions/142003/cross-thread-operation-not-valid-control-accessed-from-a-thread-other-than-the
namespace Scheduling_UI_App
{
    internal partial class AppForm : Form
    {
        // Private
        private event EventHandler onException;
        public event EventHandler onAuthencation;
        private AppState? appState;

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
            this.onException += handleException!;
            this.onAuthencation += AccessAppointment!;
            this.loginCanvas.Controls[LoginControl.ControlName].Controls["signInBtn"].Click += this.onAuthencation;
        }

        // Destructor
        ~AppForm()
        {
            // Events unsubscription
            this.appStateBackgroundWorker.DoWork -= AppStateBackgroundWorker_DoWork!;
            this.appStateBackgroundWorker.RunWorkerCompleted -= AppStateBackgroundWorker_RunWorkerCompleted!;
            this.onException -= handleException!;
            this.onAuthencation -= AccessAppointment!;
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            ShowControls(this.Controls, false);

            ChangeBackgroundColor(this.Controls);

            // Run worker asynchronously
            appStateBackgroundWorker.RunWorkerAsync();
        }

        private void AppStateBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // Execute UI updates on the UI thread
                Invoke(() =>
                {
                    ShowControls(loginCanvas, true); // show all the controls
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
                Invoke(() =>
                {
                    ((LoadingControl)loginCanvas.Controls[LoadingControl.ControlName]).LoadingAnimation(false);
                });
            }
        }

        private void AppStateBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }

        private void AccessAppointment(object sender, EventArgs e)
        {
            if (((LoginControl)this.loginCanvas.Controls[LoginControl.ControlName]).authenticated is not true)
            {
                return;
            }


            Invoke(() =>
            {
                loginCanvas.Dispose();
            });

            Invoke(() =>
            {
                appointmentCanvas.Visible = true;
                ShowControls(appointmentCanvas, true);
            });
        }

        private void handleException(object sender, EventArgs e)
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
                    control.Visible = true;
                    control.Enabled = true;
                }
                else
                {
                    control.Visible = false;
                    control.Enabled = false;
                }
            }
        }

        private void ShowControls(Control control, bool show)
        {
            if (show)
            {
                control.Visible = true;
                control.Enabled = true;
            }
            else
            {
                control.Visible = false;
                control.Enabled = false;
            }
        }

        private void ChangeBackgroundColor(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.BackColor = Color.Transparent;
            }
        }

        private void ScaleBackgroundImage(object sender, EventArgs e)
        {
            if (this.Width * 1 == this.Height * 1)
            {
                this.BackgroundImage = new Bitmap(Properties.Resources.Section_099_ThankYou_BG_Img_Ornament_Ratio_1_1_Square, this.Width, this.Height);
            }
            else if (this.Width * 2 < this.Height * 3 && this.Width > this.Height)
            {
                this.BackgroundImage = new Bitmap(Properties.Resources.Section_099_ThankYou_BG_Img_Ornament_Ratio_3_2, this.Width, this.Height);
            }
            else if (this.Height * 3 > this.Width * 2 && this.Height > this.Width)
            {
                this.BackgroundImage = new Bitmap(Properties.Resources.Section_099_ThankYou_BG_Img_Ornament_Ratio_2_3, this.Width, this.Height);
            }
            else if (this.Height * 5 > this.Width * 4 && this.Height > this.Width)
            {
                this.BackgroundImage = Properties.Resources.Section_099_ThankYou_BG_Img_Ornament_Ratio_4_5_Portrait;
            }
            else if (this.Width * 3 < this.Height * 4 && this.Width > this.Height)
            {
                this.BackgroundImage = new Bitmap(Properties.Resources.Section_099_ThankYou_BG_Img_Ornament_Ratio_4_3, this.Width, this.Height);
            }
            else if (this.Width * 9 < this.Height * 16 && this.Width > this.Height)
            {
                this.BackgroundImage = new Bitmap(Properties.Resources.Section_099_ThankYou_BG_Img_Ornament_Ratio_16_9_Landscape, this.Width, this.Height);
            }
            else if (this.Height * 16 > this.Width * 9 && this.Height > this.Width)
            {
                this.BackgroundImage = new Bitmap(Properties.Resources.Section_099_ThankYou_BG_Img_Ornament_Ratio_9_16_Story, this.Width, this.Height);
            }
            else
            {
                this.BackgroundImage = this.BackgroundImage = new Bitmap(Properties.Resources.Section_099_ThankYou_BG_Img_Ornament_Ratio_16_9_Landscape, this.Width, this.Height);
            }
        }
    }
}
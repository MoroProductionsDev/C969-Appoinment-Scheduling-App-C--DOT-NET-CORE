using Scheduling_API.Controller.State;
using Scheduling_UI_App.UI_State;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using Scheduling_UI_App.UI_StyleUtility;

namespace Scheduling_UI_App
{
    internal partial class LoginCanvasControl : UserControl
    {
        internal LoginCanvasControl()
        {
            // 1) Create Components
            InitializeComponent();
        }

        internal void ShowException(AppForm appform)
        {
            DialogResult result;
            using (new CenterWinDialog(this))
            {
                result = MessageBox.Show(appform.Exception?.Message, "Exception",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
            }


            // If the no button was pressed ...
            if (result == DialogResult.OK)
            {
                Thread.Sleep(2000);
                // cancel the closure of the form.
                Program.Abort();
            }
            errorLbl.Text = appform.Exception?.Message;
        }
    }
}
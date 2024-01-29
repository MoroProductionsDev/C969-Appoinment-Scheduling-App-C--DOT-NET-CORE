using Scheduling_API.Controller;
using Scheduling_API.Controller.Process;
using Scheduling_UI_App.UI_Process;
using System.Data;
using System.Reflection;

namespace Scheduling_UI_App
{
    // Control use to display specific data reports to the user
    public partial class ReportControl : UserControl
    {
        public const string returnBtnName = nameof(returnBtn);
        public ReportControl()
        {
            InitializeComponent();
        }

        private void ReportControl_Load(object sender, EventArgs e)
        {
            FieldInfo[] fInfo = typeof(Report.Type).GetFields();
            foreach (var reportType in fInfo)
            {
                if (reportType.Name.Equals("value__"))
                {
                    continue;
                }
                reportComboBox.Items.Add(reportType.Name);
            }
        }

        private void ReportComboBox_ValueChanged(object sender, EventArgs e)
        {
            if (reportComboBox.SelectedIndex == 0)
            {
                AppController.GenerateReport(UIState.State, Report.Type.AppointmentMonthlyTypes);
            }
            else if (reportComboBox.SelectedIndex == 1)
            {
                AppController.GenerateReport(UIState.State, Report.Type.ConsultantSchedule);
            }
            else
            {
                AppController.GenerateReport(UIState.State, Report.Type.LocationSchedule);
            }
            DataTable reportTalbe = UIState.State!.Report.ReportTable;

            reportDataGridView.DataSource = reportTalbe;
            reportDataGridView.Refresh();

            //SetUnboundDataGridView();
        }
    }
}

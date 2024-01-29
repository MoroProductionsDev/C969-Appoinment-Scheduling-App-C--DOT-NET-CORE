namespace Scheduling_UI_Library
{
    // It updates the UI with information after the user's database updates attempts.
    public partial class DatabaseStatusPanel : UserControl
    {
        public DatabaseStatusPanel()
        {
            InitializeComponent();
        }

        public void Processing()
        {
            databaseResponsepictureBox.Image = Properties.Resources.hourglass;
            dataBaseUpdateMsgLbl.Text = "Processing: Updating database.";
        }

        public void Success()
        {
            databaseResponsepictureBox.Image = Properties.Resources.check;
            dataBaseUpdateMsgLbl.Text = "Completed: Database updated.";
        }

        public void Error()
        {
            databaseResponsepictureBox.Image = Properties.Resources.remove;
            dataBaseUpdateMsgLbl.Text = "Error: Database transaction error.";
        }
    }
}

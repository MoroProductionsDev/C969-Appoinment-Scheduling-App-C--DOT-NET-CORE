using Scheduling_API.Controller;
using Scheduling_UI_App.UI_Process;
using Scheduling_UI_Library.UI_Validator;

namespace Scheduling_UI_App
{
    // The add customer's record control
    public partial class AddCustomerControl : UserControl
    {
        public const string returnBtnName = nameof(returnBtn);
        public AddCustomerControl()
        {
            InitializeComponent();
        }

        protected void OnTxtBoxFocus(object sender, EventArgs args)
        {
            TextBox txtBox = (TextBox)sender;

            if (txtBox.Text.Equals(String.Empty))
            {
                txtBox.ForeColor = Color.Black;
                txtBox.BackColor = Color.White;
                txtBox.PlaceholderText = string.Empty;
            }
        }

        protected void OnTxtBoxLostFocus(object sender, EventArgs args)
        {
            TextBox txtBox = (TextBox)sender;
            string errorMsg = ControlValidator.ValidateTxtBox(txtBox);

            if (!errorMsg.Equals(string.Empty))
            {
                txtBox.ForeColor = Color.White;
                txtBox.BackColor = Color.Orange;
                txtBox.PlaceholderText = errorMsg;
            }
        }

        private void UpdateBtn_LostFocus(object sender, EventArgs e)
        {
            databaseStatusPanel.Visible = false;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            bool isItEmpty = false;
            foreach (TextBox txtBox in this.Controls.OfType<TextBox>())
            {
                if (txtBox.Text.Equals(string.Empty) && !txtBox.Name.Equals(nameof(customerAddress2TxtBox)))
                {
                    isItEmpty = true;
                }
            }

            if (!isItEmpty)
            {
                databaseStatusPanel.Visible = true;
                databaseStatusPanel.Processing();

                UIState.State!.AppData.CustomerRecord.CustomerName = customerNameTxt.Text;
                UIState.State!.AppData.AddressRecord.Phone = customerPhoneTxtBox.Text;
                UIState.State!.AppData.AddressRecord.Address = customerAddressTxtBox.Text;
                UIState.State!.AppData.AddressRecord.Address2 = customerAddress2TxtBox.Text;
                UIState.State!.AppData.CityRecord.City = customerCityTxtBox.Text;
                UIState.State!.AppData.CountryRecord.Country = customerCountryTxtBox.Text;


                try
                {
                    AppController.AddCustomerRecord(UIState.State);
                }
                catch (Exception)
                {
                    databaseStatusPanel.Error();
                }
                databaseStatusPanel.Success();
            }
        }
    }
}

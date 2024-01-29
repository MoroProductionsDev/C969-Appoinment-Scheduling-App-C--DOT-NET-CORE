using Scheduling_API.Controller;
using Scheduling_Logic.Model.Data;
using Scheduling_Logic.Model.Structure;
using Scheduling_UI_App.UI_Process;
using Scheduling_UI_Library.UI_Validator;
using static Scheduling_Logic.Model.Structure.ClientScheduleDbSchema;

namespace Scheduling_UI_App
{
    // The update customer's record control
    public partial class UpdateCustomerControl : UserControl
    {
        const int firstColIdx = 1; // ignore the primaryKey
        public const string returnBtnName = nameof(returnBtn);
        private DataGridViewRow? selectedRow;
        private bool isColumnComboBoxFilled = false;
        public UpdateCustomerControl()
        {
            InitializeComponent();
        }

        private void UpdateCustomerControl_Load(object sender, EventArgs e)
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
            this.currentValueTxtBox.Text = this.selectedRow.Cells[firstColIdx].Value.ToString();

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
                this.columnComboBox.Items.Add(selectedColumns[idx].Name);
            }

            this.columnComboBox.SelectedIndex = 0;
            this.isColumnComboBoxFilled = true;
        }

        private void ColumnComboBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateComponent();
        }

        private void UpdateComponent()
        {
            if (this.selectedRow is not null)
            {
                int comboBoxSelectedIdx = firstColIdx + columnComboBox.SelectedIndex;
                this.currentValueTxtBox.Text = this.selectedRow.Cells[comboBoxSelectedIdx].Value.ToString();
            }
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

            if (columnComboBox.SelectedItem.Equals(AddressColumnName.Address2))
            {
                return;
            }

            string errorMsg = ControlValidator.ValidateTxtBox(txtBox);

            if (!errorMsg.Equals(string.Empty))
            {
                txtBox.ForeColor = Color.White;
                txtBox.BackColor = Color.Orange;
                txtBox.PlaceholderText = errorMsg;
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            // Address can be blank so it does not need empty space validation
            if (!this.newValueTxtBox.Text.Equals(String.Empty))
            {
                UpdateProcessing();
            }
        }

        private void UpdateBtn_LostFocus(object sender, EventArgs e)
        {
            databaseStatusPanel.Visible = false;
        }

        private void UpdateProcessing()
        {
            databaseStatusPanel.Visible = true;
            databaseStatusPanel.Processing();

            int rowIdx = selectedRow!.Index;
            string columnName = columnComboBox.SelectedItem.ToString()!;
            UpdateDbMetaData<string> updateDbMetaData;

            if (ClientScheduleDbSchema.GetCustomerColumnNames().Contains(columnName))
            {
                int customerIdPK = (int)UIState.State!.DbDataSet.DataSet.Tables[TableName.Customer]!.Rows[rowIdx][CustomerColumnName.CustomerId];

                updateDbMetaData = new UpdateDbMetaData<string>(
                                    _dbName: ClientScheduleDbSchema._dbName,
                                    _tableName: TableName.Customer,
                                    _valueColumnName: columnName,
                                    _currentValue: this.currentValueTxtBox.Text,
                                    _newValue: this.newValueTxtBox.Text,
                                    _idColumnName: CustomerColumnName.CustomerId,
                                    _idValue: customerIdPK);
            }
            else if (ClientScheduleDbSchema.GetAddressColumnNames().Contains(columnName))
            {
                int addressIdFK = (int)UIState.State!.DbDataSet.DataSet.Tables[TableName.Address]!.Rows[rowIdx][CustomerColumnName.AddressId];

                updateDbMetaData = new UpdateDbMetaData<string>(
                                    _dbName: ClientScheduleDbSchema._dbName,
                                   _tableName: TableName.Address,
                                   _valueColumnName: columnName,
                                   _currentValue: this.currentValueTxtBox.Text,
                                   _newValue: this.newValueTxtBox.Text,
                                   _idColumnName: CustomerColumnName.AddressId,
                                   _idValue: addressIdFK);
            }
            else if (ClientScheduleDbSchema.GetCityColumnNames().Contains(columnName))
            {
                int cityIdFK = (int)UIState.State!.DbDataSet.DataSet.Tables[TableName.City]!.Rows[rowIdx][AddressColumnName.CityId];

                updateDbMetaData = new UpdateDbMetaData<string>(
                                    _dbName: ClientScheduleDbSchema._dbName,
                                   _tableName: TableName.City,
                                   _valueColumnName: columnName,
                                   _currentValue: this.currentValueTxtBox.Text,
                                   _newValue: this.newValueTxtBox.Text,
                                   _idColumnName: AddressColumnName.CityId,
                                   _idValue: cityIdFK);
            }
            else
            {
                int countryIdFK = (int)UIState.State!.DbDataSet.DataSet.Tables[TableName.Country]!.Rows[rowIdx][CityColumnName.CountryId];

                updateDbMetaData = new UpdateDbMetaData<string>(
                                    _dbName: ClientScheduleDbSchema._dbName,
                                   _tableName: TableName.Country,
                                   _valueColumnName: columnName,
                                   _currentValue: this.currentValueTxtBox.Text,
                                   _newValue: this.newValueTxtBox.Text,
                                   _idColumnName: CityColumnName.CountryId,
                                   _idValue: countryIdFK);
            }

            try
            {
                AppController.UpdateRecord(UIState.State, updateDbMetaData);

                // Update UI
                databaseStatusPanel.Success();
                selectedRow!.Cells[0].Selected = false;
                this.selectedRow!.Cells[columnComboBox.SelectedIndex + firstColIdx].Value = newValueTxtBox.Text;
                this.currentValueTxtBox.Text = newValueTxtBox.Text;
                this.newValueTxtBox.ResetText();
            }
            catch (Exception)
            {
                databaseStatusPanel.Error();
            }
        }
    }
}

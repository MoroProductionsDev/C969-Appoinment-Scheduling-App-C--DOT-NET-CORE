namespace Scheduling_UI_Library.UI_Process
{

    // UI components processing class
    public static class UIComponent
    {
        const int firstCellPkIdx = 0;
        public static void SetBindingSources(Control control, string propertyName, BindingSource bindingSource)
        {
            if (control.GetType().Equals(typeof(Label)))
            {
                control.DataBindings.Add("Text", bindingSource, propertyName);
            }
        }

        public static void EnableControls(Control uiControl, Control.ControlCollection controls, bool enable)
        {
            // Execute UI updates on the UI thread
            uiControl.Invoke(() =>
            {
                foreach (Control control in controls)
                {
                    if (enable)
                    {

                        control.Enabled = true;
                    }
                    else
                    {
                        control.Enabled = false;
                    }
                }
            });
        }

        public static void EnableControl(Control uiControl, Control control, bool enable)
        {
            // Execute UI updates on the UI thread
            uiControl.Invoke(() =>
            {
                if (enable)
                {
                    control.Enabled = true;
                }
                else
                {
                    control.Enabled = false;
                }
            });
        }

        public static void ChangeBackgroundColor(Control uiControl, Control.ControlCollection controls)
        {
            // Execute UI updates on the UI thread
            uiControl.Invoke(() =>
            {
                foreach (Control control in controls)
                {
                    control.BackColor = Color.Transparent;
                }
            });
        }

        public static void SelectRowOnTableIdx(DataGridView grid, int tableId)
        {
            Nullable<int> tableRowId;
            foreach (DataGridViewRow row in grid.Rows)
            {
                tableRowId = row.Cells[firstCellPkIdx].Value as Nullable<int>;
                if (tableRowId is not null &&
                    tableRowId == tableId)
                {
                    var selectedDgvCellStyle = new DataGridViewCellStyle { BackColor = System.Drawing.Color.Yellow };
                    StyleDataGridViewCells(grid.Rows[(int)tableRowId! - 1].Cells, selectedDgvCellStyle);
                }
                else
                {
                    var unselectedDgvCellStyle = new DataGridViewCellStyle { BackColor = System.Drawing.Color.White };
                    StyleDataGridViewCells(grid.Rows[(int)tableRowId! - 1].Cells, unselectedDgvCellStyle);
                }
            }
        }

        private static void StyleDataGridViewCells(DataGridViewCellCollection dgvCells, DataGridViewCellStyle dgvCellStyle)
        {
            foreach (DataGridViewCell cell in dgvCells)
            {
                cell.Style = dgvCellStyle;
            }
        }
    }
}

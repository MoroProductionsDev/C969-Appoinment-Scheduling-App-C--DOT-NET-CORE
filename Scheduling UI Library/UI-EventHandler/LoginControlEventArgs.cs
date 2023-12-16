using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_UI_Library.UI_EventHandler
{
    internal class LoginControlEventArgs : EventArgs
    {
        public ToolTip toolTip { private set; get; }
        internal LoginControlEventArgs(ToolTip toolTip)
        {
            this.toolTip = toolTip;
        }
    }
}

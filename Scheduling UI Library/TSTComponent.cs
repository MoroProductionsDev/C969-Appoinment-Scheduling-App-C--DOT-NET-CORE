using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling_UI_Library
{
    public partial class TSTComponent : Component
    {
        public TSTComponent()
        {
            InitializeComponent();
        }

        public TSTComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}

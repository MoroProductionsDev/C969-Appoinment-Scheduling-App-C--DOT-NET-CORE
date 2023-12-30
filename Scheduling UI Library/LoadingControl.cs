using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduling_UI_Library
{
    public partial class LoadingControl : UserControl
    {
        public const string ControlName = nameof(LoadingControl);
        private event CancelEventHandler onSpinnerAnimation;
        private event CancelEventHandler onLoadingTextAnimation;
        private CancelEventArgs cancelE;

        Bitmap[] spinnerStageMap = new Bitmap[]
        {
            Properties.Resources.spinner_frame__1_,
            Properties.Resources.spinner_frame__2_,
            Properties.Resources.spinner_frame__3_,
            Properties.Resources.spinner_frame__4_,
            Properties.Resources.spinner_frame__5_,
            Properties.Resources.spinner_frame__6_,
            Properties.Resources.spinner_frame__7_,
            Properties.Resources.spinner_frame__8_,
            Properties.Resources.spinner_frame__9_,
            Properties.Resources.spinner_frame__10_,
            Properties.Resources.spinner_frame__11_,
            Properties.Resources.spinner_frame__12_,
            Properties.Resources.spinner_frame__13_,
            Properties.Resources.spinner_frame__14_,
            Properties.Resources.spinner_frame__15_,
            Properties.Resources.spinner_frame__16_,
            Properties.Resources.spinner_frame__17_,
            Properties.Resources.spinner_frame__18_,
            Properties.Resources.spinner_frame__19_,
            Properties.Resources.spinner_frame__20_,
            Properties.Resources.spinner_frame__21_,
            Properties.Resources.spinner_frame__22_,
            Properties.Resources.spinner_frame__23_,
            Properties.Resources.spinner_frame__24_,
            Properties.Resources.spinner_frame__25_,
            Properties.Resources.spinner_frame__26_,
            Properties.Resources.spinner_frame__27_,
            Properties.Resources.spinner_frame__28_,
            Properties.Resources.spinner_frame__29_,
            Properties.Resources.spinner_frame__30_
        };
        public LoadingControl()
        {
            InitializeComponent();

            this.onSpinnerAnimation += StartSpinnerAnimation!;
            this.onLoadingTextAnimation += StartLoadingTextAnimation!;
            this.cancelE = new CancelEventArgs();
        }

        public void LoadingAnimation(bool loading)
        {
            if (loading)
            {
                this.cancelE.Cancel = false;
                this.onSpinnerAnimation.Invoke(this, cancelE);
                this.onLoadingTextAnimation.Invoke(this, cancelE);
            }
            else
            {
                this.cancelE.Cancel = true;
                this.onSpinnerAnimation.Invoke(this, cancelE);
                this.onLoadingTextAnimation.Invoke(this, cancelE);
            }
        }

        private void LoadingControl_Load(object sender, EventArgs e)
        {
            this.loadingPictureBox.Hide();
        }

        private async void StartSpinnerAnimation(object sender, CancelEventArgs e)
        {
            const int spinnerFrameDelayMilliseconds = 20;

            while (!e.Cancel)
            {
                this.loadingPictureBox.Show();
                for (int i = 0; i < spinnerStageMap.Length; i++)
                {
                    await Task.Delay(spinnerFrameDelayMilliseconds);
                    this.loadingPictureBox.Image = spinnerStageMap[i];
                }
            }

            if (e.Cancel)
            {
                this.loadingPictureBox.Hide();
            }
        }

        private async void StartLoadingTextAnimation(object sender, CancelEventArgs e)
        {
            const int labelFrameDelayMilliseconds = 500;
            Padding labelPadding = this.loadingLbl.Padding;
            int initialLeftPadding = this.loadingLbl.Padding.Left;

            while (!e.Cancel)
            {
                this.loadingLbl.Show();

                this.loadingLbl.Text = "Loading";
                labelPadding.Left = initialLeftPadding;
                this.loadingLbl.Padding = labelPadding;

                for (int i = 0; i < spinnerStageMap.Length; i++)
                {
                    await Task.Delay(labelFrameDelayMilliseconds);
                    
                    this.loadingLbl.Text += ".";
                    labelPadding.Left -= 1;
                    this.loadingLbl.Padding = labelPadding;
                }
            }

            if (e.Cancel)
            {
                this.loadingLbl.Hide();
            }
        }
    }
}

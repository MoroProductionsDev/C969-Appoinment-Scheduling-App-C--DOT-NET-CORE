using Scheduling_UI_Library;

namespace Scheduling_UI_App.UI_Process
{
    // UI animation processing class
    public static class UIAnimation
    {
        public static void LoadingAnimation(Control uiControl, Control processControl, bool loading)
        {
            // Execute UI updates on the UI thread
            uiControl.Invoke(() =>
            {
                ((LoadingControl)processControl.Controls[nameof(LoadingControl)]).LoadingAnimation(loading);
            });
        }
    }
}

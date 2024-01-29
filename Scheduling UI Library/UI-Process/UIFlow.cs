namespace Scheduling_UI_App.UI_Process
{
    // UI control and form redirection processing class
    public static class UIFlow
    {
        public static void CanvasRedirection(Control uiControl, Control previousCanvas, Control currentCanvas)
        {
            uiControl.Invoke(() =>
            {
                uiControl.Size = currentCanvas.Size;

                previousCanvas.Visible = false;
                currentCanvas.Visible = true;
            });
        }

        public static void CanvasDisposal(Control uiControl, Control currentCanvas)
        {
            uiControl.Invoke(() =>
            {
                currentCanvas.Controls.Remove(currentCanvas);
                currentCanvas.Dispose();
            });
        }
    }
}

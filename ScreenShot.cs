using System.Drawing;
using System.Drawing.Imaging;


namespace ImageSearchSharp
{
    internal static class ScreenShot
    {
        public static Bitmap CaptureScreen()
        {
            Bitmap image = new(
                Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            
            var gfx = Graphics.FromImage(image);
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return image;
        }
    }
}

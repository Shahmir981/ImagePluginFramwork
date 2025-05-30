using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

public class ResizeEffect : IImageEffect
{
    public string Name => "ResizeEffect";

    public void Apply(ImageData image, object? parameter = null)
    {
        if (parameter is not int newSize || image.Content == null)
            throw new ArgumentException("Invalid parameter or image content is null.");

        using var ms = new MemoryStream(image.Content);
        using var original = new Bitmap(ms);

        int width, height;
        if (original.Width >= original.Height)
        {
            width = newSize;
            height = (original.Height * newSize) / original.Width;
        }
        else
        {
            height = newSize;
            width = (original.Width * newSize) / original.Height;
        }

        using var resized = new Bitmap(width, height);
        using (var graphics = Graphics.FromImage(resized))
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawImage(original, 0, 0, width, height);
        }

        using var outputMs = new MemoryStream();
        resized.Save(outputMs, ImageFormat.Png); // or .Jpeg, etc.
        image.Content = outputMs.ToArray();
        image.Width = width;
        image.Height = height;
    }
}

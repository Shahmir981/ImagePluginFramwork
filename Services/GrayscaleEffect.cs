using System;
using ImagePluginFrameworkBestPractice.Models;

namespace ImagePluginFrameworkBestPractice.Services
{
    public class GrayscaleEffect : IImageEffect
    {
        public string Name => "GrayscaleEffect";

        public void Apply(ImageData image, object? parameter = null)
        {
            Console.WriteLine($"[Grayscale] Applied to {image.Id}");
        }
    }
}

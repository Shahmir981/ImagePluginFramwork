using System;
using ImagePluginFrameworkBestPractice.Models;

namespace ImagePluginFrameworkBestPractice.Services
{
    public class BlurEffect : IImageEffect
    {
        public string Name => "BlurEffect";

        public void Apply(ImageData image, object? parameter = null)
        {
            if (parameter is int radius)
                Console.WriteLine($"[Blur] Applied to {image.Id} with radius: {radius}");
            else
                throw new ArgumentException("Invalid parameter for BlurEffect.");
        }
    }
}

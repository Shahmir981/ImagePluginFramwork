using ImagePluginFrameworkBestPractice.Models;

namespace ImagePluginFrameworkBestPractice.Services
{
    public interface IImageEffect
    {
        string Name { get; }
        void Apply(ImageData image, object? parameter = null);
    }
}

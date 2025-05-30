using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImagePluginFrameworkBestPractice.Models;

namespace ImagePluginFrameworkBestPractice.Services
{
    public class ImageProcessor
    {
        private readonly PluginManager _pluginManager;

        public ImageProcessor(PluginManager pluginManager)
        {
            _pluginManager = pluginManager ?? throw new ArgumentNullException(nameof(pluginManager));
        }

        public async Task ApplyEffectsAsync(ImageData image, List<(string EffectName, object Parameter)> effects)
        {
            foreach (var (name, param) in effects)
            {
                var effect = _pluginManager.GetEffect(name);
                await Task.Run(() => effect.Apply(image, param));
            }
        }
    }
}

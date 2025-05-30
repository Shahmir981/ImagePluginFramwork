using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImagePluginFrameworkBestPractice.Models
{
    public class ImageEffectRequest
    {
        [Required]
        public ImageData ImageData { get; set; } = null!;

        [Required]
        public List<(string EffectName, object Parameter)> Effects { get; set; }

        public ImageEffectRequest()
        {
            Effects = new List<(string EffectName, object Parameter)>();
        }
    }
}

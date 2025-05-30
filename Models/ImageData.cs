using System;
using System.ComponentModel.DataAnnotations;

namespace ImagePluginFrameworkBestPractice.Models
{
    public class ImageData
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? Description { get; set; }

        public byte[]? Content { get; set; }

        public string? FileName { get; set; }

        public string? Format { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }
    }
}

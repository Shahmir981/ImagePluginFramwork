using System;
using System.Linq;
using System.Threading.Tasks;
using ImagePluginFrameworkBestPractice.Models;
using ImagePluginFrameworkBestPractice.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImagePluginFrameworkBestPractice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ImageProcessor _processor;

        public ImagesController(ImageProcessor processor)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }
       [HttpPost("apply-effects")]
       public async Task<IActionResult> ApplyEffectsAsync([FromBody] ImageEffectRequest request)
       {
         if (request == null)
            return BadRequest("Request cannot be null.");

          if (request.ImageData == null)
              return BadRequest("Image data cannot be null.");

           if (request.Effects == null || !request.Effects.Any())
             return BadRequest("No effects specified.");
      try {

        await _processor.ApplyEffectsAsync(request.ImageData, request.Effects);

        if (request.ImageData.Content == null || request.ImageData.Content.Length == 0)
            return StatusCode(500, "Image processing failed. Content is empty.");

        var base64 = Convert.ToBase64String(request.ImageData.Content);
        var mimeType = GetMimeType(request.ImageData.Format ?? "png"); // default to PNG

        return Ok(new
        {
            message = "Effects applied successfully.",
            imageId = request.ImageData.Id,
            base64Image = $"data:{mimeType};base64,{base64}"
        });
         }  catch (KeyNotFoundException ex)  {
        return NotFound(ex.Message);
    } catch (ArgumentException ex) {
        return BadRequest(ex.Message);
    } catch (Exception ex) {
        return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
    }
    }
  }
}

using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/texts")]
    public class TextsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public TextsController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTextsAsync()
        {
            var texts = await _manager.TextService.GetAllTextsAsync(false);
            return Ok(texts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneTextAsync([FromRoute(Name = "id")] int id)
        {
            var text = await _manager.TextService.GetOneTextByIdAsync(id, false);
            return Ok(text);
        }


        [Authorize(Roles = "User")]
        [HttpPost]

        public async Task<IActionResult> CreateOneTextAsync([FromBody] TextDtoForInsertion textDto)
        {
            if (textDto is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            // ✅ Kullanıcının Id’sini token içinden al
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;


            if (string.IsNullOrEmpty(userId))
                return Unauthorized(); // Kullanıcı doğrulanamamışsa

            var text = await _manager.TextService.CreateOneTextAsync(textDto, userId);
            return StatusCode(201, text);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneTextAsync([FromRoute(Name = "id")] int id,
            [FromBody] TextDtoForUpdate textDto)
        {
            if (textDto is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.TextService.UpdateOneTextAsync(id, textDto, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneTextAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.TextService.DeleteOneTextAsync(id, false);
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneTextAsync([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<TextDtoForUpdate> textPatch)
        {
            if (textPatch is null)
                return BadRequest();

            var result = await _manager.TextService.GetOneTextForPatchAsync(id, false);

            textPatch.ApplyTo(result.textDtoForUpdate, ModelState);

            TryValidateModel(result.textDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.TextService.SaveChangesForPatchAsync(result.textDtoForUpdate, result.text);

            return NoContent();
        }

        [Authorize]
        [HttpGet("my-texts")]
        public async Task<IActionResult> GetMyTextsAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var texts = await _manager.TextService.GetTextsByUserIdAsync(userId, false);
            return Ok(texts);
        }
        [Authorize(Roles = "User")]
        [HttpPost("{id:int}/upload-photo")]
        public async Task<IActionResult> UploadPhoto(int id, IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                return BadRequest(new { error = "Photo is required." });

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            // Text entity'yi çekiyoruz (entity çünkü UserId kontrolü için lazım)
            var textEntity = await _manager.TextService.GetOneTextByIdAsync(id, trackChanges: true);
            if (textEntity == null)
                return NotFound();

            var textentity = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            try
            {
                await _manager.TextService.UploadPhotoAsync(id, photo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id:int}/download-photo")]
        public async Task<IActionResult> DownloadPhoto(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            // Text entity'yi trackChanges: false olarak alalım
            var text = await _manager.TextService.GetOneTextByIdAsync(id, false);

            if (text == null)
                return NotFound(new { error = "Text bulunamadı." });

            

            try
            {
                var (photoData, contentType) = await _manager.TextService.GetPhotoByTextIdAsync(id);

                if (photoData == null || contentType == null)
                    return NotFound(new { error = "Fotoğraf bulunamadı." });

                return File(photoData, contentType, $"photo_{id}");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }


        }
        [Authorize]
        [HttpGet("my-photos")]
        public async Task<IActionResult> GetMyUploadedPhotosAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var photos = await _manager.TextService.GetAllTextPhotosByUserIdAsync(userId, trackChanges: false);

            if (photos == null || !photos.Any())
                return NotFound(new { message = "Yüklenmiş fotoğraf bulunamadı." });

            return Ok(photos);
        }
    }
}
    

    
    

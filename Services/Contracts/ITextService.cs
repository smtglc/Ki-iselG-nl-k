using Entities.DataTransferObjects;
using Entities.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ITextService
    {
        Task<IEnumerable<TextDto>> GetAllTextsAsync(bool trackChanges);
        Task<TextDto> GetOneTextByIdAsync(int id, bool trackChanges);
        Task<TextDto> CreateOneTextAsync(TextDtoForInsertion textDto,string userId);
        Task UpdateOneTextAsync(int id, TextDtoForUpdate textDto, bool trackChanges);
        Task DeleteOneTextAsync(int id, bool trackChanges);

        Task<(TextDtoForUpdate textDtoForUpdate, Text text)> GetOneTextForPatchAsync(int id, bool trackChanges);
        Task SaveChangesForPatchAsync(TextDtoForUpdate textDtoForUpdate, Text text);

        Task<IEnumerable<TextDto>> GetTextsByUserIdAsync(string userId, bool trackChanges);
        Task UploadPhotoAsync(int textId, IFormFile photo);
        Task<(byte[] PhotoData, string ContentType)> GetPhotoByTextIdAsync(int textId);
        Task<IEnumerable<TextPhotoDto>> GetAllTextPhotosByUserIdAsync(string userId, bool trackChanges);




    }
}

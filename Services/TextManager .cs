using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TextManager :ITextService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerServices _logger;
        private readonly IMapper _mapper;

        public TextManager(IRepositoryManager repository,
            ILoggerServices logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TextDto> CreateOneTextAsync(TextDtoForInsertion textDto ,string userId)
        {
            var entity = _mapper.Map<Text>(textDto);
            // UserId'yi set et
            entity.UserId = userId;
            _repository.Text.CreateOneText(entity);
            await _repository.SaveAsync();
            return _mapper.Map<TextDto>(entity);
        }

        public async Task DeleteOneTextAsync(int id, bool trackChanges)
        {
            var entity = await _repository.Text.GetOneTextByIdAsync(id, trackChanges);

            if (entity is null)
                throw new TextNotFoundException(id);

            _repository.Text.DeleteOneText(entity);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<TextDto>> GetAllTextsAsync(bool trackChanges)
        {
            var texts = await _repository.Text.GetAllTextBooksAsync(trackChanges);
            return _mapper.Map<IEnumerable<TextDto>>(texts);
        }

        public async Task<TextDto> GetOneTextByIdAsync(int id, bool trackChanges)
        {
            var text = await _repository.Text.GetOneTextByIdAsync(id, trackChanges);

            if (text is null)
                throw new TextNotFoundException(id);

            return _mapper.Map<TextDto>(text);
        }

        public async Task<(TextDtoForUpdate textDtoForUpdate, Text text)> GetOneTextForPatchAsync(int id, bool trackChanges)
        {
            var text = await _repository.Text.GetOneTextByIdAsync(id, trackChanges);

            if (text is null)
                throw new TextNotFoundException(id);

            var textDtoForUpdate = _mapper.Map<TextDtoForUpdate>(text);

            return (textDtoForUpdate, text);
        }

        public async Task SaveChangesForPatchAsync(TextDtoForUpdate textDtoForUpdate, Text text)
        {
            _mapper.Map(textDtoForUpdate, text);
            await _repository.SaveAsync();
        }

        public async Task UpdateOneTextAsync(int id, TextDtoForUpdate textDto, bool trackChanges)
        {
            var entity = await _repository.Text.GetOneTextByIdAsync(id, trackChanges);

            if (entity is null)
                throw new TextNotFoundException(id);

            _mapper.Map(textDto, entity);

            _repository.Text.UpdateOneText(entity);
            await _repository.SaveAsync();
        }
        public async Task<IEnumerable<TextDto>> GetTextsByUserIdAsync(string userId, bool trackChanges)
        {
            var texts = await _repository.Text.GetTextsByUserIdAsync(userId, trackChanges);
            return _mapper.Map<IEnumerable<TextDto>>(texts);
        }
        public async Task UploadPhotoAsync(int textId, IFormFile photo)
        {
            var text = await _repository.Text.GetOneTextByIdAsync(textId, true);

            if (text == null)
                throw new TextNotFoundException(textId);

            using var ms = new MemoryStream();
            await photo.CopyToAsync(ms);

            text.PhotoData = ms.ToArray();
            text.PhotoContentType = photo.ContentType;

            await _repository.SaveAsync();
        }
        public async Task<(byte[] PhotoData, string ContentType)> GetPhotoByTextIdAsync(int textId)
        {
            var text = await _repository.Text.GetOneTextByIdAsync(textId, false);

            if (text == null || text.PhotoData == null)
                throw new Exception("Photo not found");

            return (text.PhotoData, text.PhotoContentType);
        }
        public async Task<IEnumerable<TextPhotoDto>> GetAllTextPhotosByUserIdAsync(string userId, bool trackChanges)
        {
            var texts = await _repository.Text.GetTextsByUserIdAsync(userId, trackChanges);

            var photoList = texts
                .Where(t => t.PhotoData != null && t.PhotoData.Length > 0)
                .Select(t => new TextPhotoDto
                {
                    TextId = t.Id,
                    Title = t.Title ?? string.Empty,
                    ContentPreview = t.Content?.Length > 100 ? t.Content.Substring(0, 100) + "..." : t.Content,
                    PhotoContentType = t.PhotoContentType,
                    PhotoBase64 = Convert.ToBase64String(t.PhotoData)
                });

            return photoList;
        }

    }

}


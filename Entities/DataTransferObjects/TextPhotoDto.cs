using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record TextPhotoDto
    {
        public int TextId { get; init; }
        public string Title { get; init; }
        public string? ContentPreview { get; init; }
        public string? PhotoBase64 { get; init; }
        public string? PhotoContentType { get; init; }
    }
}

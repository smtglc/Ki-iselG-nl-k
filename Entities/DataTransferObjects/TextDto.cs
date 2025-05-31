using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record TextDto
    {
        public int Id { get; init; }

        public string Title { get; init; } // <-- Eklendi
        public string Content { get; init; }
        public DateTime CreatedDate { get; init; }
    }
}

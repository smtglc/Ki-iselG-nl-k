using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record TextDtoForManipulation
    {
        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(40000, ErrorMessage = "Content must not exceed 6000 words.")]
        public string? Content { get; init; }
    }
}

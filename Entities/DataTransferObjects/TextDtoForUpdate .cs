using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record TextDtoForUpdate : TextDtoForManipulation
    {
        [Required(ErrorMessage = "Id is required for updating.")]
        public int Id { get; init; }
    }
}

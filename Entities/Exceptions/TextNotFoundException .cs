using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class TextNotFoundException : NotFoundException
    {
        public TextNotFoundException(int id)
            : base($"The Text with id {id} could not be found.")
        {
        }
    }
}

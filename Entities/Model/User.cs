using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class User :IdentityUser
    {
        public String? FirstName { get; set; }
        public String? LastName { get; set; }

        public ICollection<Text> Texts { get; set; } // <-- bu satırı ekle
    }
}

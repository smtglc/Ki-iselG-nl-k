using Entities.DataTransferObjects;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        ITextService TextService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}

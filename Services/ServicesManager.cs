using AutoMapper;
using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager :IServiceManager
    {
        private readonly Lazy<ITextService> _textService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServicesManager(IRepositoryManager repositoryManager,
            ILoggerServices logger,
            UserManager<User> userManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _textService = new Lazy<ITextService>(() =>
                new TextManager(repositoryManager, logger, mapper));

            _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationManager(logger,mapper,userManager,configuration));    
        }

        public ITextService TextService => _textService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}

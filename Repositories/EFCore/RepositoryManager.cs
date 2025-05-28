using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<ITextRepository> _textRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _textRepository = new Lazy<ITextRepository>(() => new TextRepository(_context));
        }

        public ITextRepository Text => _textRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
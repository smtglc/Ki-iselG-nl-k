using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class TextRepository : RepositoryBase<Text>, ITextRepository
    {
        public TextRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneText(Text text) => Create(text);

        public void DeleteOneText(Text text) => Delete(text);

        public async Task<IEnumerable<Text>> GetAllTextBooksAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                  .OrderBy(t => t.Id)
                  .ToListAsync();

        public async Task<Text> GetOneTextByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(t => t.Id.Equals(id), trackChanges)
                  .SingleOrDefaultAsync();

        public void UpdateOneText(Text text) => Update(text);
    }
}

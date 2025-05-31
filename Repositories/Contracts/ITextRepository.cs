using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ITextRepository : IRepositoryBase<Text>
    {
        Task<IEnumerable<Text>> GetAllTextBooksAsync(bool trackChanges);
        Task<Text> GetOneTextByIdAsync(int id, bool trackChanges);
        void CreateOneText(Text text);
        void DeleteOneText(Text text);
        void UpdateOneText(Text text);
        Task<IEnumerable<Text>> GetTextsByUserIdAsync(string userId, bool trackChanges);

    }
}

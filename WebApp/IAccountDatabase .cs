using System;
using System.Threading.Tasks;

namespace WebApp
{
    public interface IAccountDatabase
    {
        Action<Account> OnAccountAdd { set; }

        Task<Account> GetOrCreateAccountAsync(string id);
        
        Task<Account> GetOrCreateAccountAsync(long id);
        
        Task<Account> FindByUserNameAsync(string userName);
    }
}
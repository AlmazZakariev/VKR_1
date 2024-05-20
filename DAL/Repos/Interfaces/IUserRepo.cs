using DAL.Repos.Base;
using Domain.Entities;

namespace DAL.Repos.Interfaces
{
    public interface IUserRepo:IRepo<User>
    {
       ValueTask<User?> FindByEmailAsync(string email);
       ValueTask<IEnumerable<User?>> FindAllAdminsAsync();
    }
}

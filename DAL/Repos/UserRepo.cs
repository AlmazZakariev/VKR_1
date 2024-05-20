using DAL.EfStructures;
using DAL.Repos.Base;
using DAL.Repos.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DAL.Repos
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        public UserRepo(ApplicationDBContext context) : base(context) { }
        internal UserRepo(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public async ValueTask<User?> FindByEmailAsync(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async ValueTask<IEnumerable<User?>> FindAllAdminsAsync()
        {
            return await Context.Users.Where(u => u.Admin[0] == 1).ToListAsync();
        }
    }
}

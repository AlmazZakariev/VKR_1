using DAL.EfStructures;
using DAL.Repos.Base;
using DAL.Repos.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class GeneralRepo : BaseRepo<General>, IGeneralRepo
    {
        public GeneralRepo(ApplicationDBContext context) : base(context)
        {

        }
        public GeneralRepo(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public async ValueTask<General?> FindSingle()
        {
            return await Context.General.OrderByDescending(g => g.Id).FirstOrDefaultAsync();
        }
    }
}

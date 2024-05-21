using DAL.EfStructures;
using DAL.Repos.Base;
using DAL.Repos.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class RegistrationRepo : BaseRepo<Registration>, IRegistrationRepo
    {
        public RegistrationRepo(ApplicationDBContext context):base(context)
        {
                
        }
        public RegistrationRepo(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
                
        }
        
      
    }
}

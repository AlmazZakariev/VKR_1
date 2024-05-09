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
    internal class RequestRepo : BaseRepo<Request>, IRequestRepo
    {
        public RequestRepo(ApplicationDBContext context) : base(context)
        {
                
        }
        public RequestRepo(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
                
        }
    }
}

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
    internal class RoomRepo : BaseRepo<Room>, IRoomRepo
    {
        public RoomRepo(ApplicationDBContext context) : base(context)
        {
                
        }
        internal RoomRepo(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
                
        }
    }
}

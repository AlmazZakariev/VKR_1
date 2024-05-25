using DAL.EfStructures;
using DAL.Repos.Base;
using DAL.Repos.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class RoomRepo : BaseRepo<Room>, IRoomRepo
    {
        public RoomRepo(ApplicationDBContext context) : base(context)
        {
                
        }
        internal RoomRepo(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
                
        }
        public async ValueTask<IEnumerable<short>?> FindFloorsWithFreeRoomsByAdminAndGender(long adminId, short gender)
        {
            return await Context.Rooms
                .Where(r => r.AdministratorId == adminId && r.FreeSlots > 0 &&(r.Gender == gender||r.Gender==null))
                .Select(r => r.Floor)
                .Distinct()
                .OrderBy(f => f)
                .ToListAsync();
        }
        public async ValueTask<IEnumerable<short>?> FindNumbersWithFreeRoomsByAdminAndFloor(long adminId, short gender, short floor)
        {
            return await Context.Rooms
                .Where(r => r.AdministratorId == adminId && r.FreeSlots > 0 && (r.Gender == gender || r.Gender == null) && r.Floor == floor)
                .Select(r => r.Number)
                .Distinct()
                .OrderBy(n => n)
                .ToListAsync();
        }
        public async ValueTask<IEnumerable<Room>?> FindRoomsWithFreeRoomsByAdminAndFloorAndNumber(long adminId, short gender, short floor, short number)
        {
            return await Context.Rooms
                .Where(r => r.AdministratorId == adminId && r.FreeSlots > 0 && (r.Gender == gender || r.Gender == null) && r.Floor == floor&&r.Number==number)
                .OrderBy(n => n.Id)
                .ToListAsync();
        }
    }
}

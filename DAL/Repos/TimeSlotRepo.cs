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
    public class TimeSlotRepo:BaseRepo<TimeSlot>, ITimeSlotRepo
    {
        public TimeSlotRepo(ApplicationDBContext context) : base(context)
        {

        }
        public TimeSlotRepo(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public async ValueTask<int> AddRangeAsync(IEnumerable<TimeSlot> entities, bool persist = true)
        {
            await Table.AddRangeAsync(entities);
            return  persist ? await SaveChangesAsync() : 0;
        }
        public async ValueTask<TimeSlot?> FindFreeByDay(DateTime day)
        {
            return await Context.TimeSlots.FirstOrDefaultAsync(t => t.Free == new byte[] {0}&&t.Date.Date==day.Date);
        }
        public async ValueTask<TimeSlot?> FindFree()
        {
            return await Context.TimeSlots.FirstOrDefaultAsync(t => t.Free == new byte[] { 0 });
        }
    }
}

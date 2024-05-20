using DAL.Repos.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.Interfaces
{
    public interface ITimeSlotRepo:IRepo<TimeSlot>
    {
        ValueTask<int> AddRangeAsync(IEnumerable<TimeSlot> entities, bool persist = true);
        ValueTask<TimeSlot?> FindFreeByDay(DateTime day);
        ValueTask<TimeSlot?> FindFree();
    }
}

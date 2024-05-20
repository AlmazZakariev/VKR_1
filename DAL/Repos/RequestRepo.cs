using DAL.EfStructures;
using DAL.Repos.Base;
using DAL.Repos.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace DAL.Repos
{
    public class RequestRepo : BaseRepo<Request>, IRequestRepo
    {
        public RequestRepo(ApplicationDBContext context) : base(context)
        {

        }
        public RequestRepo(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        //public Task<Request> GetRequestByUser(User user)
        //{
        //    return Context.Requests.FirstOrDefaultAsync(x => x.UserId == user.Id);
        //}
        public override async ValueTask<int> AddAsync(Request entity, bool persist = true)
        {
            ValueTask<int> task1 = new ValueTask<int>();
            ValueTask<int> task2 = new ValueTask<int>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var _timeSlotRepo = new TimeSlotRepo(Context);
                TimeSlot timeSlot;
                if (entity.PreferenceDate == null)
                {
                    timeSlot = await _timeSlotRepo.FindFree();
                }
                else
                {
                    timeSlot = await _timeSlotRepo.FindFreeByDay((DateTime)entity.PreferenceDate);
                }
                if ( timeSlot!= null)
                {
                    entity.TimeSlotId = timeSlot.Id;
                    timeSlot.Free = [0];

                    await _timeSlotRepo.UpdateAsync(timeSlot);
                    //task1 = persist ? await Context.SaveChangesAsync() : 0;
                    var a = await SaveChangesAsync();
                    
                    await Context.Requests.AddAsync(entity);
                    //task2 = persist ? SaveChangesAsync() : new ValueTask<int>(0);
                    await SaveChangesAsync();
                }
                scope.Complete();
            }
            return task1.Result+task2.Result;
        }
        public async ValueTask<Request?> FindByUserAsync(long userId)
        {
            return await Context.Requests
               .Include(r => r.User)
               .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}

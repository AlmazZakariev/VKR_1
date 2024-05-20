using DAL.EfStructures;
using DAL.Repos.Base;
using DAL.Repos.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

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

        //public Task<Request> GetRequestByUser(User user)
        //{
        //    return Context.Requests.FirstOrDefaultAsync(x => x.UserId == user.Id);
        //}
        public async ValueTask<int> AddAsync(Request entity, DateTime? date=null, bool persist = true)
        {
            
            using(TransactionScope scope = new TransactionScope())
            {
                var _timeSlotRepo = new TimeSlotRepo(Context);
                TimeSlot timeSlot;
                if (date== null)
                {
                    timeSlot = await _timeSlotRepo.FindFree();
                }
                else
                {
                    timeSlot = await _timeSlotRepo.FindFreeByDay((DateTime)date);
                }
                if ( timeSlot!= null)
                {
                    entity.TimeSlotId = timeSlot.Id;
                    await Context.Requests.AddAsync(entity);

                    return persist ? await SaveChangesAsync() : 0;
                }

                scope.Complete();
            }
            return 0;
        }
    }
}

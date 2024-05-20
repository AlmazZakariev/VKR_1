using DAL.EfStructures;
using DAL.Exceptions;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EfStructures;

namespace DAL.Repos.Base
{
    public abstract class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
    {
        private readonly bool _disposeContext;
        public ApplicationDBContext Context { get; }

        public DbSet<T> Table { get; }
        protected BaseRepo(ApplicationDBContext context)
        {
            Context = context;
            Table = Context.Set<T>();
            _disposeContext = false;
        }
        protected BaseRepo(DbContextOptions<ApplicationDBContext> options)
            : this(new ApplicationDBContext(options))
        {
            _disposeContext = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool _isDisposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            if (disposing)
            {
                if (_disposeContext)
                {
                    Context.Dispose();
                }

            }
            _isDisposed = true; ;
        }

        public virtual async ValueTask<int> AddAsync(T entity, bool persist = true)
        {
            await Table.AddAsync(entity);
            return persist ?  await SaveChangesAsync() : 0;
        }

        //public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
        //{
        //    Table.AddRange(entities);
        //    return persist ? SaveChanges() : 0;
        //}

        public virtual async ValueTask<int> UpdateAsync(T entity, bool persist = true)
        {
            Table.Update(entity);
            return  persist ? await SaveChangesAsync() : 0;
        }

        //public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        //{
        //    Table.UpdateRange(entities);
        //    return persist ? SaveChanges() : 0;
        //}

        //public virtual int Delete(int id, byte[] timeStamp, bool persist = true)
        //{
        //    var entity = new T { Id = id, TimeStamp = timeStamp };
        //    Context.Entry(entity).State = EntityState.Deleted;
        //    return persist ? SaveChanges() : 0;
        //}

        public virtual async ValueTask<int> DeleteAsync(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? await SaveChangesAsync() : 0;
        }

        //public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
        //{
        //    Table.RemoveRange(entities);
        //    return persist ? SaveChanges() : 0;
        //}

        public virtual async ValueTask<T?> FindAsync(long? id)
        {
            return await Table.FindAsync(id);
        }

        //public virtual T? FindAsNoTracking(int id)
        //{
        //    return Table.AsNoTrackingWithIdentityResolution()
        //        .FirstOrDefault(x => x.Id == id);
        //}

        //public T? FindIgnoreQueryFilters(int id)
        //{
        //    return Table.IgnoreQueryFilters()
        //        .FirstOrDefault(x => x.Id == id);
        //}

        public virtual IEnumerable<T> GetAll()
        {
            return Table;
        }

        //public virtual IEnumerable<T> GetAllIgnoreQueryFilters()
        //{
        //    return Table.IgnoreQueryFilters();
        //}

        public void ExecuteQuery(string sql, object[] sqlParametersObject)
        {
            Context.Database.ExecuteSqlRaw(sql, sqlParametersObject);
        }

        public async ValueTask<int> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (CustomException ex)
            {
                //уже зарегистрировано в журнале
                throw;
            }
            catch (Exception ex)
            {
                //подлежит регистрации в журнале
                throw new CustomException("An error occured updating the database", ex);
            }
        }

        ~BaseRepo()
        {
            Dispose(false);
        }
    }
}

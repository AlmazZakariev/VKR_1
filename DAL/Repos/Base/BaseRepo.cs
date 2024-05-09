using DAL.EfStructures;
using DAL.Exceptions;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.Base
{
    //public abstract class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
    //{
    //    private readonly bool _disposeContext;
    //    public ApplicationDbContext Context { get; }

    //    public DbSet<T> Table { get; }
    //    protected BaseRepo(ApplicationDbContext context)
    //    {
    //        Context = context;
    //        Table = Context.Set<T>();
    //        _disposeContext = false;
    //    }
    //    protected BaseRepo(DbContextOptions<ApplicationDbContext> options)
    //        : this(new ApplicationDbContext(options))
    //    {
    //        _disposeContext = true;
    //    }
    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }
    //    private bool _isDisposed;
    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (_isDisposed)
    //        {
    //            return;
    //        }
    //        if (disposing)
    //        {
    //            if (_disposeContext)
    //            {
    //                Context.Dispose();
    //            }

    //        }
    //        _isDisposed = true; ;
    //    }

    //    public virtual int Add(T entity, bool persist = true)
    //    {
    //        Table.Add(entity);
    //        return persist ? SaveChanges() : 0;
    //    }

    //    public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
    //    {
    //        Table.AddRange(entities);
    //        return persist ? SaveChanges() : 0;
    //    }

    //    public virtual int Update(T entity, bool persist = true)
    //    {
    //        Table.Update(entity);
    //        return persist ? SaveChanges() : 0;
    //    }

    //    public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
    //    {
    //        Table.UpdateRange(entities);
    //        return persist ? SaveChanges() : 0;
    //    }

    //    public virtual int Delete(int id, byte[] timeStamp, bool persist = true)
    //    {
    //        var entity = new T { Id = id, TimeStamp = timeStamp };
    //        Context.Entry(entity).State = EntityState.Deleted;
    //        return persist ? SaveChanges() : 0;
    //    }

    //    public virtual int Delete(T entity, bool persist = true)
    //    {
    //        Table.Remove(entity);
    //        return persist ? SaveChanges() : 0;
    //    }

    //    public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
    //    {
    //        Table.RemoveRange(entities);
    //        return persist ? SaveChanges() : 0;
    //    }

    //    public virtual T? Find(int? id)
    //    {
    //        return Table.Find(id);
    //    }

    //    public virtual T? FindAsNoTracking(int id)
    //    {
    //        return Table.AsNoTrackingWithIdentityResolution()
    //            .FirstOrDefault(x => x.Id == id);
    //    }

    //    public T? FindIgnoreQueryFilters(int id)
    //    {
    //        return Table.IgnoreQueryFilters()
    //            .FirstOrDefault(x => x.Id == id);
    //    }

    //    public virtual IEnumerable<T> GetAll()
    //    {
    //        return Table;
    //    }

    //    public virtual IEnumerable<T> GetAllIgnoreQueryFilters()
    //    {
    //        return Table.IgnoreQueryFilters();
    //    }

    //    public void ExecuteQuery(string sql, object[] sqlParametersObject)
    //    {
    //        Context.Database.ExecuteSqlRaw(sql, sqlParametersObject);
    //    }

    //    public int SaveChanges()
    //    {
    //        try
    //        {
    //            return Context.SaveChanges();
    //        }
    //        catch (CustomException ex)
    //        {
    //            //уже зарегистрировано в журнале
    //            throw;
    //        }
    //        catch (Exception ex)
    //        {
    //            //подлежит регистрации в журнале
    //            throw new CustomException("An error occured updating the database", ex);
    //        }
    //    }

    //    ~BaseRepo()
    //    {
    //        Dispose(false);
    //    }
    //}
}

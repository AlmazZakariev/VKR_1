using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.Base
{
    public interface IRepo<T>:IDisposable
    {
        ValueTask<int> AddAsync(T entity, bool persist = true);
        //int AddRange(IEnumerable<T> entities, bool persist = true);
        ValueTask<int> UpdateAsync(T entity, bool persist = true);
        //int UpdateRange(IEnumerable<T> entities, bool persist = true);
        //int Delete(int id, byte[] timeStamp, bool persist = true);
        ValueTask<int> DeleteAsync(T entity, bool persist = true);
        //int DeleteRange(IEnumerable<T> entities, bool persist = true);
        ValueTask<T?> FindAsync(long? id);
        //T? FindAsNoTracking(int id);
        //T? FindIgnoreQueryFilters(int id);
        IEnumerable<T?> GetAll();
        //IEnumerable<T> GetAllIgnoreQueryFilters();
        void ExecuteQuery(string sql, object[] sqlParametersObject);
        ValueTask<int> SaveChangesAsync();
    }
}

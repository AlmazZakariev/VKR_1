using DAL.EfStructures;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL.Controllers
{
    public  class Connection :IDisposable
    {
        public readonly IConfiguration Configuration;
        public readonly ApplicationDBContext Context;

        public virtual void Dispose()
        {
            Context.Dispose();
        }

        public Connection()
        {
            Configuration = ConnectionHelper.GetConfiguration();
            Context = ConnectionHelper.GetContext(Configuration);
        }
        protected void ExeciteInATransaction(Action actionToExecute)
        {
            var strategy = Context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var trans = Context.Database.BeginTransaction();
                actionToExecute();
                trans.Rollback();
            });
        }

        protected void ExecuteInASharedTransaction(Action<IDbContextTransaction> actionToExecute)
        {
            var strategy = Context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using IDbContextTransaction trans =
                    Context.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
                actionToExecute(trans);
                trans.Rollback();
            });
        }
    }
}

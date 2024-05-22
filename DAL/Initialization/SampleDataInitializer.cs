using DAL.EfStructures;
using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
namespace DAL.Initialization
{
    public static class SampleDataInitializer
    {
        public static void DropAndCreateDatabase(ApplicationDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
        public static void ClearData(ApplicationDBContext context)
        {
            var entities = new[]
            {
                typeof(Room).FullName,
                typeof(Registration).FullName,
                typeof(Request).FullName,
                typeof(TimeSlot).FullName,
                typeof(General).FullName,
                typeof(User).FullName,
            };
            foreach (var entityName in entities)
            {
                var entity = context.Model.FindEntityType(entityName);
                var tableName = entity.GetTableName();
                var schemaName = entity.GetSchema();
                context.Database.ExecuteSqlRaw($"DELETE FROM {schemaName}.{tableName}");
                context.Database
                    .ExecuteSqlRaw($"DBCC CHECKIDENT (\"{schemaName}.{tableName}\", RESEED, 1);");
            }
        }

        internal static void SeedData(ApplicationDBContext context)
        {
            try
            {
                ProcessInsert(context, context.Users!, SampleData.Users);
                ProcessInsert(context, context.Rooms!, SampleData.Rooms);
                ProcessInsert(context, context.Requests!, SampleData.Requests);
                ProcessInsert(context, context.Registrations!, SampleData.Registrations);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            static void ProcessInsert<TEntity>(ApplicationDBContext context, DbSet<TEntity> table, List<TEntity> records) where TEntity : BaseEntity
            {
                if (table.Any())
                {
                    return;
                }
                IExecutionStrategy stratagy = context.Database.CreateExecutionStrategy();
                stratagy.Execute(() =>
                {
                    using var transaction = context.Database.BeginTransaction();
                    try
                    {
                        var metaData =
                            context.Model.FindEntityType(typeof(TEntity).FullName);
                        context.Database.ExecuteSqlRaw(
                            $"SET IDENTITY_INSERT{metaData.GetSchema()}.{metaData.GetTableName()} ON");
                        table.AddRange(records);
                        context.SaveChanges();
                        context.Database.ExecuteSqlRaw(
                            $"SET IDENTITY_INSERT {metaData.GetSchema()}.{metaData.GetTableName()} OFF");
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                });
            }
        }

        public static void InitializeData(ApplicationDBContext context)
        {
            DropAndCreateDatabase(context);
            SeedData(context);
        }
        public static void ClearAndReseedDataBase(ApplicationDBContext context)
        {
            ClearData(context);
            SeedData(context);
        }

        
    }
}

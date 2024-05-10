using DAL.Controllers;
using DAL.Initialization;
using Microsoft.EntityFrameworkCore;

namespace DAL.Tests
{

    internal class Program
    {
        static void Main(string[] args)
        {
            
            var connection = new Connection();

            //SampleDataInitializer.ClearAndReseedDataBase(connection.Context);

            var query = connection.Context.Users.IgnoreQueryFilters()
                .Where(x => x.Id<3);
            var qs = query.ToQueryString();
            var users = query.ToList();
            foreach (var user in users)
            {
                Console.WriteLine(user.ToString());
            }
        }
        
    }
}

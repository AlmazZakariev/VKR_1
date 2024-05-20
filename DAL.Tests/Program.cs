using DAL.Controllers;
using DAL.EfStructures;
using DAL.Initialization;
using DAL.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Tests
{

    internal class Program
    {
        static void Main(string[] args)
        {

            var connection = new Connection();

            var tsRepo = new TimeSlotRepo(connection.Context);

            var ts = tsRepo.GetAll();
            DateTime day = DateTime.Now.AddDays(2);
            

            var ts2 = tsRepo.FindFreeByDay(day).Result;
            var ts3 = connection.Context.TimeSlots.Where(x => x.Date == day.Date).FirstOrDefault();
            Console.WriteLine();
            //UserRepo userRepo = new UserRepo(connection.Context);

            //var user =  userRepo.Find(3).Result;
            //Console.WriteLine(user);

            //SampleDataInitializer.ClearData(connection.Context);
            //SampleDataInitializer.InitializeData(connection.Context);

            //CreateUsers(connection.Context);

            //CreateRooms(connection.Context);

            //UpdateRooms(connection.Context);

            //CreateTimeSlots(connection.Context);

            //UpdateTimeSlots(connection.Context);

            //CreateRequest(connection.Context);

            //CreateRegistration(connection.Context);
        }

        private static void CreateUsers(ApplicationDBContext context)
        {
            List<User> Users = new()
            {   
                new () {Name= "Алмаз", Surname = "Закариев", Patronymic="Фанилевич", Email="almazzakariev@yandex.ru", Phone="89125536367", Pass= SecretHasher.Hash("qwerty1234"), Admin= [0] },
                new () { Name= "Иван", Surname = "Иванов", Patronymic="Иванович", Email="ivanov@yandex.ru", Phone="89122711367", Pass=SecretHasher.Hash("zxcvbn00"), Admin= [0] },
                new () {Name= "Николай", Patronymic="Николаевич", Email="NNNikolaev@yandex.ru", Phone="89125333333", Pass=SecretHasher.Hash("abcdf"), Admin= [0] },
                new () { Name= "Администратор", Surname = "Первый", Patronymic="Общежития", Email="admin1@yandex.ru", Phone="89111111111", Pass=SecretHasher.Hash("admin12345"), Admin= [1] },
            };

            foreach(var user in Users)
            {
                TestDB.CreateUser(user, context);
            }
        }
        private static void UpdateUsers(ApplicationDBContext context)
        {
            List<User> Users = new()
            {
                new () {Name= "Алмаз", Surname = "Закариев", Patronymic="Фанилевич", Email="almazzakariev@yandex.ru", Phone="89123456789", Pass= SecretHasher.Hash("qwerty1234"), Admin= [1] },
                new () { Surname = "Иванов", Patronymic="Иванович", Email="ivanov@yandex.ru", Phone="89122711367", Pass=SecretHasher.Hash("zxcvbn00"), Admin= [0] },                
                
            };

            TestDB.UpdateUser(Users[0], context, 2);
            TestDB.UpdateUser(Users[1], context, 3);
        }

        private static void CreateRooms(ApplicationDBContext context)
        {
            List<Room> Rooms = new()
            {
                new() { Floor = 2, Number = 1, AddNumber = 2, FreeSlots = 2, Gender = null },
                new() { Floor = 2, Number = 1, AddNumber = 3, FreeSlots = 3, Gender = null },
                new() { Floor = 2, Number = 2, AddNumber = 2, FreeSlots = 0, Gender = 0 },
                new() { Floor = 2, Number = 2, AddNumber = 3, FreeSlots = 1, Gender = 0 },
                new() { Floor = 2, Number = 3, AddNumber = 2, FreeSlots = 1, Gender = 1 },
                new() { Floor = 2, Number = 3, AddNumber = 3, FreeSlots = 2, Gender = 1 },
                new() { Floor = 2, Number = 4, AddNumber = 3, FreeSlots = 3 },
                new() { Floor = 2, Number = 4,  FreeSlots = 0 },

                //new() { Floor = 2, Number = 4, AddNumber = 2, },
                //new() { Floor = 2, Number = 5, },
                //new() { Floor = 2, AddNumber = 2, FreeSlots = 2, Gender = 1 },
            };
        
            foreach (Room room in Rooms)
            {
                TestDB.CreateRoom(room, context);
            }
        }
        private static void UpdateRooms(ApplicationDBContext context)
        {
            List<Room> Rooms = new()
            {               
                new() { Floor = 2, Number = 3, AddNumber = 2, FreeSlots = 0, Gender = 1 },
            };

            TestDB.UpdateRoom(Rooms[0], context, 6);
        }

        //private static void CreateTimeSlots(ApplicationDBContext context)
        //{
        //    List<TimeSlot> timeSlots = new()
        //    {
        //        new (){Date = new DateTime(2024, 8, 27, 9,0,0), Time = new TimeOnly(9, 0), Free = [0], AdministratorId = 5},
        //        new (){Day = new DateTime(2024, 8, 27), Time = new TimeOnly(9, 15), Free = [0], AdministratorId = 5},
        //        new (){Day = new DateTime(2024, 8, 27), Time = new TimeOnly(9, 30), Free = [1], AdministratorId = 5},
        //        new (){Day = new DateTime(2024, 8, 27), Time = new TimeOnly(9, 45), Free = [0], AdministratorId = 5},
        //        new (){Day = new DateTime(2024, 8, 27), Time = new TimeOnly(10, 0), Free = [1], AdministratorId = 5},

        //        //new (){Day = new DateTime(2024, 8, 27), Time = new TimeOnly(10, 15), Free = [0]},
        //        //new (){Day = new DateTime(2024, 8, 27), Time = new TimeOnly(10, 30)},
        //        //new (){Day = new DateTime(2024, 8, 27) },

        //    };

        //    foreach (var ts in timeSlots)
        //    {
        //        TestDB.CreateTimeSlot(ts, context);
        //    }
        //}

        //private static void UpdateTimeSlots(ApplicationDBContext context)
        //{
        //    List<TimeSlot> timeSlots = new()
        //    {
        //        new (){Day = new DateTime(2024, 8, 27), Time = new TimeOnly(9, 0), Free = [1], AdministratorId = 5},         
        //        new (){Day = new DateTime(2024, 8, 27), Time = new TimeOnly(9, 30), Free = [0], AdministratorId = 5},
        //    };

        //    TestDB.UpdateTimeSlot(timeSlots[0], context, 1);
        //    TestDB.UpdateTimeSlot(timeSlots[1], context, 3);
        //}

        private static void CreateRequest(ApplicationDBContext context)
        {
            List<Request> requests = new()
        {
                new (){Date = DateTime.Now, PreferenceDate = new DateTime(2024, 8, 27), UserId = 2, TimeSlotId = 3},
                new (){Date = DateTime.Now, UserId = 3, TimeSlotId = 4},

                //new (){Date = DateTime.Now, PreferenceDate = new DateTime(2024, 8, 27)},
                //new (){Date = DateTime.Now, UserId = 5, TimeSlotId = 2},
        };

            foreach (var request in requests)
            {
                TestDB.CreateRequest(request, context);
            }
        }
        private static void CreateRegistration(ApplicationDBContext context)
        {
            List<Registration> registrations = new()
            {
                new (){RequestId = 1, AdministratorId = 5, RoomId = 3, Date = DateTime.Now},

                new(){RequestId = 2, AdministratorId = 5}
            };

            foreach (var registration in registrations)
            {
                TestDB.CreateRegistration(registration, context);
            }
        }
    }

    
}

//SampleDataInitializer.ClearAndReseedDataBase(connection.Context);

//var query = connection.Context.Users.IgnoreQueryFilters();

//var qs = query.ToQueryString();
//var users = query.ToList();
//foreach (var user in users)
//{
//    Console.WriteLine(user.ToString());
//}
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Controllers;

namespace DAL.Initialization
{
    public static class SampleData
    {

        public static List<User> Users => new()
        {
           new () {Id = 1, Name= "Алмаз", Surname = "Закариев", Patronymic="Фанилевич", Email="almazzakariev@yandex.ru", Phone="89125536367", Pass= SecretHasher.Hash("qwerty1234"), Admin= [0] , Gender = 1},
           new () {Id = 2, Name= "Иван", Surname = "Иванов", Patronymic="Иванович", Email="iiivanov@yandex.ru", Phone="89122711367", Pass=SecretHasher.Hash("zxcvbn00"), Admin= [0],Gender = 1 },
           new () {Id = 3, Name= "Николай", Surname = "Николаев", Patronymic="Николаевич", Email="NNNikolaev@yandex.ru", Phone="89125333333", Pass=SecretHasher.Hash("abcdf"), Admin= [0],Gender = 1 },
           new () {Id = 4, Name= "Администратор", Surname = "Первый", Patronymic="Общежития", Email="admin1@yandex.ru", Phone="89111111111", Pass=SecretHasher.Hash("admin12345"), Admin= [1],Gender = 0 },
        };
        public static List<Room> Rooms => new()
        {
           new() {Id=1, Floor = 2, Number=1, AddNumber=2, FreeSlots=2, Gender = null},
           new() {Id=2, Floor = 2, Number=1, AddNumber=3, FreeSlots=3, Gender = null},
           new() {Id=3, Floor = 2, Number=2, AddNumber=2, FreeSlots=0, Gender = 0},
           new() {Id=4, Floor = 2, Number=2, AddNumber=3, FreeSlots=1, Gender = 0},
           new() {Id=5, Floor = 2, Number=3, AddNumber=2, FreeSlots=1, Gender = 1},
           new() {Id=6, Floor = 2, Number=3, AddNumber=3, FreeSlots=2, Gender = 1},
        };
        public static List<Request> Requests => new()
        {

        };
        public static List<Registration> Registrations => new()
        {

        };
    }
}

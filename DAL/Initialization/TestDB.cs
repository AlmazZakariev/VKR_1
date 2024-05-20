using DAL.EfStructures;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Controllers;
using Domain.Entities;

namespace DAL.Initialization
{
    public static class TestDB
    {

        public static void CreateUser(User user, ApplicationDBContext context)
        {
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine("Пользователь создан успешно");
            }
            catch
            {
                Console.WriteLine("ОШБИКА! Не удалось добавить пользователя");
                context.Remove(user);       
            }
        }
        public static void CreateRoom(Room room, ApplicationDBContext context)
        {
            try
            {
                context.Rooms.Add(room);
                context.SaveChanges();
                Console.WriteLine("Комната создана успешно");

            }
            catch
            {
                Console.WriteLine("ОШБИКА! Не удалось добавить комнату");
                context.Remove(room);
                throw;
            }
        }
        public static void CreateRequest(Request request, ApplicationDBContext context)
        {
            try
            {
                context.Requests.Add(request);
                context.SaveChanges();
                Console.WriteLine("Заявка создана успешно");

            }
            catch
            {
                Console.WriteLine("ОШБИКА! Не удалось добавить заявку");
                context.Remove(request);
                throw;
            }
        }
        public static void CreateRegistration(Registration registration, ApplicationDBContext context)
        {
            try
            {
                context.Registrations.Add(registration);
                context.SaveChanges();
                Console.WriteLine("Регистрация создана успешно");

            }
            catch
            {
                Console.WriteLine("ОШБИКА! Не удалось добавить регистрацию");
                context.Remove(registration);
            }
        }

        public static void CreateTimeSlot(TimeSlot timeSlot, ApplicationDBContext context)
        {
            try
            {
                context.TimeSlots.Add(timeSlot);
                context.SaveChanges();
                Console.WriteLine("Временной интервал создан успешно");

            }
            catch
            {
                Console.WriteLine("ОШБИКА! Не удалось добавить временной интервал");
                context.Remove(timeSlot);
            }
        }

        public static void UpdateUser(User user, ApplicationDBContext context, long id)
        {
            var _user = context.Users.FirstOrDefault(t => t.Id == id);
            if (_user != null)
            {
                _user.Surname = user.Surname;
                _user.Name = user.Name;
                _user.Patronymic = user.Patronymic;
                _user.Phone = user.Phone;
                _user.Gender = user.Gender;
                _user.Admin = user.Admin;
                try
                {
                    context.Users.Update(_user);
                    context.SaveChanges();
                    Console.WriteLine("Пользователь обновлён успешно");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ОШБИКА! Не удалось обновить пользователя");
                }
            }
            else
            {
                Console.WriteLine("ОШБИКА! Не удалось найти такого пользователя");
            }
        }
      
        public static void UpdateRoom(Room room, ApplicationDBContext context, long id)
        {
            var _room = context.Rooms.FirstOrDefault(t => t.Id == id);
            if (_room != null)
            {
                _room.FreeSlots = room.FreeSlots;
                try
                {
                    context.Rooms.Update(_room);
                    context.SaveChanges();
                    Console.WriteLine("Комната обновлена успешно");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ОШБИКА! Не удалось обновить комнату");
                    Console.WriteLine(ex);
                }
            }
            else
            {
                Console.WriteLine("ОШБИКА! Не удалось найти такую комнату");
            }
        }
        public static void UpdateTimeSlot(TimeSlot timeSlot, ApplicationDBContext context, long id)
        {
            var _timeSlot = context.TimeSlots.FirstOrDefault(t => t.Id == id);
            if (_timeSlot != null)
            {
                _timeSlot.Free = timeSlot.Free;

                try
                {
                    context.TimeSlots.Update(_timeSlot);
                    context.SaveChanges();
                    Console.WriteLine("Временной интервал обновлён успешно");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ОШБИКА! Не удалось обновить временной интервал");
                    Console.WriteLine(ex);

                }
            }
            else
            {
                Console.WriteLine("ОШБИКА! Не удалось найти такой временной интервал");
            }

        }
    }
}

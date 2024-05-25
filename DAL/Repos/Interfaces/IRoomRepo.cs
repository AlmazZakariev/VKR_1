using DAL.Repos.Base;
using Domain.Entities;

namespace DAL.Repos.Interfaces
{
    public interface IRoomRepo:IRepo<Room>
    {
        ValueTask<IEnumerable<short>?> FindFloorsWithFreeRoomsByAdminAndGender(long adminId, short gender);
        ValueTask<IEnumerable<short>?> FindNumbersWithFreeRoomsByAdminAndFloor(long adminId, short gender, short floor);
        ValueTask<IEnumerable<Room>?> FindRoomsWithFreeRoomsByAdminAndFloorAndNumber(long adminId, short gender, short floor, short number);
    }
}

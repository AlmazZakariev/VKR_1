using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace VKR_1.Models.Registration
{
    public class RegistrationViewModel
    {
        public Request? CurrentRequest { get; set; }
        
        public IEnumerable<short>? Floors { get; set; } = null!;
        
        public IEnumerable<short>? RoomNumbers { get; set; } = null!;

        public IEnumerable<short>? Rooms { get; set; } = null!;

        public short? SelectedFloor { get; set; }

        public short? SelectedRoomNumber { get; set; }  

        public long? SelectedRoom { get; set; }

    }

}

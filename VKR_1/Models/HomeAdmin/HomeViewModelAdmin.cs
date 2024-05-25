using Domain.Entities;
using Microsoft.IdentityModel.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace VKR_1.Models.HomeAdmin
{
    public class HomeViewModelAdmin
    {
        public IEnumerable<RegistrationViewModel> Registrations { get; set; }
        //public IEnumerable<Request>? Requests { get; set; }

        public IEnumerable<User?> Admins { get; set; }
        [Required(ErrorMessage = "Данное поле обязательно")]
        [Display(Name = "Старт")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        [Display(Name = "Финиш")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        [Display(Name = "Номер комнаты")]
        public string Room { get; set; }
        public bool DatesSet { get; set; }
    }

    public class RegistrationViewModel
    {
        public IEnumerable<short>? Floors { get; set; } = null!;
        public Request? CurrentRequest { get; set; } = null!;

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Room { get; set; }
    }
}

using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace VKR_1.Models.HomeAdmin
{
    public class HomeViewModelAdmin
    {
        public IEnumerable<Request>? Requests { get; set; }

        public IEnumerable<User>? Admins { get; set; }
        [Required]
        [Display(Name = "Старт")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Финиш")]
        [DataType(DataType.Date)]

        public DateTime EndDate { get; set; }
        public bool DatesSet { get; set; }
    }
}

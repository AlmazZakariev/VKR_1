using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace VKR_1.Models.Home
{
    public class HomeViewModel
    {
        [Required]
        [Display(Name = "Желаемая дата")]
        [DataType(DataType.Date)]
        public DateTime PreferenceDate { get; set; }

        public Request? Request { get; set; }
    }
}

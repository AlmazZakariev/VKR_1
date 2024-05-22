using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace VKR_1.Models.Registration
{
    public class RegistrationViewModel
    {
        //public RegistrationViewModel(Request request)
        //{
        //    CurrentRequest = request;
        //}
        public Request CurrentRequest { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Room { get; set; } = null!;
    }
}

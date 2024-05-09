using System.ComponentModel.DataAnnotations;

namespace VKR_1.Models.Account
{
    public class AccountViewModel
    {
        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Данное поле обязательно")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Данное поле обязательно")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        [Phone]
        public string Phone { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EasyMarket.Models
{
    public class RegistrationModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string surname { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " {0} должен быть не короче {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [System.Web.Mvc.CompareAttribute("password", ErrorMessage = "Пароли не совпадают.")]
        public string confirmPassword { get; set; }

        [Required]
        [Display(Name = "Адрес")]
        public string address { get; set; }

        [Required]
        [Display(Name = "Номер моб. телефона")]       
        public string phone { get; set; }

        [Required]
        [Display(Name="Email")]
        [EmailAddress(ErrorMessage="Не корректный Email.")]
        public string email { get; set; }
    }
}
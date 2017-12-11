using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Spot.Models.Auth.ViewModels
{
    public class SignInViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Authentication
{
    public class RegisterUserViewModel
    {
        [Required]
        [MaxLength(30)]
        [DisplayName("نام کاربری")]
        public string Username { get; set; }

        [Required]
        [DisplayName("ایمیل")]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [DisplayName("رمز عبور")]
        public string Password { get; set; }

        [Required]
        [DisplayName("تکرار رمز عبور")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
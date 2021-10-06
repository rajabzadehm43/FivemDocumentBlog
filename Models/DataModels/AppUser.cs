using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Models.DataModels
{
    public class AppUser : IdentityUser
    {

        public AppUser()
        {
            ImageName = "default.png";
        }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string ImageName { get; set; }

    }
}
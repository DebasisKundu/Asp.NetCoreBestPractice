using Microsoft.AspNetCore.Mvc;

namespace FindMyPG.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        [Remote("CheckExistingEmail", "User", ErrorMessage = "Email Already Exist")]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}

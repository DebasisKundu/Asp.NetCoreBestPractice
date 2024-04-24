using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Users
{
    public class UserRegistrationRequest
    {
        public UserRegistrationRequest(string firstName, string lastName, string phoneNumber,
            string email, string password, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            Role = role;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}

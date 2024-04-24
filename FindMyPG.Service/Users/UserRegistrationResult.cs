using FindMyPG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Users
{
    public class UserRegistrationResult
    {
        public UserRegistrationResult()
        {
            Errors = new List<string>();
        }

        public User User { get; set; }

        public IList<string> Errors { get; set; }

        public bool Success => !Errors.Any();

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}

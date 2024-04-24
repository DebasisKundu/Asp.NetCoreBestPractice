using FindMyPG.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace FindMyPG.Infrastructure.Validators
{
    public class PGPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : User
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            if (string.Equals(user.UserName, password, StringComparison.OrdinalIgnoreCase))
            {
                List<IdentityError> errors = new List<IdentityError>();
                errors.Add(new IdentityError()
                {
                    Code = "Password Check",
                    Description = "Password should not be same as user name"
                });

                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}

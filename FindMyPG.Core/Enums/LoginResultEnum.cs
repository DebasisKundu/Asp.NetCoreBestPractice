using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Enums
{
    public enum LoginResultEnum
    {
        Successful = 1,
        UserNotExist = 2,
        WrongPassword = 3,
        LockedOut = 4,
        PhoneNumberNotConfirmed = 5,
        EmailNotConfirmed = 6
    }
}

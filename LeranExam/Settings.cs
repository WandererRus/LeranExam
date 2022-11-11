using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeranExam
{
    internal class Settings : ISettings
    {

        public IUser ChangeUserSettingsAll(IUser user, string password, DateTime date)
        {
           user.Password = password;
            user.BirthDay = date;
            return user;
        }

        public IUser ChangeUserSettingsBirthDate(IUser user, DateTime date)
        {
            user.BirthDay = date;
            return user;
        }

        public IUser ChangeUserSettingsPassword(IUser user, string password)
        {
            user.Password = password;
            return user;
        }

    }
}

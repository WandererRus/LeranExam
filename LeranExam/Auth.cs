using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeranExam
{
    class Auth : IAuth
    {
        public bool userInSystem { get; set; }

        private string pathUsers = "users.xml";
        public bool GetAuth(IUser user, IMyXmlFile XmlFile)
        {
            IUser findUser = XmlFile.GetUser(pathUsers, user.Login);
            if (findUser.Password == user.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LeranExam
{
    class Register : IRegister
    {
        public IMyXmlFile XmlFile { get; set; }

        public bool ExistUser(IUser user)
        {
            bool findUser = false;
            bool exist = XmlFile.ExistFile("users.xml");
            if (exist)
            {
                IUser existUser = XmlFile.GetUser("users.xml", user.Login);
                if (existUser.Login != "")
                    findUser = true;
            }
            return findUser;
        }

        public bool SaveNewUser(IUser user)
        {
            return XmlFile.SaveUser("auth.xml", user);
        }

        public Register(IMyXmlFile file)
        {
            XmlFile = file;
        }
    }
}

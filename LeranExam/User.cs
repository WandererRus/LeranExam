using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeranExam
{
    internal class User : IUser
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }

        public User()
        {
            Name = "username";
            Login = "";
            Password = "";
            BirthDay = DateTime.MinValue;
        }
        public User(string name, string login, string password, DateTime dt)
        {
            Name = name;
            Login = login;
            Password = password;
            BirthDay = dt;
        }
        public User(string login, string password)
        {
            Name = "username";
            Login = login;
            Password = password;
            BirthDay = DateTime.MinValue;
        }

        public override string ToString()
        {
            return $"Пользователь с логином {Login} паролем  {Password} датой рождения {BirthDay} и ником {Name}";
        }
    }
}

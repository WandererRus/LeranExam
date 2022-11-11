using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeranExam
{
    public class Victorina : IVictorina
    {
        public IMyXmlFile XmlFile { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<IQuestion> questions { get; set; }
        public Victorina()
        {
            XmlFile = new MyXmlFile();
            Path = "";
            Name = "";
            questions = new List<IQuestion>();
        }
        public Victorina(IMyXmlFile xml)
        {
            XmlFile = xml;
            Path = "";
            Name = "";
            questions = new List<IQuestion>();
            for (int i = 0; i < 2; i++)
            {
                questions.Add(new Question());
            }
        }
        public Victorina(string path, IMyXmlFile xml)
        {
            XmlFile = xml;
            Path = path;
            IVictorina victorina = XmlFile.GetVictorina("IT.xml");
            Name = victorina.Name;
            questions = victorina.questions;
        }
        public void Load()
        {
                
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public ITableItem Start(IUser user)
        {
            TableItem ti = new TableItem();
            ti.NameVictorina = Name;
            ti.NameUser = user.Name;
            foreach (var question in questions)
            {
                int numberQuest = 0;
                Console.WriteLine(question.Name);
                foreach (string var in question.variants)
                {
                    numberQuest += 1;
                    Console.WriteLine(numberQuest + ". " + var);
                }
                Console.WriteLine("\n Введите номер правильного ответа или несколько номеров через запятую");
                int userAnswer = Int32.Parse(Console.ReadLine());
                if (question.answers[userAnswer - 1] == 1)
                {
                    ti.Score += 1;
                }

            }
            return ti;
        }
    }
}

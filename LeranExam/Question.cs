using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeranExam
{
    [Serializable]
    public class Question : IQuestion
    {
        public string Name { get; set; }
        public List<string> variants { get; set; }
        public List<int> answers { get; set; }

        public Question()
        {
            Name = "";
            variants = new List<string>() { "", "", "", "" };
            answers = new List<int>() { 0, 0, 0, 0 };
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

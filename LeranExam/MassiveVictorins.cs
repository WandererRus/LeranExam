using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeranExam
{
    internal class MassiveVictorins : IMassiveVictorins
    {
        public IMyXmlFile XmlFile { get; set; }
        public List<IVictorina> VictorinaList { get; set; }

        public MassiveVictorins()
        {
            XmlFile = new MyXmlFile();
            VictorinaList = new List<IVictorina>();
        }

        public void Load()
        {
            VictorinaList = XmlFile.GetAllVictorins().VictorinaList;
        }
    }
}

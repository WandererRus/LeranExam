using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeranExam
{
    class ResultTable : IResultTable
    {
        public string Name { get; set; }//Имя викторины
        public IMyXmlFile XmlFile { get; set; }//файл 
        public List<ITableItem> TableItems { get; set; }// таблица элементов
        public string Path { get; set; }//путь

        public ResultTable() 
        {
            Name = "";
            XmlFile = new MyXmlFile();
            TableItems = new List<ITableItem>();
            Path = "";
        }
        public void SaveResultTable()
        {
            XmlFile.SaveResultTable("results.xml",this);
        }

        public string Top20(string name)
        {
            TableItems = XmlFile.GetResultTable("results.xml").TableItems;
            string resultop20= "";
            List<TableItem> list = new List<TableItem>();

            foreach (TableItem item in TableItems)
            {
                if(item.NameVictorina == name)
                    list.Add(item);
            }
            TableItem ti = new TableItem();
            list.Sort(ti);
            list.Reverse();
            for (int i = 0; i < 20; i++)
            {
                resultop20 += list[i].ToString() + "\n";
            }
            return resultop20;
            
        }

        public string AllResults(string name)
        {
            TableItems = XmlFile.GetResultTable("results.xml").TableItems;
            string resultall = "";
            List<TableItem> list = new List<TableItem>();

            foreach (TableItem item in TableItems)
            {
                if (item.NameVictorina == name)
                    list.Add(item);
            }
            TableItem ti = new TableItem();
            list.Sort(ti);
            list.Reverse();
            foreach (TableItem tableItem in list)
            {
                resultall += tableItem.ToString() + "\n";
            }
            return resultall;
        }
    }
}

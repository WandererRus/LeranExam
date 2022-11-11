using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeranExam
{
    internal class TableItem : ITableItem, IComparable<TableItem>, IComparer<TableItem>
    {
        public int Score { get; set; }
        public string NameUser { get; set; }
        public string NameVictorina { get; set; }

        public override string ToString()
        {
            return $"{NameVictorina} | {NameUser} | {Score}";
        }

        public int CompareTo(TableItem? other)
        {
            return this.Score.CompareTo(other.Score);
        }

        public int Compare(TableItem? x, TableItem? y)
        {
            return x.CompareTo(y);
        }

        public TableItem() 
        {
            Score = 0;
            NameUser = "";
            NameVictorina = "";
        }
    }
}

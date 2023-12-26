using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNOW
{
    public class CellNumber : Cell
    {
        public int Num { get; set; }

        public CellNumber(int num) : base()
        {
            Num = num;
            S = $"|  {Num}  |";
            
        }
        public CellNumber(string s) : base()
        {
            S = s;
        }

        public override void PrintCell()
        {
            Console.Write(S);

        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNOW
{
    public class CellWithSnow : Cell
    {
      

        public CellWithSnow() : base()
        {
            S =  $"|  *  |";
            

        }
        public CellWithSnow(string s) : base()
        {
            S = s;
        }

        public override void PrintCell()
        {
            Console.Write(S);

        }
    }
}

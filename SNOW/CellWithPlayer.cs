using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNOW
{
    public class CellWithPlayer : Cell
    {
       

        public CellWithPlayer () : base()
        {
            S = $"| >i< |";
        }
        public CellWithPlayer(string s) : base()
        {
            S = s;
        }

        public override void PrintCell()
        {
            Console.Write(S);

        }

    }
}

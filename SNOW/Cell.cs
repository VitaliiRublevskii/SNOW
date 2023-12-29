using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNOW
{
    public class Cell
    {
        public string S { get; set; }
        public char C { get; set; }


        public Cell () {
            S = $"|     |";
            C = ' ';
            
        }
        public Cell (string s) {
            S = s;
            C = ' ';
        }

        public virtual void PrintCell ()
        {
            Console.Write(S);

        }

        public  string GetCell()
        {
            return this.S;
        }


    }
}

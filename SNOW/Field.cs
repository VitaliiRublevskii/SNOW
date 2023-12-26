using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNOW
{
    public class Field
    {
        public List <Cell> Cells {  get; set; }

        public int Count { get; set; }

        public Field() { }

        public Field (int count)
        {
            List < Cell > Cells = new List<Cell> (count);
        }

        

        //  Add,   Remove
    }
}

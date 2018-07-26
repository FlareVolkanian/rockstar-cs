using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS
{
    class CSLine
    {
        public int LineNumber { get; set; }
        public string CS { get; set; }
        public CSLine(string CS, int LineNumber)
        {
            this.CS = CS;
            this.LineNumber = LineNumber;
        }
    }
}

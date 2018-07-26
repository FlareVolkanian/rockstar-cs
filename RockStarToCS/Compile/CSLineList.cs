using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Compile
{
    class CSLineList : List<CSLine>
    {
        public void Add(string CS, int LineNumber)
        {
            this.Add(new CSLine(CS, LineNumber));
        }
    }
}

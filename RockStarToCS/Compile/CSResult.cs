using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Compile
{
    class CSResult
    {
        public CSLineList GeneratedCS { get; set; }
        public BuildVariableType? ReturnType { get; set; }
    }
}

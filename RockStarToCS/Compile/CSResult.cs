using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS
{
    class CSResult
    {
        public CSLineList GeneratedCS { get; set; }
        public VariableType? ReturnType { get; set; }
    }
}

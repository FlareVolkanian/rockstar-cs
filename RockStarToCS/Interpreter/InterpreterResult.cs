using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Interpreter
{
    class InterpreterResult
    {
        public InterpreterVariableType Type { get; set; }
        public object Value { get; set; }

        public InterpreterResult()
        {
            Type = InterpreterVariableType.Null;
            Value = null;
        }
    }
}

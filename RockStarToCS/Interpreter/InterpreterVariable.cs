using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Interpreter
{
    enum InterpreterVariableType
    {
        Null,
        Undefined,
        Boolean,
        Numeric,
        String,
        Object
    }

    class InterpreterVariable
    {
        public InterpreterVariableType Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }

        public InterpreterVariable(string Name, InterpreterVariableType Type)
        {
            this.Name = Name;
            this.Type = Type;
        }

        public InterpreterVariable(string Name, InterpreterVariableType Type, object Value) : this(Name, Type)
        {
            this.Value = Value;
        }
    }
}

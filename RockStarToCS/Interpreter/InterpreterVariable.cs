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
        Object,
        NaN
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

        public static decimal? GetNumericValueFor(object Value)
        {
            if(Value is InterpreterVariable)
            {
                return (Value as InterpreterVariable).NumericValue;
            }
            if(Value == null)
            {
                return 0;
            }
            if(Value is string)
            {
                return 0;
            }
            if(Value is bool?)
            {
                if((Value as bool?).Value)
                {
                    return 1;
                }
                return 0;
            }
            if(Value is decimal?)
            {
                return Value as decimal?;
            }
            if(Value is double?)
            {
                return (decimal)(Value as double?);
            }
            return null;
        }

        public decimal? NumericValue
        {
            get
            {
                if(Type == InterpreterVariableType.Undefined)
                {
                    return null;
                }
                if(Type == InterpreterVariableType.Boolean)
                {
                    if((Value as bool?).Value)
                    {
                        return 1;
                    }
                    return 0;
                }
                if(Type == InterpreterVariableType.NaN)
                {
                    return null;
                }
                if(Type == InterpreterVariableType.Null)
                {
                    return 0;
                }
                if(Type == InterpreterVariableType.Object)
                {
                    return null;
                }
                if(Type == InterpreterVariableType.String)
                {
                    return null;
                }
                return Value as decimal?;
            }
        }
    }
}

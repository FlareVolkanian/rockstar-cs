using RockStarToCS.Compile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class VariableParseNode : ParseNode
    {
        public bool IsProper { get; set; }
        public bool IsCommon => !IsProper;
        public string Name { get; set; }
        public bool IsLast { get; set; }
        public VariableParseNode(Token T, string Name, bool IsProper) : base(T)
        {
            this.Name = Name;
            this.IsProper = IsProper;
            IsLast = false;
        }

        public VariableParseNode(Token T) : base(T)
        {
            IsLast = true;
            Name = string.Empty;
            IsProper = false;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            if(IsLast)
            {
                if(Env.CurrentContext.LastVariable != null)
                {
                    InterpreterVariable v = Env.CurrentContext.LastVariable;
                    return new InterpreterResult() { Type = v.Type, Value = v.Value };
                }
                throw new InterpreterException("Invalid use of " + T.Value, T);
            }
            if(Env.CurrentContext.VariableExists(Name))
            {
                InterpreterVariable v = Env.CurrentContext.GetVariable(Name);
                return new InterpreterResult() { Value = v.Value, Type = v.Type };
            }
            return new InterpreterResult() { Type = InterpreterVariableType.Undefined };
        }
    }
}

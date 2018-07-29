using RockStarToCS.Compile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class BooleanParseNode : ParseNode
    {
        public bool Value { get; set; }
        public BooleanParseNode(Token T) : base(T)
        {
            Value = T.Name == "TRUE";
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            return new InterpreterResult() { Type = InterpreterVariableType.Boolean, Value = Value };
        }
    }
}

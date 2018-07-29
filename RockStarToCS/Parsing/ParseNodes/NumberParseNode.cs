using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class NumberParseNode : ParseNode
    {
        public decimal Value { get; set; }

        public NumberParseNode(Token T, string NumberValue) : base(T)
        {
            Value = decimal.Parse(NumberValue);
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            return new InterpreterResult() { Value = Value, Type = InterpreterVariableType.Numeric };
        }
    }
}

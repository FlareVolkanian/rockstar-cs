using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class StringParseNode : ParseNode
    {
        public string Value { get; set; }

        public StringParseNode(Token T, string Value) : base(T)
        {
            this.Value = Value;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            throw new NotImplementedException();
        }
    }
}

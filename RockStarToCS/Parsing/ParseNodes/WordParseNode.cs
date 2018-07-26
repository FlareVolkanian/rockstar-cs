using RockStarToCS.Compile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class WordParseNode : ParseNode
    {
        public string Text { get; set; }

        public WordParseNode(Token T) : base(T)
        {
            Text = T.Value;
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

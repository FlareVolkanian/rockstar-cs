using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    /**
     * The purpose of this ParseNode is just to help out in parsing.
     */
    class TokenParseNode : ParseNode
    {
        public TokenParseNode(Token T) : base(T)
        {
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

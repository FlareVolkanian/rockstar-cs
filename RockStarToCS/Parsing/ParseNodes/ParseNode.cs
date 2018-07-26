using RockStarToCS.Compile;
using RockStarToCS.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Parsing.ParseNodes
{
    abstract class ParseNode
    {
        public Token T { get; set; }

        public ParseNode(Token T)
        {
            this.T = T;
        }

        public abstract CSResult BuildToCS(BuildEnvironment Env);
        public abstract InterpreterResult Interpret(InterpreterEnvironment Env);
    }
}

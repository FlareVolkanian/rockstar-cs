using RockStarToCS.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Interpreter
{
    class InterpreterException : Exception
    {
        public Token T { get; private set; }
        public InterpreterException(string Message, Token T) : base(Message + " on line: " + T.LineNumber)
        {
            this.T = T;
        }
    }
}

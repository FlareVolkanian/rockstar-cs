using RockStarToCS.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS
{
    class GenCSException : Exception
    {
        public Token T { get; set; }
        public GenCSException(string Message, Token T) : base(Message + " on line: " + T.LineNumber)
        {
            this.T = T;
        }
    }
}

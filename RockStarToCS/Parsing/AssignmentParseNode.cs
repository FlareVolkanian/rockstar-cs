using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Parsing
{
    class AssignmentParseNode : ParseNode
    {
        public ParseNode Value { get; set; }
        public VariableParseNode Variable { get; set; }
        public AssignmentParseNode(Token T, ParseNode Value, VariableParseNode Variable) : base(T)
        {
            this.Value = Value;
            this.Variable = Variable;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Parsing
{
    class EmptyLineParseNode : ParseNode
    {
        public EmptyLineParseNode(Token T) : base(T) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Parsing
{
    class WordParseNode : ParseNode
    {
        public string Text { get; set; }

        public WordParseNode(Token T) : base(T)
        {
            Text = T.Value;
        }
    }
}

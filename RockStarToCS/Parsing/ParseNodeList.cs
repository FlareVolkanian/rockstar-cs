using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Parsing
{
    class ParseNodeList : ParseNode
    {
        private List<ParseNode> Nodes;
        public ParseNodeList() : base(null)
        {
            Nodes = new List<ParseNode>();
        }

        public ParseNodeList(ParseNode Node) : this()
        {
            Add(Node);
        }

        public void Add(ParseNode Node)
        {
            if(Node is ParseNodeList)
            {
                Nodes.AddRange((Node as ParseNodeList).Nodes);
            }
            else
            {
                Nodes.Add(Node);
            }
        }

        public List<ParseNode> GetNodes()
        {
            return Nodes;
        }
    }
}

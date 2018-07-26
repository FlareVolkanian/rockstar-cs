using RockStarToCS.Compile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
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

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            CSLineList cs = new CSLineList();
            foreach(ParseNode node in Nodes)
            {
                CSResult result = node.BuildToCS(Env);
                if(result.GeneratedCS != null)
                {
                    cs.AddRange(result.GeneratedCS);
                }
            }
            return new CSResult() { GeneratedCS = cs, ReturnType = null };
        }

        public List<ParseNode> GetNodes()
        {
            return Nodes;
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            foreach(ParseNode node in Nodes)
            {
                node.Interpret(Env);
            }
            return new InterpreterResult();
        }
    }
}

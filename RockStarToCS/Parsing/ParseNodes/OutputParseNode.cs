using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Parsing
{
    class OutputParseNode : ParseNode
    {
        public ParseNode ToSay { get; set; }

        public OutputParseNode(Token T, ParseNode ToSay) : base(T)
        {
            this.ToSay = ToSay;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            if(ToSay is VariableParseNode)
            {
                VariableParseNode vpn = ToSay as VariableParseNode;
                if(Env.CurrentContext.VariableExists(vpn.Name))
                {
                    BuildVariable variable = Env.CurrentContext.GetVariable(vpn.Name);
                    return new CSResult() { GeneratedCS = new CSLineList() { new CSLine("Console.Write(" + variable.Name + ");", T.LineNumber) } };
                }
                return new CSResult() { GeneratedCS = new CSLineList() { new CSLine("Console.Write(\"mysterious\");", T.LineNumber) } };
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}

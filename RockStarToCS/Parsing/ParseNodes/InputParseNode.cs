using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Parsing
{
    class InputParseNode : ParseNode
    {
        public VariableParseNode Variable;

        public InputParseNode(Token T, VariableParseNode Variable) : base(T)
        {
            this.Variable = Variable;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            CSLineList cs = new CSLineList();
            if (Env.CurrentContext.VariableExists(Variable.Name))
            {
                BuildVariable existingVar = Env.CurrentContext.GetVariable(Variable.Name);
                if(existingVar.Type != VariableType.String)
                {
                    existingVar.Type = VariableType.String;
                    existingVar.CodeCount++;
                    cs.Add(existingVar.CSType + " " + existingVar.CodeName + " = Console.ReadLine();", T.LineNumber);
                }
                else
                {
                    cs.Add(existingVar.CodeName + " = Console.ReadLine();", T.LineNumber);
                }
            }
            else
            {
                BuildVariable newVar = new BuildVariable(VariableType.String, Variable.Name);
                Env.CurrentContext.AddVariable(newVar);
                cs.Add(newVar.CSType + " " + newVar.CodeName + " = Console.ReadLine();", T.LineNumber);
            }
            return new CSResult() { GeneratedCS = cs };
        }
    }
}

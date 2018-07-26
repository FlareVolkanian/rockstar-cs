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

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            VariableType typeOfAssignment = VariableType.Undefined;
            string value = "null";
            if(Value is BooleanParseNode)
            {
                typeOfAssignment = VariableType.Boolean;
                value = (Value as BooleanParseNode).Value ? "true" : "false";
            }
            else if(Value is NullParseNode)
            {
                typeOfAssignment = VariableType.Null;
                value = "null";
            }
            else if(Value is ParseNodeList)
            {
                typeOfAssignment = VariableType.Numeric;
                value = "";
                List<ParseNode> wordNodes = (Value as ParseNodeList).GetNodes();
                //the nodes are parsed in backwards so they need to be reversed
                //otherwise "Tommy was a lean mean wrecking machine" is interpreted as "Tommy was machine wrecking mean lean a"
                wordNodes.Reverse();
                wordNodes.ForEach(pn => value += (pn as WordParseNode).Text.Length % 10);
            }

            CSLineList cs = new CSLineList();

            if(Env.CurrentContext.VariableExists(Variable.Name))
            {
                Variable existingVar = Env.CurrentContext.GetVariable(Variable.Name);
                if(existingVar.Type != typeOfAssignment)
                {
                    existingVar.CodeCount++;
                    cs.Add(existingVar.CSType + " " + existingVar.CodeName + " = " + value + ";", T.LineNumber);
                }
                else
                {
                    cs.Add(existingVar.CodeName + " = " + value + ";", T.LineNumber);
                }
            }
            else
            {
                Variable newVariable = new Variable(typeOfAssignment, Variable.Name);
                Env.CurrentContext.AddVariable(newVariable);
                cs.Add(newVariable.CSType + " " + newVariable.CodeName + " = " + value + ";", T.LineNumber);
            }
            return new CSResult() { GeneratedCS = cs, ReturnType = null };
        }
    }
}

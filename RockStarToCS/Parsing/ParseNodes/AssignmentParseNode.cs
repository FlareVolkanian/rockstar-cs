using RockStarToCS.Compile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
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
            BuildVariableType typeOfAssignment = BuildVariableType.Undefined;
            string value = "null";
            if(Value is BooleanParseNode)
            {
                typeOfAssignment = BuildVariableType.Boolean;
                value = (Value as BooleanParseNode).Value ? "true" : "false";
            }
            else if(Value is NullParseNode)
            {
                typeOfAssignment = BuildVariableType.Null;
                value = "null";
            }
            else if(Value is ParseNodeList)
            {
                typeOfAssignment = BuildVariableType.Numeric;
                value = "";
                List<ParseNode> wordNodes = (Value as ParseNodeList).GetNodes();
                wordNodes.ForEach(pn => value += (pn as WordParseNode).Text.Length % 10);
            }
            else if(Value is StringParseNode)
            {
                typeOfAssignment = BuildVariableType.String;
                value = (Value as StringParseNode).Value;
            }

            CSLineList cs = new CSLineList();

            if(Env.CurrentContext.VariableExists(Variable.Name))
            {
                BuildVariable existingVar = Env.CurrentContext.GetVariable(Variable.Name);
                if(existingVar.Type != typeOfAssignment)
                {
                    existingVar.CodeCount++;
                    existingVar.Type = typeOfAssignment;
                    cs.Add(existingVar.CSType + " " + existingVar.CodeName + " = " + value + ";", T.LineNumber);
                }
                else
                {
                    cs.Add(existingVar.CodeName + " = " + value + ";", T.LineNumber);
                }
            }
            else
            {
                BuildVariable newVariable = new BuildVariable(typeOfAssignment, Variable.Name);
                Env.CurrentContext.AddVariable(newVariable);
                cs.Add(newVariable.CSType + " " + newVariable.CodeName + " = " + value + ";", T.LineNumber);
            }
            return new CSResult() { GeneratedCS = cs, ReturnType = null };
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            InterpreterVariableType typeOfAssignment = InterpreterVariableType.Null;
            object value = null;
            if(Value is ParseNodeList)
            {
                typeOfAssignment = InterpreterVariableType.Numeric;
                string strValue = "";
                List<ParseNode> wordNodes = (Value as ParseNodeList).GetNodes();
                wordNodes.ForEach(pn => strValue += (pn as WordParseNode).Text.Replace("'", "").Length % 10);
                value = decimal.Parse(strValue);
            }
            else
            {
                InterpreterResult rhsResult = Value.Interpret(Env);
                typeOfAssignment = rhsResult.Type;
                value = rhsResult.Value;
            }
            InterpreterVariable variable = null;
            if(Variable.IsLast)
            {
                if(Env.CurrentContext.LastVariable != null)
                {
                    variable = Env.CurrentContext.LastVariable;
                }
                else
                {
                    throw new InterpreterException("Invalid use of " + Variable.T.Value, Variable.T);
                }
            }
            else if(Env.CurrentContext.VariableExists(Variable.Name))
            {
                variable = Env.CurrentContext.GetVariable(Variable.Name);
            }
            else
            {
                variable = new InterpreterVariable(Variable.Name, typeOfAssignment);
                Env.CurrentContext.AddVariable(variable);
            }
            if(variable.Type != typeOfAssignment)
            {
                variable.Type = typeOfAssignment;
            }
            variable.Value = value;
            return new InterpreterResult() { Type = InterpreterVariableType.Null };
        }
    }
}

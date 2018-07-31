using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class StringLiteralAssignmentParseNode : ParseNode
    {
        public VariableParseNode Variable { get; set; }
        public ParseNodeList Words { get; set; }

        public StringLiteralAssignmentParseNode(Token T, VariableParseNode Variable, ParseNodeList Words) : base(T)
        {
            this.Variable = Variable;
            this.Words = Words;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            InterpreterVariable variable = null;
            if(Variable.IsLast)
            {
                if(Env.CurrentContext.LastVariable == null)
                {
                    throw new InterpreterException("Invalid use of " + Variable.T.Value, Variable.T);
                }
                variable = Env.CurrentContext.LastVariable;
            }
            else if(Env.CurrentContext.VariableExists(Variable.Name))
            {
                variable = Env.CurrentContext.GetVariable(Variable.Name);
            }
            else
            {
                variable = new InterpreterVariable(Variable.Name, InterpreterVariableType.Undefined);
                Env.CurrentContext.AddVariable(variable);
            }
            variable.Type = InterpreterVariableType.String;
            string value = "";
            List<ParseNode> words = Words.GetNodes();
            foreach(ParseNode node in words)
            {
                WordParseNode word = node as WordParseNode;
                value += word.Text + " ";
            }
            if(value.Length > 0)
            {
                value = value.Substring(0, value.Length - 1);
            }
            variable.Value = value;
            return new InterpreterResult();
        }
    }
}

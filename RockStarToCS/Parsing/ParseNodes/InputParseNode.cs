using RockStarToCS.Compile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
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
                if(existingVar.Type != BuildVariableType.String)
                {
                    existingVar.Type = BuildVariableType.String;
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
                BuildVariable newVar = new BuildVariable(BuildVariableType.String, Variable.Name);
                Env.CurrentContext.AddVariable(newVar);
                cs.Add(newVar.CSType + " " + newVar.CodeName + " = Console.ReadLine();", T.LineNumber);
            }
            return new CSResult() { GeneratedCS = cs };
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
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
                variable = new InterpreterVariable(Variable.Name, InterpreterVariableType.String);
                Env.CurrentContext.AddVariable(variable);
            }
            if(variable.Type != InterpreterVariableType.String)
            {
                variable.Type = InterpreterVariableType.String;
            }
            variable.Value = Console.ReadLine();
            return new InterpreterResult() { Type = InterpreterVariableType.Null };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class PutIntoParseNode : ParseNode
    {
        public ParseNode Value { get; set; }
        public VariableParseNode Variable { get; set; }

        public PutIntoParseNode(Token T, VariableParseNode Variable, ParseNode Value) : base(T)
        {
            this.Value = Value;
            this.Variable = Variable;
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
            InterpreterResult result = Value.Interpret(Env);
            variable.Type = result.Type;
            variable.Value = result.Value;
            return new InterpreterResult() { Value = variable.Value, Type = result.Type };
        }
    }
}

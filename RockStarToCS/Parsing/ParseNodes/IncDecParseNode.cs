using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class IncDecParseNode : ParseNode
    {
        public VariableParseNode Variable { get; set; }
        public bool Up { get; set; }

        public IncDecParseNode(Token T, VariableParseNode Variable, bool Up) : base(T)
        {
            this.Variable = Variable;
            this.Up = Up;
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
                    throw new InterpreterException(Variable.Name + " is mysterious", T);
                }
                variable = Env.CurrentContext.LastVariable;
            }
            else if(!Env.CurrentContext.VariableExists(Variable.Name))
            {
                throw new InterpreterException(Variable.Name + " is mysterious", T);
            }
            else
            {
                variable = Env.CurrentContext.GetVariable(Variable.Name);
            }

            if (variable.Type == InterpreterVariableType.Undefined)
            {
                throw new InterpreterException(Variable.Name + " is mysterious", T);
            }

            if (Up)
            {
                if (variable.Type == InterpreterVariableType.Boolean)
                {
                    if ((variable.Value as bool?).Value)
                    {
                        variable.Type = InterpreterVariableType.Numeric;
                        variable.Value = 2;
                    }
                    else
                    {
                        variable.Type = InterpreterVariableType.Numeric;
                        variable.Value = 1;
                    }
                }
                else if (variable.Type == InterpreterVariableType.Null)
                {
                    variable.Type = InterpreterVariableType.Numeric;
                    variable.Value = 1;
                }
                else if (variable.Type == InterpreterVariableType.Object || variable.Type == InterpreterVariableType.String)
                {
                    variable.Type = InterpreterVariableType.Numeric;
                    variable.Value = double.NaN;
                }
                else
                {
                    variable.Value = (variable.Value as decimal?).Value + 1;
                }
            }
            else
            {
                if (variable.Type == InterpreterVariableType.Boolean)
                {
                    if ((variable.Value as bool?).Value)
                    {
                        variable.Type = InterpreterVariableType.Numeric;
                        variable.Value = 0;
                    }
                    else
                    {
                        variable.Type = InterpreterVariableType.Numeric;
                        variable.Value = -1;
                    }
                }
                else if (variable.Type == InterpreterVariableType.Null)
                {
                    variable.Type = InterpreterVariableType.Numeric;
                    variable.Value = -1;
                }
                else if (variable.Type == InterpreterVariableType.Object || variable.Type == InterpreterVariableType.String)
                {
                    variable.Type = InterpreterVariableType.Numeric;
                    variable.Value = double.NaN;
                }
                else
                {
                    variable.Value = (variable.Value as decimal?).Value - 1;
                }
            }
            return new InterpreterResult() { Type = variable.Type, Value = variable.Value };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class AdditionParseNode : ParseNode
    {
        public ParseNode LHS { get; set; }
        public ParseNode RHS { get; set; }

        public AdditionParseNode(Token T, ParseNode LHS, ParseNode RHS) : base(T)
        {
            this.LHS = LHS;
            this.RHS = RHS;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            InterpreterResult lhsResult = LHS.Interpret(Env);
            InterpreterResult rhsResult = RHS.Interpret(Env);
            //string concatenation
            if(lhsResult.Type == InterpreterVariableType.String || rhsResult.Type == InterpreterVariableType.String)
            {
                string lhsString = "";
                string rhsString = "";
                if (lhsResult.Type == InterpreterVariableType.Undefined)
                {
                    lhsString = "mysterious";
                }
                else if (lhsResult.Type == InterpreterVariableType.Null)
                {
                    rhsString = "null";
                }
                else if (lhsResult.Type == InterpreterVariableType.NaN)
                {
                    lhsString = "NaN";
                }
                else
                {
                    lhsString = lhsResult.Value.ToString();
                }

                if (rhsResult.Type == InterpreterVariableType.Undefined)
                {
                    rhsString = "mysterious";
                }
                else if (rhsResult.Type == InterpreterVariableType.Null)
                {
                    rhsString = "null";
                }
                else if (rhsResult.Type == InterpreterVariableType.NaN)
                {
                    rhsString = "NaN";
                }
                else
                {
                    rhsString = rhsResult.Value.ToString();
                }
                return new InterpreterResult() { Value = lhsString + rhsString, Type = InterpreterVariableType.String };
            }

            if (lhsResult.Type == InterpreterVariableType.Undefined || rhsResult.Type == InterpreterVariableType.Undefined ||
                lhsResult.Value == null || rhsResult.Value == null)
            {
                throw new InterpreterException("Unable to add mysterious", T);
            }
            decimal? lhs = InterpreterVariable.GetNumericValueFor(lhsResult.Value);
            decimal? rhs = InterpreterVariable.GetNumericValueFor(rhsResult.Value);
            if (!lhs.HasValue || !rhs.HasValue)
            {
                throw new InterpreterException("Unable to add mysterious", T);
            }
            return new InterpreterResult() { Type = InterpreterVariableType.Numeric, Value = lhs.Value + rhs.Value };
        }
    }
}

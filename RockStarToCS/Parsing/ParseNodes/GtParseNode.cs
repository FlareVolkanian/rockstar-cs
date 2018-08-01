using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class GtParseNode : ParseNode
    {
        public ParseNode LHS { get; set; }
        public ParseNode RHS { get; set; }

        public GtParseNode(Token T, ParseNode LHS, ParseNode RHS) : base(T)
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

            InterpreterResult trueResult = new InterpreterResult() { Value = true, Type = InterpreterVariableType.Boolean };
            InterpreterResult falseResult = new InterpreterResult() { Value = false, Type = InterpreterVariableType.Boolean };

            if (lhsResult.Value == null)
            {
                return falseResult;
            }

            if(rhsResult.Value == null)
            {
                return trueResult;
            }

            if (lhsResult.Type == InterpreterVariableType.NaN || rhsResult.Type == InterpreterVariableType.NaN)
            {
                return falseResult;
            }

            if (rhsResult.Type == InterpreterVariableType.Null)
            {
                return trueResult;
            }

            if (lhsResult.Type == InterpreterVariableType.Null)
            {
                return trueResult;
            }

            if (lhsResult.Type == InterpreterVariableType.Undefined || rhsResult.Type == InterpreterVariableType.Undefined)
            {
                return falseResult;
            }

            if (lhsResult.Type == InterpreterVariableType.Numeric && rhsResult.Type == InterpreterVariableType.Numeric)
            {
                if ((lhsResult.Value as decimal?).Value > (rhsResult.Value as decimal?).Value)
                {
                    return trueResult;
                }
                return falseResult;
            }

            if (lhsResult.Type == InterpreterVariableType.String || lhsResult.Type == InterpreterVariableType.String)
            {
                return falseResult;
            }

            decimal? lhsNumVal = InterpreterVariable.GetNumericValueFor(lhsResult.Value);
            decimal? rhsNumVal = InterpreterVariable.GetNumericValueFor(rhsResult.Value);
            if (lhsNumVal.HasValue && rhsNumVal.HasValue && lhsNumVal.Value > rhsNumVal.Value)
            {
                return trueResult;
            }
            return falseResult;
        }
    }
}

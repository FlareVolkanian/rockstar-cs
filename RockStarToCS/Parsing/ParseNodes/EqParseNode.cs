using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class EqParseNode : ParseNode
    {
        public ParseNode LHS { get; set; }
        public ParseNode RHS { get; set; }

        public EqParseNode(Token T, ParseNode LHS, ParseNode RHS) : base(T)
        {
            this.LHS = LHS;
            this.RHS = RHS;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }

        public static InterpreterResult InterpretBoolResult(ParseNode LHS, ParseNode RHS, InterpreterEnvironment Env)
        {
            InterpreterResult lhsResult = LHS.Interpret(Env);
            InterpreterResult rhsResult = RHS.Interpret(Env);

            InterpreterResult trueResult = new InterpreterResult() { Value = true, Type = InterpreterVariableType.Boolean };
            InterpreterResult falseResult = new InterpreterResult() { Value = false, Type = InterpreterVariableType.Boolean };

            if (lhsResult.Value == null && rhsResult.Value == null)
            {
                return trueResult;
            }

            if (lhsResult.Type == InterpreterVariableType.NaN && rhsResult.Type == InterpreterVariableType.NaN)
            {
                return trueResult;
            }

            if (lhsResult.Type == InterpreterVariableType.Null && rhsResult.Type == InterpreterVariableType.Null)
            {
                return trueResult;
            }

            if (lhsResult.Type == InterpreterVariableType.Undefined && rhsResult.Type == InterpreterVariableType.Undefined)
            {
                return trueResult;
            }

            if (lhsResult.Type == InterpreterVariableType.Numeric && rhsResult.Type == InterpreterVariableType.Numeric)
            {
                if ((lhsResult.Value as decimal?).Value == (rhsResult.Value as decimal?).Value)
                {
                    return trueResult;
                }
                return falseResult;
            }

            if (lhsResult.Type == InterpreterVariableType.String && lhsResult.Type == InterpreterVariableType.String)
            {
                if (lhsResult.Value as string == rhsResult.Value as string)
                {
                    return trueResult;
                }
                return falseResult;
            }

            decimal? lhsNumVal = InterpreterVariable.GetNumericValueFor(lhsResult.Value);
            decimal? rhsNumVal = InterpreterVariable.GetNumericValueFor(rhsResult.Value);
            if (lhsNumVal.HasValue && rhsNumVal.HasValue && lhsNumVal.Value == rhsNumVal.Value)
            {
                return trueResult;
            }
            return falseResult;
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            return InterpretBoolResult(LHS, RHS, Env);
        }
    }
}

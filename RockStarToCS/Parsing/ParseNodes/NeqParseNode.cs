using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class NeqParseNode : ParseNode
    {
        public ParseNode LHS { get; set; }
        public ParseNode RHS { get; set; }

        public NeqParseNode(Token T, ParseNode LHS, ParseNode RHS) : base(T)
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
            InterpreterResult eqResult = EqParseNode.InterpretBoolResult(LHS, RHS, Env);
            if((eqResult.Value as bool?).Value)
            {
                return new InterpreterResult() { Value = false, Type = InterpreterVariableType.Boolean };
            }
            return new InterpreterResult() { Value = true, Type = InterpreterVariableType.Boolean };
        }
    }
}

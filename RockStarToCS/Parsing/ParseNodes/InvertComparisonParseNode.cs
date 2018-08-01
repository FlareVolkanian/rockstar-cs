using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class InvertComparisonParseNode : ParseNode
    {
        public ParseNode Comparison { get; set; }

        public InvertComparisonParseNode(Token T, ParseNode Comparison) : base(T)
        {
            this.Comparison = Comparison;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            InterpreterResult comparisonResult = Comparison.Interpret(Env);
            if((comparisonResult.Value as bool?).Value)
            {
                return new InterpreterResult() { Value = false, Type = InterpreterVariableType.Boolean };
            }
            return new InterpreterResult() { Value = true, Type = InterpreterVariableType.Boolean };
        }
    }
}

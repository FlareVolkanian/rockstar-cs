using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class UntilParseNode : ParseNode
    {
        public ParseNode Condition { get; set; }
        public ParseNode Block { get; set; }

        public UntilParseNode(Token T, ParseNode Condition, ParseNode Block) : base(T)
        {
            this.Condition = Condition;
            this.Block = Block;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            InterpreterResult result = null;
            while ((result = Condition.Interpret(Env)).Type == InterpreterVariableType.Boolean && !(result.Value as bool?).Value)
            {
                Block.Interpret(Env);
            }
            return new InterpreterResult();
        }
    }
}

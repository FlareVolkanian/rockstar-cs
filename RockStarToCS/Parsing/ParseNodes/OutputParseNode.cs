using RockStarToCS.Compile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class OutputParseNode : ParseNode
    {
        public ParseNode ToSay { get; set; }

        public OutputParseNode(Token T, ParseNode ToSay) : base(T)
        {
            this.ToSay = ToSay;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            if(ToSay is VariableParseNode)
            {
                VariableParseNode vpn = ToSay as VariableParseNode;
                if(Env.CurrentContext.VariableExists(vpn.Name))
                {
                    BuildVariable variable = Env.CurrentContext.GetVariable(vpn.Name);
                    return new CSResult() { GeneratedCS = new CSLineList() { new CSLine("Console.Write(" + variable.Name + ");", T.LineNumber) } };
                }
                return new CSResult() { GeneratedCS = new CSLineList() { new CSLine("Console.Write(\"mysterious\");", T.LineNumber) } };
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            object value = null;
            if(ToSay is VariableParseNode)
            {
                VariableParseNode variableNode = ToSay as VariableParseNode;
                InterpreterVariable variable = null;
                if(variableNode.IsLast)
                {
                    if(Env.CurrentContext.LastVariable != null)
                    {
                        variable = Env.CurrentContext.LastVariable;
                    }
                    else
                    {
                        throw new InterpreterException("Invalid use of " + variableNode.T.Value, variableNode.T);
                    }
                }
                else if(Env.CurrentContext.VariableExists(variableNode.Name))
                {
                    variable = Env.CurrentContext.GetVariable(variableNode.Name);
                }
                else
                {
                    Console.Write("mysterious");
                    return new InterpreterResult();
                }
                if(variable.Type == InterpreterVariableType.Null)
                {
                    Console.Write("null");
                    return new InterpreterResult();
                }
                if(variable.Type == InterpreterVariableType.Undefined)
                {
                    Console.Write("mysterious");
                    return new InterpreterResult();
                }
                value = variable.Value;
            }
            else
            {
                InterpreterResult result = ToSay.Interpret(Env);
                if(result.Type == InterpreterVariableType.Null)
                {
                    Console.Write("null");
                }
                else if(result.Type == InterpreterVariableType.Undefined)
                {
                    Console.Write("mysterious");
                }
                else
                {
                    value = result.Value;
                }
            }
            Console.Write(value);
            return new InterpreterResult();
        }
    }
}

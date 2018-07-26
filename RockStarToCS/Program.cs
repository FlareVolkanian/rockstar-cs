using RockStarToCS.Compile;
using RockStarToCS.Interpreter;
using RockStarToCS.Parsing;
using RockStarToCS.Parsing.ParseNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS
{
    class Program
    {
        static void Main(string[] args)
        {
            //string test = "Tommy was a lean mean wrecking machine\nShout Tommy";
            string test = "Listen Tommy\nShout Tommy";
            Tokeniser tok = new Tokeniser();
            List<Token> tokens = tok.Tokenise(test);
            Parser parser = new Parser();
            ParseNode root = parser.Parse(tokens);

            if (root != null)
            {
                /*BuildEnvironment env = new BuildEnvironment();

                CSResult result = root.BuildToCS(env);
                string code = "";
                result.GeneratedCS.ForEach(cs => code += cs.CS + "\n");
                Console.WriteLine(code);*/

                InterpreterEnvironment env = new InterpreterEnvironment();
                root.Interpret(env);
            }
            Console.ReadKey();
        }
    }
}

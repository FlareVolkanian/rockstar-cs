using RockStarToCS.Compile;
using RockStarToCS.Interpreter;
using RockStarToCS.Parsing;
using RockStarToCS.Parsing.ParseNodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("usage: rockstart.exe rockstarfile");
                Environment.Exit(1);
            }
            if(!File.Exists(args[0]))
            {
                Console.WriteLine("Unable to open file");
                Environment.Exit(1);
            }
            string text = File.ReadAllText(args[0]);
            Tokeniser tok = new Tokeniser();
            List<Token> tokens = tok.Tokenise(text);
            Parser parser = new Parser();
            ParseNode root = parser.Parse(tokens);

            if (root != null)
            {
                /*BuildEnvironment env = new BuildEnvironment();

                CSResult result = root.BuildToCS(env);
                string code = "";
                result.GeneratedCS.ForEach(cs => code += cs.CS + "\n");
                Console.WriteLine(code);*/

                try
                {
                    InterpreterEnvironment env = new InterpreterEnvironment();
                    root.Interpret(env);
                }
                catch(InterpreterException ie)
                {
                    Console.WriteLine("Error: " + ie.Message + " on line: " + ie.T.LineNumber);
#if DEBUG
                    Console.ReadLine();
#endif
                    Environment.Exit(1);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: The interpreter encountered a problem: " + ex.Message);
#if DEBUG
                    Console.WriteLine(ex.StackTrace);
                    Console.ReadLine();
#endif
                    Environment.Exit(1);
                }
            }
            else
            {
                Console.WriteLine("Syntax error on line: " + parser.HighestLine);
#if DEBUG
                Console.ReadLine();
#endif
                Environment.Exit(1);
            }
#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}

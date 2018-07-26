using RockStarToCS.Parsing;
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
            string test = "Tommy was a lean mean wrecking machine";
            Tokeniser tok = new Tokeniser();
            List<Token> tokens = tok.Tokenise(test);
            Parser parser = new Parser();
            ParseNode root = parser.Parse(tokens);
        }
    }
}

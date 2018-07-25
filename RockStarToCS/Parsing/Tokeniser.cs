using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Parsing
{
    //This code is pretty much copied and modified from another project so it probably will need refactoring at some point
    class Tokeniser
    {
        private List<TokenDefinition> TokDefs;
        private static string WhiteSpace = " \r\t";
        private int line;

        private string InvalidKeyWordEndings = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUV";
        private string LowerCase = "abcdefghijklmnopqrstuvwxyz";

        public Tokeniser()
        {
            TokenDefinitionList defs = new TokenDefinitionList();
            
            //proper variables
            defs.Add(new MatchingTokenDefinition((Text, StrPtr) =>
            {
                string name = "";
                if (TokenDefinition.IsBetween(Text, StrPtr, 'A', 'Z'))
                {
                    while(TokenDefinition.IsBetween(Text, StrPtr, 'A', 'Z') || TokenDefinition.IsBetween(Text, StrPtr, 'a', 'z') || (TokenDefinition.Matches(Text, StrPtr, " ") && TokenDefinition.IsBetween(Text, ++StrPtr, 'A', 'Z')))
                    {
                        name += Text[StrPtr];
                        StrPtr++;
                    }
                    return new TokenDefinition.TokenResult() { NewStrPtr = StrPtr, T = new Token("VAR", name) };
                }
                return null;
            }));

            //common variable specifiers
            defs.Add("a", "CVAR", InvalidKeyWordEndings);
            defs.Add("an", "CVAR", InvalidKeyWordEndings);
            defs.Add("the", "CVAR", InvalidKeyWordEndings);
            defs.Add("my", "CVAR", InvalidKeyWordEndings);
            defs.Add("your", "CVAR", InvalidKeyWordEndings);



            //words
            defs.Add(new MatchingTokenDefinition((Text, StrPtr) =>
            {
                if(TokenDefinition.InSet(Text, StrPtr, LowerCase))
                {
                    string text = "";
                    while(TokenDefinition.InSet(Text, StrPtr, LowerCase))
                    {
                        text += Text[StrPtr];
                        StrPtr++;
                    }
                    return new TokenDefinition.TokenResult() { NewStrPtr = StrPtr, T = new Token("WORD", text) };
                }
                return null;
            }));

            TokDefs = defs;
        }

        public List<Token> Tokenise(string Text)
        {
            List<Token> tokens = new List<Token>();
            line = 1;
            int StrPtr = 0;
            while (true)
            {
                if (StrPtr >= Text.Length)
                {
                    tokens.Add(new Token("EOF", "EOF", line));
                    break;
                }
                //skip white space
                StrPtr = SkipCommentsAndWhiteSpace(Text, StrPtr);
                //look for new lines and empty lines
                if(TokenDefinition.Matches(Text, StrPtr, "\n"))
                {
                    StrPtr++;
                    StrPtr = SkipCommentsAndWhiteSpace(Text, StrPtr);
                    if(TokenDefinition.Matches(Text, StrPtr, "\n"))
                    {
                        tokens.Add(new Token("ELINE", ""));
                        continue;
                    }
                }
                if (StrPtr >= Text.Length)
                {
                    //loop back around to add EOF
                    continue;
                }
                bool found = false;
                foreach (TokenDefinition tc in TokDefs)
                {
                    TokenDefinition.TokenResult tr = null;
                    if (tc is MatchingTokenDefinition)
                    {
                        tr = (tc as MatchingTokenDefinition).Match(Text, StrPtr);
                    }
                    else if (tc is ConstantDefinition)
                    {
                        tr = (tc as ConstantDefinition).Match(Text, StrPtr);
                    }
                    if (tr != null)
                    {
                        tr.T.LineNumber = line;
                        tokens.Add(tr.T);
                        StrPtr = tr.NewStrPtr;
                        found = true;
                        line += tr.LinesAbsorbed;
                        break;
                    }
                }
                if (!found)
                {
                    throw new TokenException("Invalid token found on line: " + line);
                }
            }
            return tokens;
        }

        private int SkipCommentsAndWhiteSpace(string Text, int StrPtr)
        {
            //skip white space
            StrPtr = SkipWhiteSpace(Text, StrPtr);
            //skip comments
            StrPtr = SkipComments(Text, StrPtr);
            //skip more white space
            StrPtr = SkipWhiteSpace(Text, StrPtr);
            return StrPtr;
        }

        private int SkipComments(string Text, int StrPtr)
        {
            if (TokenDefinition.Matches(Text, StrPtr, "/*"))
            {
                while (!TokenDefinition.Matches(Text, StrPtr, "*/"))
                {
                    if (TokenDefinition.Matches(Text, StrPtr, "\n"))
                    {
                        line++;
                    }
                    StrPtr++;
                }
                StrPtr += 2;
            }
            else if (TokenDefinition.Matches(Text, StrPtr, "//"))
            {
                while (!TokenDefinition.Matches(Text, StrPtr, "\n"))
                {
                    StrPtr++;
                }
            }
            return StrPtr;
        }

        private static int SkipWhiteSpace(string Text, int StrPtr)
        {
            while (TokenDefinition.InSet(Text, StrPtr, WhiteSpace))
            {
                StrPtr++;
            }
            return StrPtr;
        }
    }

    class TokenException : Exception
    {
        public TokenException(string Message) : base(Message) { }
    }

    abstract class TokenDefinition
    {
        public class TokenResult
        {
            public Token T { get; set; }
            public int NewStrPtr { get; set; }
            public int LinesAbsorbed { get; set; }
        }

        public static bool Matches(string Text, int StrPtr, string TextToMatch)
        {
            if (StrPtr >= Text.Length)
            {
                return false;
            }
            for (int i = 0; i < TextToMatch.Length && i + StrPtr < Text.Length; ++i)
            {
                if (Text[StrPtr + i] != TextToMatch[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool Matches(string Text, int StrPtr, string TextToMatch, string InvalidEndings)
        {
            if (StrPtr >= Text.Length)
            {
                return false;
            }
            for (int i = 0; i < TextToMatch.Length && StrPtr + i < Text.Length; ++i)
            {
                if (Text[StrPtr + i] != TextToMatch[i])
                {
                    return false;
                }
            }
            if (Text.Length > StrPtr + TextToMatch.Length && InvalidEndings.Contains(Text[StrPtr + TextToMatch.Length]))
            {
                return false;
            }
            return true;
        }

        public static bool InSet(string Text, int StrPtr, char[] Chars)
        {
            if (StrPtr < Text.Length && Chars.Contains(Text[StrPtr]))
            {
                return true;
            }
            return false;
        }

        public static bool InSet(string Text, int StrPtr, string Chars)
        {
            if (StrPtr < Text.Length && Chars.Contains(Text[StrPtr]))
            {
                return true;
            }
            return false;
        }

        public static bool IsBetween(string Text, int StrPtr, char Lower, char Upper)
        {
            if(StrPtr < Text.Length && Text[StrPtr] >= Lower && Text[StrPtr] <= Upper)
            {
                return true;
            }
            return false;
        }
    }

    class ConstantDefinition : TokenDefinition
    {
        private string TextToMatch { get; set; }
        private string TokenName { get; set; }
        private string AltTokenName { get; set; }
        private string InvalidEndings { get; set; }

        public ConstantDefinition(string TextToMatch, string TokenName, string InvalidEndings)
        {
            this.TextToMatch = TextToMatch;
            this.TokenName = TokenName;
            this.InvalidEndings = InvalidEndings;
        }

        public ConstantDefinition(string TextToMatch, string TokenName)
        {
            this.TextToMatch = TextToMatch;
            this.TokenName = TokenName;
            InvalidEndings = "";
        }

        public ConstantDefinition(string TextToMatch, string TokenName, string TokenAltName, string InvalidEndings)
        {
            this.TextToMatch = TextToMatch;
            this.TokenName = TokenName;
            this.AltTokenName = TokenAltName;
            this.InvalidEndings = InvalidEndings;
        }

        public TokenResult Match(string Text, int StrPtr)
        {
            if (TokenDefinition.Matches(Text, StrPtr, TextToMatch))
            {
                if (!string.IsNullOrEmpty(InvalidEndings))
                {
                    if (InSet(Text, StrPtr + TextToMatch.Length, InvalidEndings))
                    {
                        return null;
                    }
                }
                if (!string.IsNullOrWhiteSpace(AltTokenName))
                {
                    return new TokenResult() { NewStrPtr = StrPtr + TextToMatch.Length, T = new Token(TokenName, AltTokenName, TextToMatch) };
                }
                return new TokenResult() { NewStrPtr = StrPtr + TextToMatch.Length, T = new Token(TokenName, TextToMatch) };
            }
            return null;
        }

        public ConstantDefinition(string TokenNameAndValue)
        {
            TextToMatch = TokenNameAndValue;
            TokenName = TokenNameAndValue;
            InvalidEndings = "";
        }
    }

    class MatchingTokenDefinition : TokenDefinition
    {
        public delegate TokenResult MatchFunc(string Text, int StrPtr);

        public MatchFunc Match { get; set; }

        public MatchingTokenDefinition(MatchFunc Match)
        {
            this.Match = Match;
        }
    }

    class TokenDefinitionList : List<TokenDefinition>
    {
        public void Add(string TextToMatch, string TokenName, string InvalidEndings)
        {
            Add(new ConstantDefinition(TextToMatch, TokenName, InvalidEndings));
        }

        public void Add(string TextToMatch, string TokenName, string AltTokenName, string InvalidEndings)
        {
            Add(new ConstantDefinition(TextToMatch, TokenName, AltTokenName, InvalidEndings));
        }

        public void Add(string TextToMatch, string TokenName)
        {
            Add(new ConstantDefinition(TextToMatch, TokenName));
        }

        public void Add(string TokenNameAndValue)
        {
            Add(new ConstantDefinition(TokenNameAndValue));
        }

        public void Add(MatchingTokenDefinition.MatchFunc Match)
        {
            Add(new MatchingTokenDefinition(Match));
        }
    }
}

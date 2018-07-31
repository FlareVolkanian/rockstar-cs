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
        private string NumberChars = "0123456789";

        public Tokeniser()
        {
            TokenDefinitionList defs = new TokenDefinitionList();
            
            //most of the tokens are defined here, a couple such as EOF and ELINE (empty line) are defined in the Tokenise method

            //common variable specifiers
            defs.Add("a", "CVARSP", InvalidKeyWordEndings);
            defs.Add("A", "CVARSP", InvalidKeyWordEndings);
            defs.Add("an", "CVARSP", InvalidKeyWordEndings);
            defs.Add("An", "CVARSP", InvalidKeyWordEndings);
            defs.Add("the", "CVARSP", InvalidKeyWordEndings);
            defs.Add("The", "CVARSP", InvalidKeyWordEndings);
            defs.Add("my", "CVARSP", InvalidKeyWordEndings);
            defs.Add("My", "CVARSP", InvalidKeyWordEndings);
            defs.Add("your", "CVARSP", InvalidKeyWordEndings);
            defs.Add("Your", "CVARSP", InvalidKeyWordEndings);

            //last variable/pronouns
            defs.Add("it", "LVAR", InvalidKeyWordEndings);
            defs.Add("he", "LVAR", InvalidKeyWordEndings);
            defs.Add("she", "LVAR", InvalidKeyWordEndings);
            defs.Add("him", "LVAR", InvalidKeyWordEndings);
            defs.Add("her", "LVAR", InvalidKeyWordEndings);
            defs.Add("them", "LVAR", InvalidKeyWordEndings);
            defs.Add("they", "LVAR", InvalidKeyWordEndings);
            defs.Add("ze", "LVAR", InvalidKeyWordEndings);
            defs.Add("hir", "LVAR", InvalidKeyWordEndings);
            defs.Add("zie", "LVAR", InvalidKeyWordEndings);
            defs.Add("zir", "LVAR", InvalidKeyWordEndings);
            defs.Add("xe", "LVAR", InvalidKeyWordEndings);
            defs.Add("xem", "LVAR", InvalidKeyWordEndings);
            defs.Add("ve", "LVAR", InvalidKeyWordEndings);
            defs.Add("ver", "LVAR", InvalidKeyWordEndings);

            //assignment
            defs.Add("put", "PUT", InvalidKeyWordEndings);
            defs.Add("into", "INTO", InvalidKeyWordEndings);

            //poetic literals
            //poetic type literals
            defs.Add("is", "IS", InvalidKeyWordEndings);
            defs.Add("was", "IS", InvalidKeyWordEndings);
            defs.Add("were", "IS", InvalidKeyWordEndings);
            //poetic string literals
            defs.Add("says", "SAYS", InvalidKeyWordEndings);

            //arithmetic
            defs.Add("plus", "ADD", InvalidKeyWordEndings);
            defs.Add("with", "ADD", InvalidKeyWordEndings);
            defs.Add("minus", "SUB", InvalidKeyWordEndings);
            defs.Add("without", "SUB", InvalidKeyWordEndings);
            defs.Add("times", "MULT", InvalidKeyWordEndings);
            defs.Add("of", "MULT", InvalidKeyWordEndings);
            defs.Add("over", "DIV", InvalidKeyWordEndings);
            //defs.Add("by", "DIV", InvalidKeyWordEndings); //looks like this has been removed from the spec

            //increment/decrement
            defs.Add("build", "BLD", InvalidKeyWordEndings);
            defs.Add("Build", "BLD", InvalidKeyWordEndings);
            defs.Add("up", "UP", InvalidKeyWordEndings);
            defs.Add("knock", "KNK", InvalidKeyWordEndings);
            defs.Add("Knock", "KNK", InvalidKeyWordEndings);
            defs.Add("down", "DWN", InvalidKeyWordEndings);

            //I/O
            defs.Add("Say", "SAY", InvalidKeyWordEndings);
            defs.Add("Shout", "SAY", InvalidKeyWordEndings);
            defs.Add("Whisper", "SAY", InvalidKeyWordEndings);
            defs.Add("Listen to", "LSTN", InvalidKeyWordEndings);
            defs.Add("Listen", "LSTN", InvalidKeyWordEndings);

            //types
            //undefined
            defs.Add("mysterious", "UNDEF", InvalidKeyWordEndings);
            //null
            defs.Add("nothing", "NULL", InvalidKeyWordEndings);
            defs.Add("nowhere", "NULL", InvalidKeyWordEndings);
            defs.Add("nobody", "NULL", InvalidKeyWordEndings);
            defs.Add("null", "NULL", InvalidKeyWordEndings);
            defs.Add("empty", "NULL", InvalidKeyWordEndings);
            defs.Add("gone", "NULL", InvalidKeyWordEndings);
            //boolean
            defs.Add("true", "TRUE", InvalidKeyWordEndings);
            defs.Add("right", "TRUE", InvalidKeyWordEndings);
            defs.Add("yes", "TRUE", InvalidKeyWordEndings);
            defs.Add("ok", "TRUE", InvalidKeyWordEndings);
            defs.Add("false", "FALSE", InvalidKeyWordEndings);
            defs.Add("wrong", "FALSE", InvalidKeyWordEndings);
            defs.Add("no", "FALSE", InvalidKeyWordEndings);
            defs.Add("lies", "FALSE", InvalidKeyWordEndings);
            //numeric litterals
            defs.Add(new MatchingTokenDefinition((Text, StrPtr) =>
            {
                if(TokenDefinition.InSet(Text, StrPtr, NumberChars))
                {
                    string value = "";
                    while(TokenDefinition.InSet(Text, StrPtr, NumberChars))
                    {
                        value += Text[StrPtr++];
                    }
                    //allow a single decimal place
                    if(TokenDefinition.Matches(Text, StrPtr, "."))
                    {
                        value += ".";
                        StrPtr++;
                    }
                    while(TokenDefinition.InSet(Text, StrPtr, NumberChars))
                    {
                        value += Text[StrPtr++];
                    }
                    return new TokenDefinition.TokenResult() { NewStrPtr = StrPtr, T = new Token("NUM", value) };
                }
                return null;
            }));
            //string literal
            defs.Add(new MatchingTokenDefinition((Text, StrPtr) =>
            {
                if (TokenDefinition.Matches(Text, StrPtr, "\""))
                {
                    StrPtr++;
                    string value = "";
                    char last = ' ';
                    // I don't know if escape characters exist in RockStar, but they do in this implementation...
                    while (!(TokenDefinition.Matches(Text, StrPtr, "\"") && !TokenDefinition.Matches(Text, StrPtr - 1, "\\\"")))
                    {
                        char c = Text[StrPtr++];
                        if(last == '\\')
                        {
                            if(c == '"')
                            {
                                value += "\"";
                            }
                            else if(c == 'n')
                            {
                                value += "\n";
                            }
                            else if(c == 't')
                            {
                                value += "\t";
                            }
                            else if(c == '\\')
                            {
                                value += "\\";
                            }
                            else
                            {
                                throw new TokenException("Invalid escape character: \\" + c);
                            }
                            last = c;
                            continue;
                        }
                        last = c;
                        if(c == '\\')
                        {
                            continue;
                        }
                        value += c;
                    }
                    StrPtr++;
                    return new TokenDefinition.TokenResult() { NewStrPtr = StrPtr, T = new Token("STR", value) };
                }
                return null;
            }));
            //object
            //uhh...

            //proper variables
            defs.Add(new MatchingTokenDefinition((Text, StrPtr) =>
            {
                string name = "";
                if (TokenDefinition.IsBetween(Text, StrPtr, 'A', 'Z'))
                {
                    while (TokenDefinition.IsBetween(Text, StrPtr, 'A', 'Z') || TokenDefinition.IsBetween(Text, StrPtr, 'a', 'z') || (TokenDefinition.Matches(Text, StrPtr, " ") && TokenDefinition.IsBetween(Text, ++StrPtr, 'A', 'Z')))
                    {
                        name += Text[StrPtr];
                        StrPtr++;
                    }
                    return new TokenDefinition.TokenResult() { NewStrPtr = StrPtr, T = new Token("PVAR", name) };
                }
                return null;
            }));

            //'s as is
            defs.Add(new PreviousDependantDefinition((Text, StrPtr, Tokens) =>
            {
                if((Tokens.Count > 0 && Tokens.Last().Name == "PVAR") || (Tokens.Count > 1 && Tokens[Tokens.Count - 2].Name == "CVARSP") && TokenDefinition.Matches(Text, StrPtr, "'s"))
                {
                    StrPtr += 2;
                    return new TokenDefinition.TokenResult() { NewStrPtr = StrPtr, T = new Token("IS", "'s") };
                }
                return null;
            }));

            //words
            defs.Add(new MatchingTokenDefinition((Text, StrPtr) =>
            {
                if(TokenDefinition.InSet(Text, StrPtr, LowerCase) || TokenDefinition.Matches(Text, StrPtr, "'"))
                {
                    string text = "";
                    while(TokenDefinition.InSet(Text, StrPtr, LowerCase) || TokenDefinition.Matches(Text, StrPtr, "'"))
                    {
                        //skip over apostrophies
                        if(TokenDefinition.Matches(Text, StrPtr, "'"))
                        {
                            StrPtr++;
                            continue;
                        }
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
                    tokens.Add(new Token("NL", "NL", line));//shhh this is our secret...
                    tokens.Add(new Token("EOF", "EOF", line));
                    break;
                }
                //skip white space
                StrPtr = SkipCommentsAndWhiteSpace(Text, StrPtr);
                //look for new lines and empty lines
                if(TokenDefinition.Matches(Text, StrPtr, "\n"))
                {
                    StrPtr++;
                    int oldStrPtr = StrPtr;
                    line++;
                    StrPtr = SkipCommentsAndWhiteSpace(Text, StrPtr);
                    if(TokenDefinition.Matches(Text, StrPtr, "\n"))
                    {
                        StrPtr++;
                        line++;
                        tokens.Add(new Token("ELINE", "ELINE"));
                        continue;
                    }
                    StrPtr = oldStrPtr;
                    tokens.Add(new Token("NL", "NL"));
                    continue;
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
                    else if(tc is PreviousDependantDefinition)
                    {
                        tr = (tc as PreviousDependantDefinition).Match(Text, StrPtr, tokens);
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
            if (TokenDefinition.Matches(Text, StrPtr, "("))
            {
                while (!TokenDefinition.Matches(Text, StrPtr, ")"))
                {
                    if (TokenDefinition.Matches(Text, StrPtr, "\n"))
                    {
                        line++;
                    }
                    StrPtr++;
                }
                StrPtr++;
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

    class PreviousDependantDefinition : TokenDefinition
    {
        public delegate TokenResult MatchFunc(string Text, int StrPtr, List<Token> Tokens);

        public MatchFunc Match { get; set; }

        public PreviousDependantDefinition(MatchFunc Match)
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

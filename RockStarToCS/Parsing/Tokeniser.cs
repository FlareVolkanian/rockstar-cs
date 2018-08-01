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
        public static string WhiteSpace = " \r\t";
        private int line;

        private static string InvalidKeyWordEndings = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUV";
        private static string LowerCase = "abcdefghijklmnopqrstuvwxyz";
        private static string WordCharacters = LowerCase + "'!?',;:@#$%^&*";
        private static string NumberChars = "0123456789";

        public Tokeniser()
        {
            TokenDefinitionList defs = new TokenDefinitionList();
            
            //most of the tokens are defined here, a couple such as EOF and ELINE (empty line) are defined in the Tokenise method

            //common variable specifiers
            defs.Add("a", "CVARSP", true);
            defs.Add("A", "CVARSP", true);
            defs.Add("an", "CVARSP", true);
            defs.Add("An", "CVARSP", true);
            defs.Add("the", "CVARSP", true);
            defs.Add("The", "CVARSP", true);
            defs.Add("my", "CVARSP", true);
            defs.Add("My", "CVARSP", true);
            defs.Add("your", "CVARSP", true);
            defs.Add("Your", "CVARSP", true);

            //last variable/pronouns
            defs.Add("it", "LVAR", true);
            defs.Add("he", "LVAR", true);
            defs.Add("she", "LVAR", true);
            defs.Add("him", "LVAR", true);
            defs.Add("her", "LVAR", true);
            defs.Add("them", "LVAR", true);
            defs.Add("they", "LVAR", true);
            defs.Add("ze", "LVAR", true);
            defs.Add("hir", "LVAR", true);
            defs.Add("zie", "LVAR", true);
            defs.Add("zir", "LVAR", true);
            defs.Add("xe", "LVAR", true);
            defs.Add("xem", "LVAR", true);
            defs.Add("ve", "LVAR", true);
            defs.Add("ver", "LVAR", true);

            //assignment
            defs.Add("put", "PUT", true);
            defs.Add("Put", "PUT", true);
            defs.Add("into", "INTO", true);

            //poetic literals
            //poetic type literals
            defs.Add("is", "IS", true);
            defs.Add("was", "WAS", true);
            defs.Add("were", "WAS", true);
            //poetic string literals
            defs.Add("says", "SAYS", true);

            //arithmetic
            defs.Add("plus", "ADD", true);
            defs.Add("with", "ADD", true);
            defs.Add("minus", "SUB", true);
            defs.Add("without", "SUB", true);
            defs.Add("times", "MULT", true);
            defs.Add("of", "MULT", true);
            defs.Add("over", "DIV", true);

            //comparison
            defs.Add("not", "NOT", true);
            defs.Add("ain't", "AINT", true);
            defs.Add("aint", "AINT", true);
            defs.Add("than", "THAN", true);
            defs.Add("higher", "GT", true);
            defs.Add("greater", "GT", true);
            defs.Add("bigger", "GT", true);
            defs.Add("stronger", "GT", true);
            defs.Add("lower", "LT", true);
            defs.Add("less", "LT", true);
            defs.Add("smaller", "LT", true);
            defs.Add("weaker", "LT", true);
            defs.Add("as", "AS", true);
            defs.Add("high", "GTEQ", true);
            defs.Add("great", "GTEQ", true);
            defs.Add("big", "GTEQ", true);
            defs.Add("strong", "GTEQ", true);
            defs.Add("low", "LTEQ", true);
            defs.Add("little", "LTEQ", true);
            defs.Add("small", "LTEQ", true);
            defs.Add("weak", "LTEQ", true);

            //increment/decrement
            defs.Add("build", "BLD", true);
            defs.Add("Build", "BLD", true);
            defs.Add("up", "UP", true);
            defs.Add("knock", "KNK", true);
            defs.Add("Knock", "KNK", true);
            defs.Add("down", "DWN", true);

            //I/O
            defs.Add("Say", "SAY", true);
            defs.Add("Shout", "SAY", true);
            defs.Add("Whisper", "SAY", true);
            defs.Add("Listen to", "LSTN", true);
            defs.Add("Listen", "LSTN", true);

            //types
            //undefined
            defs.Add("mysterious", "UNDEF", true);
            //null
            defs.Add("nothing", "NULL", true);
            defs.Add("nowhere", "NULL", true);
            defs.Add("nobody", "NULL", true);
            defs.Add("null", "NULL", true);
            defs.Add("empty", "NULL", true);
            defs.Add("gone", "NULL", true);
            //boolean
            defs.Add("true", "TRUE", true);
            defs.Add("right", "TRUE", true);
            defs.Add("yes", "TRUE", true);
            defs.Add("ok", "TRUE", true);
            defs.Add("false", "FALSE", true);
            defs.Add("wrong", "FALSE", true);
            defs.Add("no", "FALSE", true);
            defs.Add("lies", "FALSE", true);
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
            defs.Add(".", "WORD");
            defs.Add(new MatchingTokenDefinition((Text, StrPtr) =>
            {
                if(TokenDefinition.InSet(Text, StrPtr, WordCharacters))
                {
                    string text = "";
                    while(TokenDefinition.InSet(Text, StrPtr, WordCharacters))
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

        public static bool Matches(string Text, int StrPtr, string TextToMatch, string InvalidEndings)
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

        public static bool Matches(string Text, int StrPtr, string TextToMatch, bool EndsWithWhiteSpace)
        {
            if(StrPtr >= Text.Length)
            {
                return false;
            }
            for(int i = 0;i < TextToMatch.Length && StrPtr + i < Text.Length;++i)
            {
                if(Text[StrPtr + i] != TextToMatch[i])
                {
                    return false;
                }
            }
            //match whitespace or end of Text
            if(EndsWithWhiteSpace && ((Text.Length > StrPtr + TextToMatch.Length && (Tokeniser.WhiteSpace + "\n").Contains(Text[StrPtr + TextToMatch.Length])) || Text.Length == StrPtr + TextToMatch.Length))
            {
                return true;
            }
            else if(!EndsWithWhiteSpace)
            {
                return true;
            }
            return false;
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
        private bool EndsWithWhiteSpace { get; set; }

        public ConstantDefinition(string TextToMatch, string TokenName, bool EndsWithWhiteSpace)
        {
            this.TextToMatch = TextToMatch;
            this.TokenName = TokenName;
            this.EndsWithWhiteSpace = EndsWithWhiteSpace;
        }

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
            if (!string.IsNullOrEmpty(InvalidEndings))
            {
                if(TokenDefinition.Matches(Text, StrPtr, TextToMatch, InvalidEndings))
                {
                    return new TokenResult() { NewStrPtr = StrPtr + TextToMatch.Length, T = new Token(TokenName, TextToMatch) };
                }
            }
            else if (EndsWithWhiteSpace)
            {
                if (TokenDefinition.Matches(Text, StrPtr, TextToMatch, true))
                {
                    return new TokenResult() { NewStrPtr = StrPtr + TextToMatch.Length, T = new Token(TokenName, TextToMatch) };
                }
            }
            else
            {
                if (TokenDefinition.Matches(Text, StrPtr, TextToMatch))
                {
                    return new TokenResult() { NewStrPtr = StrPtr + TextToMatch.Length, T = new Token(TokenName, TextToMatch) };
                }
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

        public void Add(string TextToMatch, string TokenName, bool EndsWithWhiteSpace)
        {
            Add(new ConstantDefinition(TextToMatch, TokenName, EndsWithWhiteSpace));
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RockStarToCS.Parsing
{
    class Parser
    {
        private int TokenIndex;
        private List<Token> Tokens;
        public int HighestLine { get; private set; }

        public ParseNode Parse(List<Token> Tokens)
        {
            this.Tokens = Tokens;
            TokenIndex = 0;
            HighestLine = 0;
            ParseNode retVal = null;
            if((retVal = Matches_root()) != null)
            {
                return retVal;
            }
            return null;
        }

        private ParseNode Matches_root()
        {
            //root => rstarprog
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = Matches_rstarprog()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_rstarprog()
        {
            //rstarprog => EOF
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("EOF")) != null)
            {
                Func<object[], ParseNode> f = x => new ParseNodeList();
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__wrdset_1()
        {
            //_wrdset => WORD _wrdset
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = TokenMatches("WORD")) != null && (matches[1] = Matches__wrdset()) != null)
            {
                Func<object[], ParseNode> f = x => { var lst = x[1] as ParseNodeList; lst.Add(new WordParseNode(x[0] as Token)); return lst; };
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__wrdset_2()
        {
            //_wrdset => WORD
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("WORD")) != null)
            {
                Func<object[], ParseNode> f = x => new ParseNodeList(new WordParseNode(x[0] as Token));
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__wrdset(int RuleIndex=0)
        {
            ParseNode matches = null;
            //_wrdset => WORD _wrdset
            if(RuleIndex != 1 && (matches = Matches__wrdset_1()) != null)
            {
                return matches;
            }
            //_wrdset => WORD
            if(RuleIndex != 2 && (matches = Matches__wrdset_2()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches__eline_1()
        {
            //_eline => ELINE _eline
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = TokenMatches("ELINE")) != null && (matches[1] = Matches__eline()) != null)
            {
                Func<object[], ParseNode> f = x => x[1] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__eline_2()
        {
            //_eline => ELINE
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("ELINE")) != null)
            {
                Func<object[], ParseNode> f = x => new EmptyLineParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__eline(int RuleIndex=0)
        {
            ParseNode matches = null;
            //_eline => ELINE _eline
            if(RuleIndex != 1 && (matches = Matches__eline_1()) != null)
            {
                return matches;
            }
            //_eline => ELINE
            if(RuleIndex != 2 && (matches = Matches__eline_2()) != null)
            {
                return matches;
            }
            return null;
        }

        private Token TokenMatches(string TestString)
        {
            if(TokenIndex < Tokens.Count && Tokens[TokenIndex].Names.Contains(TestString))
            {
                Token t = Tokens[TokenIndex++];
                if(t.LineNumber > HighestLine)
                {
                    HighestLine = t.LineNumber;
                }
                return t;
            }
            return null;
        }

    }

    class Token
    {
        public string[] Names { get; set; }
        public string Value { get; set; }
        public int LineNumber { get; set; }

        public Token(string[] Names, string Value)
        {
            this.Names = Names;
            this.Value = Value;
            LineNumber = 0;
        }

        public Token(string[] Names, string Value, int LineNumber) : this(Names, Value) 
        {
            this.LineNumber = LineNumber;
        }

        public Token(string Name, string Value) : this(new string[]{ Name }, Value) { }

        public Token(string Name, string Value, int LineNumber) : this(new string[]{ Name }, Value, LineNumber) { }

        public Token(string Name, string AltName, string Value) : this(new string[]{ Name, AltName }, Value) { }

        public Token(string Name, string AltName, string Value, int LineNumber) : this(new string[]{ Name, AltName }, Value, LineNumber) { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Names: ");
            foreach(string name in Names)
            {
                sb.Append(name);
                sb.Append(", ");
            }
            string result = sb.ToString();
            if(Names.Length > 0)
            {
                result = result.Substring(0, result.Length - 2);
            }
            return result;
        }
    }
}

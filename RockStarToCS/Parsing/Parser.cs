using System;
using System.Collections.Generic;
using RockStarToCS.Parsing.ParseNodes;

/* Warning: this file is generated, changes made here may be be overwritten. */

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

        private ParseNode Matches_rstarprog_1()
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

        private ParseNode Matches_rstarprog(int RuleIndex=0)
        {
            ParseNode matches = null;
            //rstarprog => EOF
            if(RuleIndex != 1 && (matches = Matches_rstarprog_1()) != null)
            {
                return matches;
            }
            //rstarprog => blk
            if(RuleIndex != 2 && (matches = Matches_blk()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(new object[]{ matches });
            }
            return null;
        }

        private ParseNode Matches_blk_1()
        {
            //blk => stmtlst eline
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_stmtlst()) != null && (matches[1] = Matches_eline()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_blk(int RuleIndex=0)
        {
            ParseNode matches = null;
            //blk => stmtlst eline
            if(RuleIndex != 1 && (matches = Matches_blk_1()) != null)
            {
                return matches;
            }
            //blk => stmtlst
            if(RuleIndex != 2 && (matches = Matches_stmtlst()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(new object[]{ matches });
            }
            return null;
        }

        private ParseNode Matches_stmtlst_1()
        {
            //stmtlst => stmtlst stmtlst
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_stmtlst(1)) != null && (matches[1] = Matches_stmtlst()) != null)
            {
                Func<object[], ParseNode> f = x => { var lst =x[0] as ParseNodeList; lst.Add(x[1] as ParseNode); return lst; };
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_stmtlst(int RuleIndex=0)
        {
            ParseNode matches = null;
            //stmtlst => stmtlst stmtlst
            if(RuleIndex != 1 && (matches = Matches_stmtlst_1()) != null)
            {
                return matches;
            }
            //stmtlst => stmt
            if(RuleIndex != 2 && (matches = Matches_stmt()) != null)
            {
                Func<object[], ParseNode> f = x => new ParseNodeList(x[0] as ParseNode);
                return f(new object[]{ matches });
            }
            return null;
        }

        private ParseNode Matches_stmt_1()
        {
            //stmt => ass NL
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_ass()) != null && (matches[1] = TokenMatches("NL")) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_stmt_2()
        {
            //stmt => io NL
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_io()) != null && (matches[1] = TokenMatches("NL")) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_stmt_3()
        {
            //stmt => incdec NL
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_incdec()) != null && (matches[1] = TokenMatches("NL")) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_stmt_4()
        {
            //stmt => psla NL
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_psla()) != null && (matches[1] = TokenMatches("NL")) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_stmt_5()
        {
            //stmt => arth NL
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_arth()) != null && (matches[1] = TokenMatches("NL")) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_stmt(int RuleIndex=0)
        {
            ParseNode matches = null;
            //stmt => ass NL
            if(RuleIndex != 1 && (matches = Matches_stmt_1()) != null)
            {
                return matches;
            }
            //stmt => io NL
            if(RuleIndex != 2 && (matches = Matches_stmt_2()) != null)
            {
                return matches;
            }
            //stmt => incdec NL
            if(RuleIndex != 3 && (matches = Matches_stmt_3()) != null)
            {
                return matches;
            }
            //stmt => psla NL
            if(RuleIndex != 4 && (matches = Matches_stmt_4()) != null)
            {
                return matches;
            }
            //stmt => arth NL
            if(RuleIndex != 5 && (matches = Matches_stmt_5()) != null)
            {
                return matches;
            }
            //stmt => loop
            if(RuleIndex != 6 && (matches = Matches_loop()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(new object[]{ matches });
            }
            return null;
        }

        private ParseNode Matches_loop_1()
        {
            //loop => WHILE arth NL blk
            int ti = TokenIndex;
            object[] matches = new object[4];
            if((matches[0] = TokenMatches("WHILE")) != null && (matches[1] = Matches_arth()) != null && (matches[2] = TokenMatches("NL")) != null && (matches[3] = Matches_blk()) != null)
            {
                Func<object[], ParseNode> f = x => new WhileParseNode(x[0] as Token, x[1] as ParseNode, x[3] as ParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_loop_2()
        {
            //loop => UNTIL arth NL blk
            int ti = TokenIndex;
            object[] matches = new object[4];
            if((matches[0] = TokenMatches("UNTIL")) != null && (matches[1] = Matches_arth()) != null && (matches[2] = TokenMatches("NL")) != null && (matches[3] = Matches_blk()) != null)
            {
                Func<object[], ParseNode> f = x => new UntilParseNode(x[0] as Token, x[1] as ParseNode, x[3] as ParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_loop(int RuleIndex=0)
        {
            ParseNode matches = null;
            //loop => WHILE arth NL blk
            if(RuleIndex != 1 && (matches = Matches_loop_1()) != null)
            {
                return matches;
            }
            //loop => UNTIL arth NL blk
            if(RuleIndex != 2 && (matches = Matches_loop_2()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_io_1()
        {
            //io => SAY arth
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = TokenMatches("SAY")) != null && (matches[1] = Matches_arth()) != null)
            {
                Func<object[], ParseNode> f = x => new OutputParseNode(x[0] as Token, x[1] as ParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_io_2()
        {
            //io => LSTN var
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = TokenMatches("LSTN")) != null && (matches[1] = Matches_var()) != null)
            {
                Func<object[], ParseNode> f = x => new InputParseNode(x[0] as Token, x[1] as VariableParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_io(int RuleIndex=0)
        {
            ParseNode matches = null;
            //io => SAY arth
            if(RuleIndex != 1 && (matches = Matches_io_1()) != null)
            {
                return matches;
            }
            //io => LSTN var
            if(RuleIndex != 2 && (matches = Matches_io_2()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_ass_1()
        {
            //ass => var is NULL
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_var()) != null && (matches[1] = Matches_is()) != null && (matches[2] = TokenMatches("NULL")) != null)
            {
                Func<object[], ParseNode> f = x => new AssignmentParseNode((x[1] as TokenParseNode).T, new NullParseNode(x[2] as Token), x[0] as VariableParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_ass_2()
        {
            //ass => var is bool
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_var()) != null && (matches[1] = Matches_is()) != null && (matches[2] = Matches_bool()) != null)
            {
                Func<object[], ParseNode> f = x => new AssignmentParseNode((x[1] as TokenParseNode).T, x[2] as ParseNode, x[0] as VariableParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_ass_3()
        {
            //ass => var is wrdlst
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_var()) != null && (matches[1] = Matches_is()) != null && (matches[2] = Matches_wrdlst()) != null)
            {
                Func<object[], ParseNode> f = x => new AssignmentParseNode((x[1] as TokenParseNode).T, x[2] as ParseNode, x[0] as VariableParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_ass_4()
        {
            //ass => var is STR
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_var()) != null && (matches[1] = Matches_is()) != null && (matches[2] = TokenMatches("STR")) != null)
            {
                Func<object[], ParseNode> f = x => new AssignmentParseNode((x[1] as TokenParseNode).T, x[2] as StringParseNode, x[0] as VariableParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_ass_5()
        {
            //ass => PUT arth INTO var
            int ti = TokenIndex;
            object[] matches = new object[4];
            if((matches[0] = TokenMatches("PUT")) != null && (matches[1] = Matches_arth()) != null && (matches[2] = TokenMatches("INTO")) != null && (matches[3] = Matches_var()) != null)
            {
                Func<object[], ParseNode> f = x => new PutIntoParseNode(x[0] as Token, x[3] as VariableParseNode, x[1] as ParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_ass(int RuleIndex=0)
        {
            ParseNode matches = null;
            //ass => var is NULL
            if(RuleIndex != 1 && (matches = Matches_ass_1()) != null)
            {
                return matches;
            }
            //ass => var is bool
            if(RuleIndex != 2 && (matches = Matches_ass_2()) != null)
            {
                return matches;
            }
            //ass => var is wrdlst
            if(RuleIndex != 3 && (matches = Matches_ass_3()) != null)
            {
                return matches;
            }
            //ass => var is STR
            if(RuleIndex != 4 && (matches = Matches_ass_4()) != null)
            {
                return matches;
            }
            //ass => PUT arth INTO var
            if(RuleIndex != 5 && (matches = Matches_ass_5()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_is_1()
        {
            //is => IS
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("IS")) != null)
            {
                Func<object[], ParseNode> f = x => new TokenParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_is_2()
        {
            //is => WAS
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("WAS")) != null)
            {
                Func<object[], ParseNode> f = x => new TokenParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_is(int RuleIndex=0)
        {
            ParseNode matches = null;
            //is => IS
            if(RuleIndex != 1 && (matches = Matches_is_1()) != null)
            {
                return matches;
            }
            //is => WAS
            if(RuleIndex != 2 && (matches = Matches_is_2()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_var_1()
        {
            //var => CVARSP WORD
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = TokenMatches("CVARSP")) != null && (matches[1] = TokenMatches("WORD")) != null)
            {
                Func<object[], ParseNode> f = x => new VariableParseNode(x[0] as Token, (x[0] as Token).Value.ToLower() + " " + (x[1] as Token).Value, false);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_var_2()
        {
            //var => PVAR
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("PVAR")) != null)
            {
                Func<object[], ParseNode> f = x => new VariableParseNode(x[0] as Token, (x[0] as Token).Value, true);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_var_3()
        {
            //var => LVAR
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("LVAR")) != null)
            {
                Func<object[], ParseNode> f = x => new VariableParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_var(int RuleIndex=0)
        {
            ParseNode matches = null;
            //var => CVARSP WORD
            if(RuleIndex != 1 && (matches = Matches_var_1()) != null)
            {
                return matches;
            }
            //var => PVAR
            if(RuleIndex != 2 && (matches = Matches_var_2()) != null)
            {
                return matches;
            }
            //var => LVAR
            if(RuleIndex != 3 && (matches = Matches_var_3()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_psla()
        {
            //psla => var SAYS wrdlst
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_var()) != null && (matches[1] = TokenMatches("SAYS")) != null && (matches[2] = Matches_wrdlst()) != null)
            {
                Func<object[], ParseNode> f = x => new StringLiteralAssignmentParseNode(x[1] as Token, x[0] as VariableParseNode, x[2] as ParseNodeList);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_bool_1()
        {
            //bool => TRUE
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("TRUE")) != null)
            {
                Func<object[], ParseNode> f = x => new BooleanParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_bool_2()
        {
            //bool => FALSE
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("FALSE")) != null)
            {
                Func<object[], ParseNode> f = x => new BooleanParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_bool(int RuleIndex=0)
        {
            ParseNode matches = null;
            //bool => TRUE
            if(RuleIndex != 1 && (matches = Matches_bool_1()) != null)
            {
                return matches;
            }
            //bool => FALSE
            if(RuleIndex != 2 && (matches = Matches_bool_2()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_incdec_1()
        {
            //incdec => BLD var UP
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = TokenMatches("BLD")) != null && (matches[1] = Matches_var()) != null && (matches[2] = TokenMatches("UP")) != null)
            {
                Func<object[], ParseNode> f = x => new IncDecParseNode(x[0] as Token, x[1] as VariableParseNode, true);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_incdec_2()
        {
            //incdec => KNK var DWN
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = TokenMatches("KNK")) != null && (matches[1] = Matches_var()) != null && (matches[2] = TokenMatches("DWN")) != null)
            {
                Func<object[], ParseNode> f = x => new IncDecParseNode(x[0] as Token, x[1] as VariableParseNode, false);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_incdec(int RuleIndex=0)
        {
            ParseNode matches = null;
            //incdec => BLD var UP
            if(RuleIndex != 1 && (matches = Matches_incdec_1()) != null)
            {
                return matches;
            }
            //incdec => KNK var DWN
            if(RuleIndex != 2 && (matches = Matches_incdec_2()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_arth()
        {
            //arth => comp
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = Matches_comp()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_comp_1()
        {
            //comp => mult IS NOT comp
            int ti = TokenIndex;
            object[] matches = new object[4];
            if((matches[0] = Matches_mult()) != null && (matches[1] = TokenMatches("IS")) != null && (matches[2] = TokenMatches("NOT")) != null && (matches[3] = Matches_comp()) != null)
            {
                Func<object[], ParseNode> f = x => new InvertComparisonParseNode(x[1] as Token, new EqParseNode(x[1] as Token, x[0] as ParseNode, x[3] as ParseNode));
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_comp_2()
        {
            //comp => mult AINT comp
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_mult()) != null && (matches[1] = TokenMatches("AINT")) != null && (matches[2] = Matches_comp()) != null)
            {
                Func<object[], ParseNode> f = x => new InvertComparisonParseNode(x[1] as Token, new EqParseNode(x[1] as Token, x[0] as ParseNode, x[2] as ParseNode));
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_comp_3()
        {
            //comp => mult IS comp
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_mult()) != null && (matches[1] = TokenMatches("IS")) != null && (matches[2] = Matches_comp()) != null)
            {
                Func<object[], ParseNode> f = x => new EqParseNode(x[1] as Token, x[0] as ParseNode, x[2] as ParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_comp_4()
        {
            //comp => mult IS GT THAN comp
            int ti = TokenIndex;
            object[] matches = new object[5];
            if((matches[0] = Matches_mult()) != null && (matches[1] = TokenMatches("IS")) != null && (matches[2] = TokenMatches("GT")) != null && (matches[3] = TokenMatches("THAN")) != null && (matches[4] = Matches_comp()) != null)
            {
                Func<object[], ParseNode> f = x => new GtParseNode(x[1] as Token, x[0] as ParseNode, x[4] as ParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_comp_5()
        {
            //comp => mult IS AS LTEQ AS comp
            int ti = TokenIndex;
            object[] matches = new object[6];
            if((matches[0] = Matches_mult()) != null && (matches[1] = TokenMatches("IS")) != null && (matches[2] = TokenMatches("AS")) != null && (matches[3] = TokenMatches("LTEQ")) != null && (matches[4] = TokenMatches("AS")) != null && (matches[5] = Matches_comp()) != null)
            {
                Func<object[], ParseNode> f = x => new InvertComparisonParseNode(x[0] as Token, new GtParseNode(x[1] as Token, x[0] as ParseNode, x[5] as ParseNode));
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_comp_6()
        {
            //comp => mult IS LT THAN comp
            int ti = TokenIndex;
            object[] matches = new object[5];
            if((matches[0] = Matches_mult()) != null && (matches[1] = TokenMatches("IS")) != null && (matches[2] = TokenMatches("LT")) != null && (matches[3] = TokenMatches("THAN")) != null && (matches[4] = Matches_comp()) != null)
            {
                Func<object[], ParseNode> f = x => new LtParseNode(x[1] as Token, x[0] as ParseNode, x[4] as ParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_comp_7()
        {
            //comp => mult IS AS GTEQ AS comp
            int ti = TokenIndex;
            object[] matches = new object[6];
            if((matches[0] = Matches_mult()) != null && (matches[1] = TokenMatches("IS")) != null && (matches[2] = TokenMatches("AS")) != null && (matches[3] = TokenMatches("GTEQ")) != null && (matches[4] = TokenMatches("AS")) != null && (matches[5] = Matches_comp()) != null)
            {
                Func<object[], ParseNode> f = x => new InvertComparisonParseNode(x[0] as Token, new LtParseNode(x[1] as Token, x[0] as ParseNode, x[5] as ParseNode));
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_comp(int RuleIndex=0)
        {
            ParseNode matches = null;
            //comp => mult IS NOT comp
            if(RuleIndex != 1 && (matches = Matches_comp_1()) != null)
            {
                return matches;
            }
            //comp => mult AINT comp
            if(RuleIndex != 2 && (matches = Matches_comp_2()) != null)
            {
                return matches;
            }
            //comp => mult IS comp
            if(RuleIndex != 3 && (matches = Matches_comp_3()) != null)
            {
                return matches;
            }
            //comp => mult IS GT THAN comp
            if(RuleIndex != 4 && (matches = Matches_comp_4()) != null)
            {
                return matches;
            }
            //comp => mult IS AS LTEQ AS comp
            if(RuleIndex != 5 && (matches = Matches_comp_5()) != null)
            {
                return matches;
            }
            //comp => mult IS LT THAN comp
            if(RuleIndex != 6 && (matches = Matches_comp_6()) != null)
            {
                return matches;
            }
            //comp => mult IS AS GTEQ AS comp
            if(RuleIndex != 7 && (matches = Matches_comp_7()) != null)
            {
                return matches;
            }
            //comp => mult
            if(RuleIndex != 8 && (matches = Matches_mult()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(new object[]{ matches });
            }
            return null;
        }

        private ParseNode Matches_mult_1()
        {
            //mult => add MULT mult
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_add()) != null && (matches[1] = TokenMatches("MULT")) != null && (matches[2] = Matches_mult()) != null)
            {
                Func<object[], ParseNode> f = x => new MultiplyParseNode(x[1] as Token, x[0] as ParseNode, x[2] as ParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_mult_2()
        {
            //mult => add DIV mult
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_add()) != null && (matches[1] = TokenMatches("DIV")) != null && (matches[2] = Matches_mult()) != null)
            {
                Func<object[], ParseNode> f = x => new DivisionParseNode(x[1] as Token, x[0] as ParseNode, x[2] as ParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_mult(int RuleIndex=0)
        {
            ParseNode matches = null;
            //mult => add MULT mult
            if(RuleIndex != 1 && (matches = Matches_mult_1()) != null)
            {
                return matches;
            }
            //mult => add DIV mult
            if(RuleIndex != 2 && (matches = Matches_mult_2()) != null)
            {
                return matches;
            }
            //mult => add
            if(RuleIndex != 3 && (matches = Matches_add()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(new object[]{ matches });
            }
            return null;
        }

        private ParseNode Matches_add_1()
        {
            //add => atom ADD add
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_atom()) != null && (matches[1] = TokenMatches("ADD")) != null && (matches[2] = Matches_add()) != null)
            {
                Func<object[], ParseNode> f = x => new AdditionParseNode(x[1] as Token, x[0] as ParseNode, x[2] as ParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_add_2()
        {
            //add => atom SUB add
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_atom()) != null && (matches[1] = TokenMatches("SUB")) != null && (matches[2] = Matches_add()) != null)
            {
                Func<object[], ParseNode> f = x => new SubtractParseNode(x[1] as Token, x[0] as ParseNode, x[2] as ParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_add(int RuleIndex=0)
        {
            ParseNode matches = null;
            //add => atom ADD add
            if(RuleIndex != 1 && (matches = Matches_add_1()) != null)
            {
                return matches;
            }
            //add => atom SUB add
            if(RuleIndex != 2 && (matches = Matches_add_2()) != null)
            {
                return matches;
            }
            //add => atom
            if(RuleIndex != 3 && (matches = Matches_atom()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(new object[]{ matches });
            }
            return null;
        }

        private ParseNode Matches_atom_4()
        {
            //atom => NULL
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("NULL")) != null)
            {
                Func<object[], ParseNode> f = x => new NullParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_atom_5()
        {
            //atom => NUM
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("NUM")) != null)
            {
                Func<object[], ParseNode> f = x => new NumberParseNode(x[0] as Token, (x[0] as Token).Value);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_atom(int RuleIndex=0)
        {
            ParseNode matches = null;
            //atom => bool
            if(RuleIndex != 1 && (matches = Matches_bool()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(new object[]{ matches });
            }
            //atom => var
            if(RuleIndex != 2 && (matches = Matches_var()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(new object[]{ matches });
            }
            //atom => str
            if(RuleIndex != 3 && (matches = Matches_str()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(new object[]{ matches });
            }
            //atom => NULL
            if(RuleIndex != 4 && (matches = Matches_atom_4()) != null)
            {
                return matches;
            }
            //atom => NUM
            if(RuleIndex != 5 && (matches = Matches_atom_5()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_wrdlst_1()
        {
            //wrdlst => wrdlst wrdlst
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_wrdlst(1)) != null && (matches[1] = Matches_wrdlst()) != null)
            {
                Func<object[], ParseNode> f = x => { var lst = x[0] as ParseNodeList; lst.Add(x[1] as ParseNode); return lst; };
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrdlst(int RuleIndex=0)
        {
            ParseNode matches = null;
            //wrdlst => wrdlst wrdlst
            if(RuleIndex != 1 && (matches = Matches_wrdlst_1()) != null)
            {
                return matches;
            }
            //wrdlst => wrd
            if(RuleIndex != 2 && (matches = Matches_wrd()) != null)
            {
                Func<object[], ParseNode> f = x => new ParseNodeList(x[0] as ParseNode);
                return f(new object[]{ matches });
            }
            return null;
        }

        private ParseNode Matches_wrd_1()
        {
            //wrd => WORD
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("WORD")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_2()
        {
            //wrd => CVARSP
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("CVARSP")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_3()
        {
            //wrd => PVAR
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("PVAR")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_4()
        {
            //wrd => LVAR
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("LVAR")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_5()
        {
            //wrd => PUT
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("PUT")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_6()
        {
            //wrd => INTO
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("INTO")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_7()
        {
            //wrd => IS
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("IS")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_8()
        {
            //wrd => SAYS
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("SAYS")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_9()
        {
            //wrd => ADD
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("ADD")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_10()
        {
            //wrd => SUB
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("SUB")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_11()
        {
            //wrd => MULT
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("MULT")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_12()
        {
            //wrd => DIV
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("DIV")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_13()
        {
            //wrd => BLD
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("BLD")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_14()
        {
            //wrd => KNK
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("KNK")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_15()
        {
            //wrd => UP
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("UP")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_16()
        {
            //wrd => DOWN
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("DOWN")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_17()
        {
            //wrd => SAY
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("SAY")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_18()
        {
            //wrd => LSTN
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("LSTN")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_19()
        {
            //wrd => UNDEF
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("UNDEF")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_20()
        {
            //wrd => NULL
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("NULL")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_21()
        {
            //wrd => TRUE
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("TRUE")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_22()
        {
            //wrd => FALSE
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("FALSE")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_23()
        {
            //wrd => NOT
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("NOT")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_24()
        {
            //wrd => AINT
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("AINT")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_25()
        {
            //wrd => THAN
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("THAN")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_26()
        {
            //wrd => GT
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("GT")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_27()
        {
            //wrd => LT
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("LT")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_28()
        {
            //wrd => AS
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("AS")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_29()
        {
            //wrd => GTEQ
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("GTEQ")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_30()
        {
            //wrd => LTEQ
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("LTEQ")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd(int RuleIndex=0)
        {
            ParseNode matches = null;
            //wrd => WORD
            if(RuleIndex != 1 && (matches = Matches_wrd_1()) != null)
            {
                return matches;
            }
            //wrd => CVARSP
            if(RuleIndex != 2 && (matches = Matches_wrd_2()) != null)
            {
                return matches;
            }
            //wrd => PVAR
            if(RuleIndex != 3 && (matches = Matches_wrd_3()) != null)
            {
                return matches;
            }
            //wrd => LVAR
            if(RuleIndex != 4 && (matches = Matches_wrd_4()) != null)
            {
                return matches;
            }
            //wrd => PUT
            if(RuleIndex != 5 && (matches = Matches_wrd_5()) != null)
            {
                return matches;
            }
            //wrd => INTO
            if(RuleIndex != 6 && (matches = Matches_wrd_6()) != null)
            {
                return matches;
            }
            //wrd => IS
            if(RuleIndex != 7 && (matches = Matches_wrd_7()) != null)
            {
                return matches;
            }
            //wrd => SAYS
            if(RuleIndex != 8 && (matches = Matches_wrd_8()) != null)
            {
                return matches;
            }
            //wrd => ADD
            if(RuleIndex != 9 && (matches = Matches_wrd_9()) != null)
            {
                return matches;
            }
            //wrd => SUB
            if(RuleIndex != 10 && (matches = Matches_wrd_10()) != null)
            {
                return matches;
            }
            //wrd => MULT
            if(RuleIndex != 11 && (matches = Matches_wrd_11()) != null)
            {
                return matches;
            }
            //wrd => DIV
            if(RuleIndex != 12 && (matches = Matches_wrd_12()) != null)
            {
                return matches;
            }
            //wrd => BLD
            if(RuleIndex != 13 && (matches = Matches_wrd_13()) != null)
            {
                return matches;
            }
            //wrd => KNK
            if(RuleIndex != 14 && (matches = Matches_wrd_14()) != null)
            {
                return matches;
            }
            //wrd => UP
            if(RuleIndex != 15 && (matches = Matches_wrd_15()) != null)
            {
                return matches;
            }
            //wrd => DOWN
            if(RuleIndex != 16 && (matches = Matches_wrd_16()) != null)
            {
                return matches;
            }
            //wrd => SAY
            if(RuleIndex != 17 && (matches = Matches_wrd_17()) != null)
            {
                return matches;
            }
            //wrd => LSTN
            if(RuleIndex != 18 && (matches = Matches_wrd_18()) != null)
            {
                return matches;
            }
            //wrd => UNDEF
            if(RuleIndex != 19 && (matches = Matches_wrd_19()) != null)
            {
                return matches;
            }
            //wrd => NULL
            if(RuleIndex != 20 && (matches = Matches_wrd_20()) != null)
            {
                return matches;
            }
            //wrd => TRUE
            if(RuleIndex != 21 && (matches = Matches_wrd_21()) != null)
            {
                return matches;
            }
            //wrd => FALSE
            if(RuleIndex != 22 && (matches = Matches_wrd_22()) != null)
            {
                return matches;
            }
            //wrd => NOT
            if(RuleIndex != 23 && (matches = Matches_wrd_23()) != null)
            {
                return matches;
            }
            //wrd => AINT
            if(RuleIndex != 24 && (matches = Matches_wrd_24()) != null)
            {
                return matches;
            }
            //wrd => THAN
            if(RuleIndex != 25 && (matches = Matches_wrd_25()) != null)
            {
                return matches;
            }
            //wrd => GT
            if(RuleIndex != 26 && (matches = Matches_wrd_26()) != null)
            {
                return matches;
            }
            //wrd => LT
            if(RuleIndex != 27 && (matches = Matches_wrd_27()) != null)
            {
                return matches;
            }
            //wrd => AS
            if(RuleIndex != 28 && (matches = Matches_wrd_28()) != null)
            {
                return matches;
            }
            //wrd => GTEQ
            if(RuleIndex != 29 && (matches = Matches_wrd_29()) != null)
            {
                return matches;
            }
            //wrd => LTEQ
            if(RuleIndex != 30 && (matches = Matches_wrd_30()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_eline_1()
        {
            //eline => eline eline
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_eline(1)) != null && (matches[1] = Matches_eline()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_eline_2()
        {
            //eline => ELINE
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

        private ParseNode Matches_eline_3()
        {
            //eline => EOF
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("EOF")) != null)
            {
                Func<object[], ParseNode> f = x => new EmptyLineParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_eline_4()
        {
            //eline => NL
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("NL")) != null)
            {
                Func<object[], ParseNode> f = x => new EmptyLineParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_eline(int RuleIndex=0)
        {
            ParseNode matches = null;
            //eline => eline eline
            if(RuleIndex != 1 && (matches = Matches_eline_1()) != null)
            {
                return matches;
            }
            //eline => ELINE
            if(RuleIndex != 2 && (matches = Matches_eline_2()) != null)
            {
                return matches;
            }
            //eline => EOF
            if(RuleIndex != 3 && (matches = Matches_eline_3()) != null)
            {
                return matches;
            }
            //eline => NL
            if(RuleIndex != 4 && (matches = Matches_eline_4()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_str()
        {
            //str => STR
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("STR")) != null)
            {
                Func<object[], ParseNode> f = x => new StringParseNode(x[0] as Token, (x[0] as Token).Value);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private Token TokenMatches(string TestString)
        {
            if(TokenIndex < Tokens.Count && Tokens[TokenIndex].Name == TestString)
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
        public string Name { get; set; }
        public string Value { get; set; }
        public int LineNumber { get; set; }

        public Token(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
            LineNumber = 0;
        }

        public Token(string Name, string Value, int LineNumber) : this(Name, Value) 
        {
            this.LineNumber = LineNumber;
        }

        public override string ToString()
        {
            return Name + ": " + Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Parsing
{
    class BooleanParseNode : ParseNode
    {
        public bool Value { get; set; }
        public BooleanParseNode(Token T) : base(T)
        {
            Value = T.Name == "TRUE";
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }
    }
}

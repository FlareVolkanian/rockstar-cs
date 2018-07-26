using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Parsing
{
    class VariableParseNode : ParseNode
    {
        public bool IsProper { get; set; }
        public bool IsCommon => !IsProper;
        public string Name { get; set; }
        public bool IsLast { get; set; }
        public VariableParseNode(Token T, string Name, bool IsProper) : base(T)
        {
            this.Name = Name;
            this.IsProper = IsProper;
            IsLast = false;
        }

        public VariableParseNode(Token T) : base(T)
        {
            IsLast = true;
            Name = string.Empty;
            IsProper = false;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }
    }
}

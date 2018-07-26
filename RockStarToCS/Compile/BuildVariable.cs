using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Compile
{
    enum BuildVariableType
    {
        Null,
        Undefined,
        Boolean,
        Numeric,
        String,
        Object
    }

    class BuildVariable
    {
        private string _CodeName;
        public BuildVariableType Type { get; set; }
        public string Name { get; set; }
        public string CodeName => _CodeName + CodeCount;
        public int CodeCount { get; set; }

        public BuildVariable(BuildVariableType Type, string Name)
        {
            this.Type = Type;
            this.Name = Name;
            _CodeName = "";
            CodeCount = 0;
            foreach(char c in Name)
            {
                if(!((c >= 'a' && c <= 'z') ||(c >= 'A' && c <= 'Z')))
                {
                    _CodeName += "_";
                }
                else
                {
                    _CodeName += c;
                }
            }
        }

        public string CSType
        {
            get
            {
                switch (Type)
                {
                    case BuildVariableType.Boolean:
                        return "bool?";
                    case BuildVariableType.Null:
                        return "object?";
                    case BuildVariableType.Numeric:
                        return "decimal?";
                    case BuildVariableType.Object:
                        return "object";
                    case BuildVariableType.String:
                        return "string";
                    case BuildVariableType.Undefined:
                        return "object";
                }
                return "object";
            }
        }
    }
}

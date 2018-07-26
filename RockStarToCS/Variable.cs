using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS
{
    enum VariableType
    {
        Null,
        Undefined,
        Boolean,
        Numeric,
        String,
        Object
    }

    class Variable
    {
        private string _CodeName;
        public VariableType Type { get; set; }
        public string Name { get; set; }
        public string CodeName => _CodeName + CodeCount;
        public int CodeCount { get; set; }

        public Variable(VariableType Type, string Name)
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
                    case VariableType.Boolean:
                        return "bool?";
                    case VariableType.Null:
                        return "object?";
                    case VariableType.Numeric:
                        return "decimal?";
                    case VariableType.Object:
                        return "object";
                    case VariableType.String:
                        return "string";
                    case VariableType.Undefined:
                        return "object";
                }
                return "object";
            }
        }
    }
}

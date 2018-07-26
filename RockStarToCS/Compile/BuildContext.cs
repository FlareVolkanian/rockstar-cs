using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS
{
    class BuildContext
    {
        public BuildContext Parent { get; private set; }

        private Dictionary<string, BuildVariable> Variables;

        public BuildContext (BuildContext Parent)
        {
            this.Parent = Parent;
            this.Variables = new Dictionary<string, BuildVariable>();
        }

        public void AddVariable(BuildVariable Var)
        {
            Variables.Add(Var.Name, Var);
        }

        public bool VariableExists(string Name)
        {
            if(Variables.ContainsKey(Name))
            {
                return true;
            }
            if(Parent != null)
            {
                return Parent.VariableExists(Name);
            }
            return false;
        }
        
        public bool VariableIsUndefined(string Name)
        {
            if(Variables.ContainsKey(Name))
            {
                if (Variables[Name].Type == VariableType.Undefined)
                {
                    return true;
                }
                return false;
            }
            if(Parent != null && Parent.VariableExists(Name))
            {
                return Parent.VariableIsUndefined(Name);
            }
            return true;
        }

        public BuildVariable GetVariable(string Name)
        {
            if(Variables.ContainsKey(Name))
            {
                return Variables[Name];
            }
            if(Parent != null)
            {
                return Parent.GetVariable(Name);
            }
            return null;
        }
    }
}

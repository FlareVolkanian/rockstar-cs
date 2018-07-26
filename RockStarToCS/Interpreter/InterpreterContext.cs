using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Interpreter
{
    class InterpreterContext
    {
        private Dictionary<string, InterpreterVariable> Variables;
        public InterpreterVariable LastVariable { get; private set; }

        public InterpreterContext Parent { get; private set; }

        public InterpreterContext(InterpreterContext Parent)
        {
            this.Parent = Parent;
            Variables = new Dictionary<string, InterpreterVariable>();
        }

        public void AddVariable(InterpreterVariable Variable)
        {
            Variables.Add(Variable.Name, Variable);
            LastVariable = Variable;
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

        public InterpreterVariable GetVariable(string Name)
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

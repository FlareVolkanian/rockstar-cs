using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Interpreter
{
    class InterpreterEnvironment
    {
        private InterpreterContext _CurrentContext;

        public InterpreterContext CurrentContext => _CurrentContext;

        public InterpreterEnvironment()
        {
            _CurrentContext = new InterpreterContext(null);
        }

        public void PushContext()
        {
            _CurrentContext = new InterpreterContext(_CurrentContext);
        }

        public void PopContext()
        {
            if(_CurrentContext.Parent != null)
            {
                _CurrentContext = _CurrentContext.Parent;
            }
        }
    }
}

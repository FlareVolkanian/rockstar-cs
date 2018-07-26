using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockStarToCS.Compile
{
    class BuildEnvironment
    {
        private BuildContext _CurrentContext;

        public BuildContext CurrentContext
        {
            get
            {
                return _CurrentContext;
            }
        }

        public BuildEnvironment()
        {
            _CurrentContext = new BuildContext(null);
        }

        public void PushBuildContext()
        {
            BuildContext newContext = new BuildContext(_CurrentContext);
            _CurrentContext = newContext;
        }

        public void PopBuildContext()
        {
            if(_CurrentContext.Parent != null)
            {
                _CurrentContext = _CurrentContext.Parent;
            }
        }
    }
}

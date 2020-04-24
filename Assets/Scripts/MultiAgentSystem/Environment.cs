using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MultiAgentSystem
{

    public class Environment
    {

        #region SingletonManagement
        private Environment instance = null;
        private Environment() { }
        public Environment getInstance()
        {
            if (instance == null)
                instance = new Environment();

            return instance;
        }
        #endregion

        private Brain brain;

    }


}

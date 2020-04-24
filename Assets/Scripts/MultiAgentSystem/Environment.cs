using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{

    public class Environment
    {

        #region SingletonManagement
        private static Environment instance = null;
        private Environment() {
            brain = new Brain();
        }
        public static Environment GetInstance()
        {
            if (instance == null)
                instance = new Environment();

            return instance;
        }
        #endregion

        private Brain brain;

        public Brain Brain { get => brain; }

    }


}

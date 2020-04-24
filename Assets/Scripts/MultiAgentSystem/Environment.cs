using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{

    public class Environment
    {

        #region SingletonManagement
        private static Environment _instance = null;
        private Environment() {
            _brain = new Brain();
        }
        public static Environment GetInstance()
        {
            if (_instance == null)
                _instance = new Environment();

            return _instance;
        }
        #endregion

        private Brain _brain;

        public Brain Brain { get => _brain; }

    }


}

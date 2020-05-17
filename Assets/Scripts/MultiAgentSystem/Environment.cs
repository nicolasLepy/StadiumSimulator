using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// Environment of the simulation
    /// Contains pysical environment and multi-agent system
    /// </summary>
    public class Environment
    {

        #region SingletonManagement
        private static Environment _instance = null;
        private Environment() {
            _environment = new Stadium(12);
            _brain = new Brain();
        }
        public static Environment GetInstance()
        {
            if (_instance == null)
                _instance = new Environment();

            return _instance;
        }
        #endregion

        /// <summary>
        /// Multi-agent system
        /// </summary>
        private Brain _brain;

        /// <summary>
        /// Temporary : only to test multi-agents class
        /// </summary>
        [ObsoleteAttribute("This property is temporary, only for test. Will be replaced by the real environment.", false)] 
        private Stadium _environment;

        public Brain Brain { get => _brain; }

        /// <summary>
        /// Get number of door of the stadium
        /// </summary>
        public int CategoriesNumber => _environment.CategoriesNumber;
        
        public Stadium environment => _environment;

    }


}

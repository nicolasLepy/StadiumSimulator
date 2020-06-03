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
            _environment = new Stadium(8);
        }
        public static Environment GetInstance()
        {
            if (_instance == null)
                _instance = new Environment();

            return _instance;
        }
        #endregion

        private Settings _settings;
        public Settings settings => _settings;
        
        /// <summary>
        /// Multi-agent system
        /// </summary>
        private Brain _brain;

        public void CreateBrain(List<int> ticketsOffices, Settings settings)
        {
            _brain = new Brain(ticketsOffices);
            _settings = settings;

        }

        /// <summary>
        /// Environment
        /// </summary>
        private Stadium _environment;

        public Brain Brain { get => _brain; }
        
        public Stadium environment => _environment;

    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiAgentSystem
{
    /// <summary>
    /// Test environment class for multiagents classes
    /// </summary>
    [Obsolete("Only for test. Will be replaced by the real environment.",false)]
    public class Environment_Test
    {

        private int _availableSeats;

        public Environment_Test()
        {

        }

        /// <summary>
        /// Exemple function to be used to test agents behaviours
        /// </summary>
        /// <returns></returns>
        public bool RequestSeat()
        {
            bool res = false;
            if(_availableSeats > 0)
            {
                _availableSeats--;
                res = true;
            }
            return res;
        }

    }
}

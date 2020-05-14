using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{

    public class Ticket
    {
        private int _door;
        public int door => _door;
        public Ticket(int door)
        {
            _door = door;
        }
    }
    
    public class Stadium
    {
        
        
        private Dictionary<int, int> _seatsByDoor;
        
        public Stadium()
        {
            _seatsByDoor = new Dictionary<int, int>();
            for (int i = 0; i < 12; i++)
            {
                _seatsByDoor.Add(i, 10);
            }
        }

        /// <summary>
        /// Get a ticket
        /// </summary>
        /// <returns></returns>
        public Ticket RequestSeat(int door)
        {
            Ticket res = null;
            if (_seatsByDoor[door] > 0)
            {
                _seatsByDoor[door]--;
                res = new Ticket(door);
            }
            return res;
        }
        
        
    }

}

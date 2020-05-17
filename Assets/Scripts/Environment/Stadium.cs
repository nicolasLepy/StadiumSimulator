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

    public enum Team
    {
        HOME,
        AWAY
    }
    
    public class Stadium
    {

        private Dictionary<int, Team> _awayZone;
        private Dictionary<int, int> _seatsByDoor;
        private int _categoriesNumber;
        public int CategoriesNumber => _categoriesNumber;

        public Stadium(int zonesNumber)
        {
            _categoriesNumber = zonesNumber;
            _seatsByDoor = new Dictionary<int, int>();
            _awayZone = new Dictionary<int, Team>();
            for (int i = 1; i <= _categoriesNumber; i++)
            {
                _seatsByDoor.Add(i, 10);
                _awayZone.Add(i, Team.HOME);
            }

            _awayZone[1] = Team.AWAY;
            _awayZone[2] = Team.AWAY;
        }

        public int AvailableSeats(int category)
        {
            int res = 0;
            if(_seatsByDoor.ContainsKey(category))
                res = _seatsByDoor[category];
            return res;
        }
        
        public List<int> StillAvailableCategories()
        {
            List<int> res= new List<int>();
            foreach (KeyValuePair<int, int> kvp in _seatsByDoor)
            {
                if (kvp.Value > 0) res.Add(kvp.Key);
            }
            return res;
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

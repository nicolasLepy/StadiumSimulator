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

        private Dictionary<int, Team> _sideByCategories;
        private Dictionary<int, int> _seatsByDoor;
        private int _categoriesNumber;
        public int CategoriesNumber => _categoriesNumber;

        public Stadium(int zonesNumber)
        {
            _categoriesNumber = zonesNumber;
            _seatsByDoor = new Dictionary<int, int>();
            _sideByCategories = new Dictionary<int, Team>();
            for (int i = 1; i <= _categoriesNumber; i++)
            {
                _seatsByDoor.Add(i, 10);
                _sideByCategories.Add(i, Team.HOME);
            }

            _sideByCategories[1] = Team.AWAY;
            _sideByCategories[12] = Team.AWAY;
        }

        public int AvailableSeats(int category)
        {
            int res = 0;
            if(_seatsByDoor.ContainsKey(category))
                res = _seatsByDoor[category];
            return res;
        }

        public List<int> CategoriesAvailableForSide(Team side)
        {
            List<int> res = new List<int>();
            foreach (KeyValuePair<int, Team> kvp in _sideByCategories)
            {
                if (kvp.Value == side) res.Add(kvp.Key);
            }

            return res;
        }
        
        public List<int> StillAvailableCategories(Team side)
        {
            List<int> res= new List<int>();
            foreach (KeyValuePair<int, int> kvp in _seatsByDoor)
            {
                if (_sideByCategories[kvp.Key] == side && kvp.Value > 0) res.Add(kvp.Key);
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

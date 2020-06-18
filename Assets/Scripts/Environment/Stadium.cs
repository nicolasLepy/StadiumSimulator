using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{

    public class Ticket
    {
        private int _door;
        public int door => _door;
        private Vector3 _position;
        public Vector3 position => _position;
        public Ticket(int door, Vector3 position)
        {
            _door = door;
            _position = position;
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
        private Dictionary<int, Vector3> _categoriesPosition;
        private Dictionary<int, List<Vector3>> _categoriesPlaces;

        public void SetCategoryPosition(int category, Vector3 position)
        {
            _categoriesPosition.Add(category, position);
        }

        public void SetCategoryPlaces(int category, List<Vector3> places)
        {
            _categoriesPlaces.Add(category, places);
            _seatsByDoor[category] = places.Count;
        }

        private int _categoriesNumber;
        public int CategoriesNumber => _categoriesNumber;

        public Stadium(int zonesNumber)
        {
            _categoriesNumber = zonesNumber;
            _seatsByDoor = new Dictionary<int, int>();
            _sideByCategories = new Dictionary<int, Team>();
            _categoriesPosition = new Dictionary<int, Vector3>();
            _categoriesPlaces = new Dictionary<int, List<Vector3>>();
            for (int i = 1; i <= _categoriesNumber; i++)
            {
                _seatsByDoor.Add(i, 10);
                _sideByCategories.Add(i, Team.HOME);
            }

            _sideByCategories[4] = Team.AWAY;
            _sideByCategories[8] = Team.AWAY;
        }

        public Vector3 CategoryPosition(int category)
        {
            return _categoriesPosition[category];
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
                Vector3 first = _categoriesPlaces[door][0];
                _categoriesPlaces[door].Remove(first);
                res = new Ticket(door, first);
            }
            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourneySoft
{
    public class Player : IEquatable<Player>
    {
        public Guid id;
        public string name;
        public uint wins = 0; //#Of wins in current tournament for player
        public uint losses = 0; //#Of losses in current tournament for player
        public bool hasBye = false;  //Does the player currently have a bye
        public uint byeCount = 0; //Number of byes this player has had
        public uint points = 0; //Number of points to be used in ranking and pairing if needed
        public int rank = -1;
        public Dictionary<string, int> prevOpponents = new Dictionary<string, int>(); //Name, numTimesPlayed

        public Player()
        {
            id = new Guid();
            name = "NotSet"; //Default value for name.
        }

        public Player(string n)
        {
            id = new Guid();
            name = n;
        }

        public void AddOpponent(Player p)
        {
            if (prevOpponents.Keys.Contains(p.name))
            {
                prevOpponents[p.name]++;
            }else
            {
                prevOpponents.Add(p.name, 1);
            }
        }

        /// <summary>
        /// Return index of a player, given their name. Should NOT be used, use .FindIndex(x => x.name == "") instead
        /// </summary>
        /// <param name="n">Name to find</param>
        /// <param name="p">List to search</param>
        /// <returns>Returns index if found, -1 if not</returns>
        public static int getPlayerIndexByName(string n, List<Player> p) //Return player index based on name, or -1 if not found
        {
            for (int i = 0; i < p.Count; i++)
            {
                if(p[i].name == n)
                {
                    return i;
                }
            }
            {
                return -1;
            }
        }

        public override string ToString()
        {
            return name; 
        }

        public bool Equals(Player other)
        {
            return id == other.id;
        }
    }
}

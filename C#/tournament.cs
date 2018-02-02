using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourneySoft
{
    public class Tournament
    {
        public  List<Player> players = new List<Player>(); //List of each player in the tournaments
        public bool isActive = false; //Is the tournament running?
        public bool isOver = false;
        public int roundNumber = 0; //Current Round
        public int prevRoundNumber = 0; //Last round with pairings generated
        public string tourneyType = "unstarted"; //Default to semiswiss type but allow it to be changed
        public int numPlayersInPairs = 2; //Default to 2 players per pair
        public int numRounds = 3; //Default to 3 round tournaments

        protected Random r = new Random(Convert.ToInt32(System.DateTime.Now.Ticks % Int32.MaxValue)); //Seed random with ticks, safeguarded from int32 overflow

        public static string description = "Tournament where no type has been set";
        public static uint getLengthReccomendation() { return 3; } //Length of 3 unless overriden


        public class PlayerPairing : IEquatable<PlayerPairing>
        {
            public List<Player> pairedPlayers;
            public Player winner;
            public bool isBye;

            public bool Contains(Player other)
            {
                return pairedPlayers.Contains(other);
            }

            public bool Equals(PlayerPairing other)
            {
                return pairedPlayers == other.pairedPlayers;
            }
        }
        public List<PlayerPairing> pairings = new List<PlayerPairing>(); //List of pairings for tournament

        public Tournament()
        {
            isActive = false;
        }

        public Tournament(List<Player> p)
        {
            players = p;
        }

        /// <summary>
        /// Function to pair players, should be overriden in each tournament type class
        /// </summary>
        public virtual void PairPlayers() { }

        /// <summary>
        /// Function to rank players, should be overriden in each tournament type class
        /// </summary>
        public virtual void RankPlayers() { }

        /// <summary>
        /// Function to assign player points, should be overriden in each tournament type class that utilizes it
        /// </summary>
        public virtual void AssignPoints() { }

    }
}

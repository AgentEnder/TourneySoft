using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourneySoft
{
    class SemiSwiss : Tournament
    {
        new public static string description = "A variation on the original algorithm used in my group project for Intro to C++. This algorithm is similar to swiss pairing methods, in that pairs are based on the amount of wins a player has. It does not allow draws, and does not differentiate between a bye or a win. It differs from swiss pairing protocol in that it's possible for the same pair to be put together multiple times.";
        /// <summary>
        /// Initialize the tournament with a list of players only
        /// </summary>
        /// <param name="p">Players from currentTournament</param>
        public SemiSwiss(List<Player> p)
        {
            tourneyType = "semiswiss";
            isActive = true;
            players = p;
        }

        /// <summary>
        /// Initialize with number of rounds and list of players
        /// </summary>
        /// <param name="p">Players from currentTournament</param>
        /// <param name="nRounds">Number of rounds</param>
        public SemiSwiss(List<Player> p, int nRounds)
        {
            tourneyType = "semiswiss";
            isActive = true;
            players = p;
            numRounds = nRounds;
        }

        /// <summary>
        /// Initalize with list of players, number of rounds, and number of players
        /// </summary>
        /// <param name="p">List of players</param>
        /// <param name="nRounds">Number of rounds</param>
        /// <param name="nPlayers">Number of players in each pair</param>
        public SemiSwiss(List<Player> p, int nRounds, int nPlayers)
        {
            tourneyType = "semiswiss";
            isActive = true;
            players = p;
            numRounds = nRounds;
            numPlayersInPairs = nPlayers;
        }

        /// <summary>
        /// Create player pairings for next round
        /// </summary>
        public override void PairPlayers()
        {
            List<Player> playerPool = new List<Player>(players); //Players who have not been paired
            List<Player> currentPool = new List<Player>(); //Players currently being paired
            pairings.Clear(); //Clear Pairings to generate new pairings
            #region byePairing
            if (playerPool.Count % 2 == 1) //Odd number of players
            {
                uint? minValue = null; //Minimum number of byes in the pool
                foreach (var player in playerPool)
                {
                    if (player.byeCount < minValue || minValue == null) //If current player has fewer byes than minimum, or minimum is unset
                    {
                        minValue = player.byeCount;
                    }
                }
                foreach (var player in playerPool)
                {
                    if (player.byeCount == minValue)
                    {
                        currentPool.Add(player); //Add each player with min value to a pool
                    }
                }
                int playerIndex = r.Next() % currentPool.Count; //Choose random in pool
                PlayerPairing byePair = new PlayerPairing(); //PlayerPairing for the bye
                byePair.pairedPlayers = new List<Player>();
                byePair.pairedPlayers.Add(currentPool[playerIndex]);
                byePair.pairedPlayers[0].byeCount += 1; //Increment byeCount of the player who recieved the bye
                byePair.pairedPlayers.Add(new Player("bye"));
                playerPool.RemoveAt(Player.getPlayerIndexByName(byePair.pairedPlayers[0].name, playerPool)); //Remove the player who recieved a bye from the pool
                currentPool.Clear();
                pairings.Add(byePair);
            }
            #endregion
            #region NormalPairing
            Player holdover = null; //Hold player in case a pool of players is odd
            while (playerPool.Count > 0) //While there are more players to assign
            {
                currentPool.Clear(); //Ensure currentPool is empty at start

                int maxWinsInPool = 0; //Value to hold max wins.
                foreach (var player in playerPool)
                {
                    if (player.wins > maxWinsInPool) //Player has more wins than previous max
                    {
                        currentPool.Clear(); //Empty previous pool
                        maxWinsInPool = (int)player.wins; //Increase max wins
                        currentPool.Add(player); //Add player to it.
                    }
                    else if (player.wins == maxWinsInPool) //Player has same number as max wins
                    {
                        currentPool.Add(player); //Add to pool
                    }
                    else
                    {
                        //Future use
                    }
                }
                if (holdover != null) //Previous player pool was odd, need to deal with extra player
                {
                    currentPool.Add(holdover); //Add this player to the current pool
                    holdover = null; //Holdover is now empty
                }
                if (currentPool.Count % 2 == 1) //Current Pool is odd.
                {
                    holdover = currentPool[0]; //Add first player to holdover.
                    playerPool.Remove(currentPool[0]); //Remove them from the playerPool.
                    currentPool.RemoveAt(0); //Remove them from the currentPool
                }
                while (currentPool.Count > 0) //Players in current pool to deal with, will always run an even number of times.
                {
                    PlayerPairing pair = new PlayerPairing(); //New Pair
                    pair.pairedPlayers = new List<Player>();
                    pair.pairedPlayers.Add(currentPool[r.Next() % currentPool.Count]); //Pick first player
                    currentPool.Remove(pair.pairedPlayers[0]); //Remove from currentPool
                    pair.pairedPlayers.Add(currentPool[r.Next() % currentPool.Count]); //Pick Second Player
                    currentPool.Remove(pair.pairedPlayers[1]); //Remove from currentPool
                    pairings.Add(pair); //Add pairing to list of pairings
                    playerPool.Remove(pair.pairedPlayers[0]); //Remove both players from playerPool.
                    playerPool.Remove(pair.pairedPlayers[1]);
                }
            }
            #endregion
        }

        /// <summary>
        /// Rank players, works similarly to the PairPlayers function
        /// </summary>
        public override void RankPlayers()
        {
            int maxWins = 0;
            int numPools = 0;
            List<Player> playerPool = new List<Player>(players);
            List<Player> currentPool = new List<Player>();
            while(playerPool.Count > 0)
            {
                currentPool.Clear();
                maxWins = 0;
                foreach (var player in playerPool)
                {
                    if (player.wins > maxWins)
                    {
                        maxWins = (int)player.wins;
                        currentPool.Clear();
                        currentPool.Add(player);
                    }else if(player.wins == maxWins)
                    {
                        currentPool.Add(player);
                    }
                }
                foreach (var player in currentPool)
                {
                    players.Find(x => x.name == player.name).rank = numPools+1; //Set rank to number of current pool
                    playerPool.Remove(player); //Remove from playerPool
                }
                numPools += 1;
            }
        }

        /// <summary>
        /// Length reccomenation based on MTG's swiss round implementation
        /// </summary>
        /// <returns>Returns an unsigned integer reccomendation</returns>
        new public static uint getLengthReccomendation()
        {
            uint numRounds = (uint)Math.Ceiling(Math.Log(Global.currentTournament.players.Count, 2));
            return numRounds;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourneySoft
{
    class Swiss : Tournament
    {
        new public static string description = "An implementation of swiss system pairing. It will not pair the same group twice, but will still pair based on number of wins and losses. It also allows for draws.";
        private int ptsPerWin = 3;
        private int ptsPerDraw = 1;
        private int ptsPerBye = 2;
        private int ptsPerLoss = 0;
        private List<List<Player>> prevPairs = new List<List<Player>>();

        private class possPairs
        {
            public List<List<Player>> pairs = new List<List<Player>>();
            public int totalCost = 0;
        }

        /// <summary>
        /// Initialize the tournament with a list of players only
        /// </summary>
        /// <param name="p">Players from currentTournament</param>
        public Swiss(List<Player> p)
        {
            tourneyType = "swiss";
            isActive = true;
            players = p;
        }

        /// <summary>
        /// Initialize with number of rounds and list of players
        /// </summary>
        /// <param name="p">Players from currentTournament</param>
        /// <param name="nRounds">Number of rounds</param>
        public Swiss(List<Player> p, int nRounds)
        {
            tourneyType = "swiss";
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
        public Swiss(List<Player> p, int nRounds, int nPlayers)
        {
            tourneyType = "swiss";
            isActive = true;
            players = p;
            numRounds = nRounds;
            numPlayersInPairs = nPlayers;
        }

        public override void PairPlayers()
        {
            pairings.Clear(); //Clear pairings from prev round
            List<Player> playerPool = new List<Player>(players); //Pool to pull players from
            List<Player> validOpponents = new List<Player>(); //Opponents who are valid for the current player
            List<Player> currentPool = new List<Player>(); //Pool to hold temporary values
            //while (playerPool.Count > 0) //Commented out to avoid infinite loop since pairing NYI
            {
                if(currentPool.Count > 0) { currentPool.Clear(); } //Clear the temporary pool of any values it contains
                #region byePairing
                while (playerPool.Count%numPlayersInPairs != 0) //Assign byes until playerPool contains a number divisible into pairs;
                {
                    uint? minValue = null; //Minimum number of byes in the pool
                    playerPool = playerPool.OrderBy(o => o.byeCount).ToList(); //Order players by number of byes
                    minValue = playerPool[0].byeCount; //First player has min number
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
                    for (int i = 1; i < numPlayersInPairs; i++)
                    {
                        byePair.pairedPlayers.Add(new Player("bye")); //Add n-1 players named bye to pair where n is equal to numPlayersInPair
                    }
                    playerPool.Remove(currentPool[playerIndex]);//RemoveAt(Player.getPlayerIndexByName(byePair.pairedPlayers[0].name, playerPool)); //Remove the player who recieved a bye from the pool
                    currentPool.Clear();
                    pairings.Add(byePair);
                } //END BYE PAIRING
                #endregion byePairing
                #region MyNormalPairing
                /*playerPool = playerPool.OrderByDescending(o => o.points).ToList();
                PlayerPairing pair = new PlayerPairing();
                pair.pairedPlayers = new List<Player>();
                pair.pairedPlayers.Add(playerPool[0]);
                playerPool.RemoveAt(0);
                foreach (Player player in playerPool)
                {
                    if (!(pair.pairedPlayers[0].prevOpponents.Contains(player))) //Player has not matched with player yet
                    {
                        validOpponents.Add(player); //Add to valid opponents
                    }
                }
                validOpponents = validOpponents.OrderBy(o => o.points).ToList();
                while(pair.pairedPlayers.Count < numPlayersInPairs)
                {
                    currentPool.Clear(); //Clear the currentPool before building a new one
                    while(validOpponents.Count > 0)
                    {
                        currentPool.Add(validOpponents[0]);
                        validOpponents.RemoveAt(0);
                        if(validOpponents.Count == 0){ break; }else
                        if (validOpponents.Count == 1) //Break before checking next item if no next item
                        {
                            currentPool.Add(validOpponents[0]);
                            validOpponents.RemoveAt(0);
                            break;
                        }else if(validOpponents[0].points > validOpponents[1].points) //break out if next item is different pool
                        {
                            break;
                        }
                    }
                    int opponentIndex = r.Next() % currentPool.Count;
                    pair.pairedPlayers[0].prevOpponents.Add(currentPool[opponentIndex]);
                    pair.pairedPlayers.Add(currentPool[opponentIndex]);
                    playerPool.Remove(currentPool[opponentIndex]);
                    currentPool.RemoveAt(opponentIndex);
                }
                pairings.Add(pair);*/
                #endregion
                #region MyAttemptAtMinimumCostPairs
                /*
                //BUILD COSTS
                Dictionary<List<Player>, int> CostLookup = new Dictionary<List<Player>, int>();
                var allCombinations = Global.Combinations(playerPool, numPlayersInPairs).ToList();
                foreach (var item in allCombinations)
                {
                    int pairCost = 0;
                    if (prevPairs.Contains(item.ToList())){
                        pairCost += 1000; //Add large cost if pair has already been made
                    }
                    float sumDifference = 0;
                    int i = 0; //num subcombination
                    foreach (var subCombination in item.Combinations(2).ToList())
                    {
                        List<Player> playerList = subCombination.ToList();
                        i += 1;
                        sumDifference += Math.Abs(playerList[1].wins - playerList[0].wins);
                    }
                    pairCost += (int)(sumDifference / i);
                    CostLookup.Add(item.ToList(), pairCost);
                } //Costs should be added

                //List<Players> = Pairing
                //List<List<Players>> = List of Pairings
                //List<List<List<Players>>> = List of Possible Pairings

                List<possPairs> validPairs = new List<possPairs>();

                var PossiblePairings = allCombinations.Combinations(playerPool.Count / numPlayersInPairs).ToList();
                foreach (IEnumerable<IEnumerable<Player>> pairingEnum in PossiblePairings)
                {
                    List<Player> usedPlayers = new List<Player>();
                    bool invalidPairing = false;
                    foreach(IEnumerable<Player> pair in pairingEnum)
                    {
                        foreach (Player p in pair)
                        {
                            if (usedPlayers.Contains(p)) invalidPairing = true;
                            if (invalidPairing) break;
                        }
                        if (invalidPairing) break;
                    }
                    if (invalidPairing) { PossiblePairings.Remove(pairingEnum); }
                    else
                    {
                        int PairingCost = 0;
                        List<List<Player>> pairs = new List<List<Player>>();
                        foreach (var pair in pairingEnum.ToList())
                        {
                            List<Player> pairList = pair.ToList();
                            pairs.Add(pairList); 
                            PairingCost += CostLookup[pairList]; //Throws error due to equivalence not working?!?!?!?!
                        }
                        possPairs p = new possPairs();
                        p.pairs = pairs;
                        p.totalCost = PairingCost;
                        validPairs.Add(p);
                    }
                }

                int minCost = -1;
                possPairs minCostPairings = new possPairs();
                foreach (possPairs pair in validPairs)
                {
                    if(minCost == -1 || pair.totalCost < minCost)
                    {
                        minCost = pair.totalCost;
                        minCostPairings = pair;
                    }
                }

                foreach (List<Player> pair in minCostPairings.pairs)
                {
                    PlayerPairing p = new PlayerPairing();
                    p.pairedPlayers = pair;
                    p.isBye = false;
                    pairings.Add(p);
                }
                */
                #endregion
                #region CommentedNotesOnNYI
                /*
                 * 
                 * OK, so standard swiss rounds are made a whole lot harder
                 * due to the fact that two players cannot be paired at the same time.
                 * 
                 * My attempt for normal pairing found elligible "enemies" for player 1
                 * and then matched him. This continued for each other player, until
                 * every player was in a pair.
                 * 
                 * This works perfectly, until you get to generating the final pair.
                 * After round 1, It is possible to generate pairs such that the 
                 * first n/n+1 pairs are created without any pairs being the same, 
                 * yet the last pair was already in round 1.
                 * 
                 * Due to this, I have found that the solution is to use 
                 * minimum cost-maximum matching, A concept in discrete mathematics and
                 * graph theory.
                 * 
                 * I have saved my attempt at it, but it is horribly ineffecient to the point
                 * of uselessness.
                 * 
                 * I hope to revisit this when I have the time, but for now
                 * this will not be implemented.
                 * 
                 * Since this has been put off, I will work on making semiswiss more traditional
                 * with tiebreakers. 
                 * 
                 */
                #endregion
                #region AttemptAtGeneratingOnTheFlyPairings
                foreach(var p in players)
                {
                    var otherPlayers = players.Where(x => x != p);
                    foreach (var o in otherPlayers)
                    {
                        p.AddOpponent(o);
                    }
                }
                getPairings();
                #endregion
            }
        }

        private List<Player> getPairings()
        {
            List<Player> finalPairings = new List<Player>();

            //Generate All Possible Pairings, stored in a hashmap with their respective costs.
            List<PlayerPairing> currentPairs = new List<PlayerPairing>();
            Player endPlayer = players[players.Count - 1]; // end at last player
            int currentPlayerIndex = 0;
            //Generate list of pairings where each
            while (players[currentPlayerIndex] != endPlayer)
            {
                var currentPlayer = players[currentPlayerIndex];
                List<Player> validOpponents = new List<Player>();
                if(currentPlayer.prevOpponents.Count == players.Count - 1) //Has went up against everyone already
                {
                    var tempRange = currentPlayer.prevOpponents.Where(e => e.Value == currentPlayer.prevOpponents.Min(x => x.Value));
                    var tempDictionary = tempRange.ToDictionary(x=> x.Key, x=> x.Value);
                    validOpponents.AddRange(players.FindAll(x=>tempDictionary.Keys.Contains(x.name)));
                }else
                {
                    validOpponents.AddRange(players.FindAll(x => !currentPlayer.prevOpponents.Keys.Contains(x.name)));
                }
                foreach (var p in validOpponents)
                {
                    throw new NotImplementedException();
                }
                currentPlayerIndex++; //Iterate starting player
            }
            return finalPairings;
        }


    }
}

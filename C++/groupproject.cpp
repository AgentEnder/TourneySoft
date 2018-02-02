/*
	Group Project (Tournament Pairing)
	
	Authors:
		Craigory Coppola
		Joshua Webb
	
*/

#include <iostream>
#include <cstdlib>
#include <string>
#include <vector>
#include <sstream> //Used purely for conversion of string to integer!
#include <time.h> //Used to seed random

using namespace std;

class Player
{
	private:
		string name;
		int wins = 0; //# of wins in current tournament for player
		int losses = 0; //# of losses in current tournament for player
		bool hasBye = false; //Does the player have a bye for the current round?
		bool hadBye = false; //Has the player had a bye in previous rounds?
		int score = 0;
	public:
		string getName() const{
			return name;
		}
		int getWins() const{
			return wins;
		}
		int getLosses() const{
			return losses;
		}
		void addWin(){
			wins+=1;
		}
		void addLoss(){
			losses+=1;
		}
		void setName(string n){
			name = n;
		}
		void giveBye(){
			hasBye = true;
			hadBye = true;
		}
		void takeBye(){
			hasBye = false;
		}
		bool getBye() const{
			return hasBye;
		}
		bool getPreviousByes() const{
			return hadBye;
		}
		int getScore() const{
			return score;
		}
		void setScore(int _score){
			score = _score;
		}
};

struct playerPairing{ //Structure to hold round pairings
	Player p1; //Player One
	Player p2; //Player Two
	bool isBye = false; // initialization of variables in structs added in cpp 11
};

//Function Prototypes
int findPlayerPlaceInVector(vector<Player>, string);
int findMaxWinsInPlayerVector(vector<Player>);
int convertStringToNumber(string i);
vector<playerPairing> pairPlayers();
void calculateScores(); //	<--THIS F(x) uses Arrays
void waitForUserInput();
void clearConsole();


//GLOBAL VARIABLES
int roundNumber = 1; //What round is the tournament on?
bool roundPaired = false; //Has the current round pairings already been generated?
vector<Player> players; //Store all active players
vector<playerPairing> pairings; //Store rounds pairings
Player topFour[4]; //Array of size 4 to store the top 4 players when the tournament ends.  <-- ARRAY

int main(){
	srand(time(NULL)); // Seed Random for Differenct player pairs each time the program is ran!
	enum MenuStates {MAINMENU,INPUTPLAYERS, VIEWPAIRINGS, DROPPLAYERS, NEXTROUND, ENDTOURNAMENT}; //All possible menu states
	MenuStates m = MAINMENU; //Container for the current menu state, defaults to the main menu
	bool programRunning = true; //Variable to check whether the main program loop should execute
	while(programRunning){ //Loop through the menu system
		switch(m){ //Skip to the selected menu
			/*
				MAIN MENU
			*/
			case MAINMENU:{
				clearConsole();
				cout << "Would you like to:" << endl; //Display Menu Options
				cout << "(1)Input players for new tournament," << endl;
				cout << "(2)View player pairings," << endl;
				cout << "(3)Drop a player," << endl;
				cout << "(4)Move to the next round" << endl;
				cout << "(5)End the Tournament and View Results"<< endl;
				cout << "(6)Exit the program\n" << endl;
				
				string userChoice; //Variable to hold user's raw input.
				int integerChoice; //Variable to hold integer formed by parsing raw input
				cin >> userChoice; //Get raw Input
				
				integerChoice = convertStringToNumber(userChoice); //Parse raw input and store it in container
				
				if(integerChoice == 6){ //If choice is 5, program should exit!
					clearConsole(); //Clear out the menu
					programRunning = false; //Exit the Infinite Loop
				}else if (1 <= integerChoice && integerChoice <= 5){ //Valid menu state range
					m = static_cast<MenuStates>(integerChoice); //Set menu state to user choice
				}
				
				break;
			}
			/*
				INPUTPLAYERS MENU
			*/
			case INPUTPLAYERS:{
				
				
				bool leaveMenu = false; //Variable to hold truth value of the statement: The last player has been entered.
				cin.ignore(); //Skip over the newline charachter currently on the stack
				while(!leaveMenu){ //Loop over data entry screen until ready to leave
					clearConsole(); //Refresh Menu
					//INPUT ALL DATA FOR PLAYER
					cout << "Enter player name, or type exit to return to the main menu:";
					Player p; //create player object;
					string n; //container for name
					getline(cin, n); 
					
					if(n == "exit" || n == ""){ //Ready to leave data entry
						m = MAINMENU;
						leaveMenu = true; //Mark as ready
						break;
					}
					
					p.setName(n); //set player name
					players.push_back(p); //add player to vector
					//ALL Data is Entered
					roundPaired = false;
					cout<<"\n\n The player has been entered. \n";
					waitForUserInput();
					
				}
				break;
			}
			/*
				VIEWPAIRINGS MENU
			*/
			case VIEWPAIRINGS:{
				clearConsole();
				
				
				
				if(!roundPaired){ //Check if current round has already been set up, if not generate new pairings.
					pairings = pairPlayers(); //Get round pairings
					roundPaired = true;
				}
				
				for(int i = 0; i < pairings.size(); i++){
					cout << "----------------" << endl;
					if(pairings[i].isBye == true){
						cout << pairings[i].p1.getName() << " has a bye!" << endl;
					}else{
						cout << "\nPairing # " << i << ": " << endl;
						cout << "Player 1:" << pairings[i].p1.getName() << "(W:" << pairings[i].p1.getWins() << " L:" << pairings[i].p1.getLosses() << ")" <<endl;
						cout << "Player 2:" << pairings[i].p2.getName() << "(W:" << pairings[i].p2.getWins() << " L:" << pairings[i].p2.getLosses() << ")"  <<endl;
					}
				}
				waitForUserInput(); 
				m = MAINMENU; //Return to main menu
				break;
			}
			/*
				EDITCUSTOMER MENU
			*/
			case DROPPLAYERS:{
				cout << "Who would you like to drop?" << endl;
				string dropName;
				cin.ignore();
				getline(cin, dropName);
				int playerPlace = findPlayerPlaceInVector(players, dropName);
				players.erase(players.begin() + playerPlace);
				waitForUserInput();
				m = MAINMENU;
				break;
			}
			case NEXTROUND:{
				//Check if current round is paired yet, if not ensure pairs are made first
				if(!roundPaired){
					cout << "You must view the current round pairings before moving on!" <<endl;
				}else{
					//LOOP THROUGH EACH PLAYER PAIRING, GET WINS/LOSSES, THEN ASSIGN THEM TO PLAYERS IN PLAYER VECTOR
					for(int i = 0; i < pairings.size(); i++){
						if(pairings[i].p1.getBye() == true){
							players[findPlayerPlaceInVector(players, pairings[i].p1.getName())].addWin();
							players[findPlayerPlaceInVector(players, pairings[i].p1.getName())].takeBye();
						}else{
							string userChoice; //Variable to hold user's raw input.
							int integerChoice = 0; //Variable to hold integer formed by parsing raw input
							while(integerChoice != 1 && integerChoice != 2){
								clearConsole();
								cout << "Enter the number next to the winning player." << endl; 
								cout << "(1)" << pairings[i].p1.getName() << endl; 
								cout << "(2)" << pairings[i].p2.getName() << endl;
								cin >> userChoice; //Get raw Input 
								integerChoice = convertStringToNumber(userChoice); //Convert user input to integer, compare it to players before looping again if incorrect input
							}
							if(integerChoice == 1){
								players[findPlayerPlaceInVector(players, pairings[i].p1.getName())].addWin(); //Add win for player 1
								players[findPlayerPlaceInVector(players, pairings[i].p2.getName())].addLoss(); //Add loss for player 2
							}else{
								players[findPlayerPlaceInVector(players, pairings[i].p2.getName())].addWin();//Add win for player 2
								players[findPlayerPlaceInVector(players, pairings[i].p1.getName())].addLoss();//Add loss for player 1
							}
						}
					}
					//END LOOP
					clearConsole();
					roundPaired = false; //Ensure a new round is generated when pairings are viewed next
					roundNumber++; //Increment the round number
					cout << "Pairings for round " << roundNumber << " will be generated the next time pairings are viewed!" << endl;
				}
				waitForUserInput();
				m = MAINMENU; //Go back to the main menu
				break;			
			}
			case ENDTOURNAMENT:
			{
				clearConsole();
				calculateScores(); //Execute code to calculateScores
				
				cout << "1st Place: " << topFour[0].getName() << endl; //Display first place <--ARRAY
				cout << "2nd Place: " << topFour[1].getName()<< endl; // Display 2nd place <-- ARRAY
				cout << "3rd Place: " << topFour[2].getName()<< endl; //Display 3rd <--ARRAY
				cout << "4th Place: " << topFour[3].getName()<< endl; //Display 4th <--ARRAY
				
				waitForUserInput();
				m = MAINMENU; //Set menu to main
				break;
			}
			default:{
				m = MAINMENU;
				break; //If menu state is not prepared, go to main menu
			}
		}
	}
	waitForUserInput();
}

//This function calculates the scores for each player in the players list and assigns places 1-4 for the tournament
void calculateScores(){  //REMEMBER Players = List Players
	int winPts = 3; //Points assigned per win
	int lossPts = 1; //Points assigned per loss
	int byePts = 2; //Points assigned per bye
	
	for(int i = 0; i < players.size(); i++){ //Loop through each player
		Player iPlayer = players[i]; //Contatiner to hold player
		players[i].setScore(iPlayer.getWins()*winPts + iPlayer.getLosses()*lossPts + iPlayer.getPreviousByes()*byePts); //Add up player score
	}
	
	Player firstPlayer; //Container for player #1
	firstPlayer.setScore(0); //Clear score to default
	Player secondPlayer; //Container for player #2
	secondPlayer.setScore(0); //Clear score to default
	Player thirdPlayer; //Container for player #3
	thirdPlayer.setScore(0);//Clear score to default
	Player fourthPlayer; //Container for player #4 
	fourthPlayer.setScore(0);//Clear score to default

	for(int i = 0; i < players.size(); i++ ){ //Loop through each player
		if(firstPlayer.getScore() < players[i].getScore()){ //if score higher than 1st
			fourthPlayer = thirdPlayer; //move third to last
			thirdPlayer = secondPlayer; //move second to third
			secondPlayer = firstPlayer; //move first to second
			firstPlayer = players[i]; //set first place
		}else if(secondPlayer.getScore() < players[i].getScore()){ //if score higher than 2nd
			fourthPlayer = thirdPlayer; //move third to 4th
			thirdPlayer = secondPlayer; //move second to 3rd
			secondPlayer = players[i]; //set second place
		}else if(thirdPlayer.getScore() < players[i].getScore()){ //if score higher than 3rd
			fourthPlayer = thirdPlayer; //set third to second
			thirdPlayer = players[i]; //set third place
		}else if(fourthPlayer.getScore() < players[i].getScore()){ //if score higher than fourth
			fourthPlayer = players[i]; //set fourth place 
		}
	}
	
	topFour[0] = firstPlayer; // <--ARRAY
	topFour[1] = secondPlayer;// <--ARRAY
	topFour[2] = thirdPlayer;// <--ARRAY
	topFour[3] = fourthPlayer;// <--ARRAY
	
}

//This function creates player pairings for each round
vector<playerPairing> pairPlayers(){
	vector<Player> remainingPlayers = players; //Unpaired players
	vector<playerPairing> pairedPlayers; //Vector of player pairs
	
	if(players.size()%2==1){ //if odd number of players
		int randomPlayer = rand() % players.size(); //Choose Player
		if(players.size()>=roundNumber){ //Is there a player who has not gotten a bye yet?
			while(players[randomPlayer].getPreviousByes()){ //Check if bye 
				randomPlayer = rand() % players.size(); //choose again
			}
		} //Otherwise give the bye to first random player
		players[randomPlayer].giveBye(); //assign bye
		playerPairing byePair;
		byePair.p1 = players[randomPlayer];
		byePair.isBye = true;
		pairedPlayers.push_back(byePair);
		//cout << players[randomPlayer].getName() << " has a bye!" << endl;
		remainingPlayers.erase(remainingPlayers.begin() + randomPlayer);
	}
	if(roundNumber == 1){ //First round should generate random pairs so it is a seperate case.
		//Pair players w/o byes
		while(remainingPlayers.size()>1){
			playerPairing pair; //Declare a container for the current pair
			int randomPlayer = rand() % remainingPlayers.size();
			pair.p1 = remainingPlayers[randomPlayer]; //assign player 1 to pair
			remainingPlayers.erase(remainingPlayers.begin() + randomPlayer); //remove player from pool of remainingPlayers
			randomPlayer = rand() % remainingPlayers.size(); 
			pair.p2 = remainingPlayers[randomPlayer]; //assign player 2 to pair
			remainingPlayers.erase(remainingPlayers.begin() + randomPlayer); //remove player from pool of remainingPlayers
			pairedPlayers.push_back(pair); //add pair to the pairings vector
		}
	}else{
		vector<Player> carryOverPlayer; //If a player pool is odd, a random player should be pulled from the pool and put here, or if there is a player here it should be moved to the pool.
		while(remainingPlayers.size()>=1){ //Loop until all pairs formed
			playerPairing pair; //Container to hold the player pair
			
			vector<Player> playerPool; //Container to hold pool of players with equivalent win values
			int playerPoolWins = -1; //Initialize to -1 as it should never be a valid value, if it stays -1, there is a problem
			
			for(int i = 0; i < remainingPlayers.size(); i++){ //Loop through each player
				if(playerPoolWins == -1){ //Check for default value
					playerPoolWins = findMaxWinsInPlayerVector(remainingPlayers); //set current max win value
				}
				if(remainingPlayers[i].getWins() == playerPoolWins){ //If player has max win value
					playerPool.push_back(remainingPlayers[i]); //Add players with max win value to the playerpool
				}
			}
			while(playerPool.size()>=1){ //Loop through each player in the current player pool
				if(playerPool.size()%2 == 1){ //If playerpool is of odd size
					if(carryOverPlayer.size() == 0){ //If no player in the carryOverPlayer container
						int randomPlayer = rand() % playerPool.size(); //Choose random player from pool
						carryOverPlayer.push_back(playerPool[randomPlayer]); //Add them to the carryOverPlayer container
						remainingPlayers.erase(remainingPlayers.begin() + findPlayerPlaceInVector(remainingPlayers, playerPool[randomPlayer].getName())); //Remove them from the remainingPlayers container, as they should not be looked at until next pool is formed.
						playerPool.erase(playerPool.begin()+randomPlayer); //Remove them from current player pool
					}else{
						playerPool.push_back(carryOverPlayer[0]); //Re-add the player from carryOverPlayer container to the player pool
						remainingPlayers.push_back(carryOverPlayer[0]); //Re-add the player from carryOverPlayer container to remainingPlayers list
						carryOverPlayer.pop_back(); //Empty the carryOverPlayer container
					}
				}
				if (playerPool.size() != 0) { //Skip if empty player pool (This will only happen when pool of size one is evaluated/)
					int randomPlayer = rand() % playerPool.size(); //Choose random p1
					pair.p1 = playerPool[randomPlayer]; //assign p1
					remainingPlayers.erase(remainingPlayers.begin() + findPlayerPlaceInVector(remainingPlayers, playerPool[randomPlayer].getName())); //Remove p1 from remainingPlayers
					playerPool.erase(playerPool.begin() + randomPlayer); //Remove p1 from playerPool
					randomPlayer = rand() % playerPool.size(); //Choose random for p2
					pair.p2 = playerPool[randomPlayer]; //assign p2
					remainingPlayers.erase(remainingPlayers.begin() + findPlayerPlaceInVector(remainingPlayers, playerPool[randomPlayer].getName())); // remove p2 from remainingPlayers
					playerPool.erase(playerPool.begin() + randomPlayer); //remove p2 from playerpool
					pairedPlayers.push_back(pair); //Push pair of players into pairedPlayers
				}
			}
		}
	}
	return pairedPlayers; //Return vector of player pairs
}

//This function searches a vector<player> (vp) for a player with name n
int findPlayerPlaceInVector(vector<Player> vp, string n){
	for(int i = 0; i < vp.size(); i++){ //Iterate through arg vector
		if(vp[i].getName() == n){ //If player name matches
			return(i); //Return place in args vector
		}
	}
	return -1; // Nothing was found
}

//This function searches a vector<player> (vp) for the number of max wins
int findMaxWinsInPlayerVector(vector<Player> vp){
	int max = 0; //Default max value is 0
	for(int i = 0; i < vp.size(); i++){ //iterate through player vector
		if(max < vp[i].getWins()){ // check inequality
			max = vp[i].getWins(); //If current player has higher win value, assign to max
		}
	}
	return max; //Return max value
}

void waitForUserInput(){
	system("PAUSE"); //Abstracted function for readability in code and easy refactoring.
}

void clearConsole(){
	system("cls"); //Abstracted function for readability in code and system dependance.
}

//This function uses a stringstream object to convert text to an integer.
int convertStringToNumber(string i){
	string text = i;//string containing the number
	int result;//number which will contain the result

	stringstream convert(text); // stringstream used for the conversion initialized with the contents of Text

	if ( !(convert >> result) ){//give the value to Result using the characters in the string
		result = 0;
	}
	return result;
}
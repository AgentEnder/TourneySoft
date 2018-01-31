from math import factorial

class Player:
	#Constructor
	def __init__(self, name, score):
		self.name = name #Player Name
		self.score = score #Player "Score"
		self.opponents = {}
	
	#Overload Equality
	def __eq__(self, other):
		return self.name == other.name
	#Overload Str
	def __str__(self):
		return self.name
	def __hash__(self):
		return hash(self.name)
	#Add Player to Opponents List
	def addOpponent(self, opponent):
		if(opponent not in self.opponents):
			self.opponents[opponent] = 0;
		self.opponents[opponent] += 1;

class Pair:
	def __init__(self, p1, p2, cost):
		self.p1 = p1
		self.p2 = p2
		self.cost = cost #Cost Assigned to the Pair

def ChoosePairings(pairs): #Args: List of player pairings to determine from.
	pass	#To be implemented

def main():
	Players = [] #List to hold all players
	Pairs = [] #List to hold pairs
	for i in range(20):
		Players.append(Player(str(i), 0))
	
	#Generate pairs in lexographic order
	for i in range(len(Players)-1): #Loop through from index 0, to index n-2 (where n is the # of players)
		for k in range(i+1, len(Players)): #Loop through from index i+1 to index n-1 where n is the number of players)
			Pairs.append(Pair(Players[i], Players[k], Players[i].score-Players[k].score)) #Add pair to the list.
			Players[i].addOpponent(Players[k])
			
	for pair in Pairs: #Print each pairing.
		print("P1: ", pair.p1.name, " |P2: ", pair.p2.name)
		
	print("Pairs created: ", len(Pairs), "/", (len(Players)*(len(Players)-1))/2) #Ensure all pairs were generated by comparing the length of the pairs array to (n choose 2) calculated through math.
main()
input("Press enter to continue")
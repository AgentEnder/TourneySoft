#######################################################
###################Craigory Coppola####################
##Generate graph with each node having exactly 1 edge##
#######################################################


#In actuality, this is a fully random pairing algorithm between n players
#Next step is to assign weights to the edges before deletion, and then use minimum cost edges first.

from random import randint

def generateGraphFromN(n): #Graph of n nodes where each node has exactly 1 edge, input is n.
	graph = [] #array to edges
	for i in range(0, n-1):
		for k in range(i+1,n):
			graph.append([i,k]) #Every possible edge created.
	new_graph = []
	while(len(new_graph) < n/2): #run until edges generated
		indexOfEdge = randint(0, len(graph)-1)
		print("Edge chosen: ", graph[indexOfEdge][0], "->", graph[indexOfEdge][1])
		new_graph.append(graph[indexOfEdge])
		n1 = graph[indexOfEdge][0] #Node 1
		n2 = graph[indexOfEdge][1] #Node 2
		indiciesOfBadEdges = []
		for index, edge in enumerate(graph): #Get indicies of edges which contain either node
			if n1 in edge or n2 in edge:
				print("Edge removed: ", edge[0], "->", edge[1])
				indiciesOfBadEdges.append(index)
		for idx in sorted(indiciesOfBadEdges, reverse=True): #delete items at these indicies. This is reversed to avoid changing indicies before deleting
			del(graph[idx])
			
	return new_graph
	
def printGraph(g):
	for edge in g:
		print(edge[0], ":", edge[1])
		
def main():
	graph = generateGraphFromN(10)
	printGraph(graph)
	
main()
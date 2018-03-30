#######################################################
###################Craigory Coppola####################
##Generate graph with each node having exactly 1 edge##
#######################################################


# In actuality, this is a fully random pairing algorithm between n players
# Next step is to assign weights to the edges before deletion, and then use minimum cost edges first.

from random import randint


def generate__graph_from_n(n):  # Graph of n nodes where each node has exactly 1 edge, input is n.
    graph = []  # array to edges
    for i in range(0, n - 1):
        for k in range(i + 1, n):
            graph.append([i, k])  # Every possible edge created.
    new_graph = []
    while len(new_graph) < n / 2:  # run until edges generated
        index_of_edge = randint(0, len(graph) - 1)
        print("Edge chosen: ", graph[index_of_edge][0], "->", graph[index_of_edge][1])
        new_graph.append(graph[index_of_edge])
        n1 = graph[index_of_edge][0]  # Node 1
        n2 = graph[index_of_edge][1]  # Node 2
        indicies_of_bad_edges = []
        for index, edge in enumerate(graph):  # Get indicies of edges which contain either node
            if n1 in edge or n2 in edge:
                print("Edge removed: ", edge[0], "->", edge[1])
                indicies_of_bad_edges.append(index)
        for idx in sorted(indicies_of_bad_edges,
                          reverse=True):  # delete items at these indicies. This is reversed to avoid changing indicies before deleting
            del (graph[idx])

    return new_graph


def print_graph(g):
    for edge in g:
        print(edge[0], ":", edge[1])


def main():
    graph = generate__graph_from_n(10)
    print_graph(graph)


main()

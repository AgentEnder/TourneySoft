import Graph
import random


class Player:
    def __init__(self, player_id):
        self.player_id = player_id
        self.wins = 0
        self.opponents = {}

    def __int__(self):
        return self.player_id

    def __str__(self):
        return str(self.player_id) + ": w = " + str(self.wins)

    def __eq__(self, other):
        return self.player_id == other.player_id

    def __hash__(self):
        return hash(repr(self))

    def  __lt__(self, other):
        return self.wins < other.wins


def calculate_weights():
    for edge in g.get_edges():
        price = 0
        if edge[0] in edge[1].opponents.keys():  # previous pairing
            price += 1000*edge[1].opponents[edge[0]]
        price += abs(edge[0].wins - edge[1].wins)  # difference in wins.
        edge[2] = price


def pair_players():
    g.make_complete()
    for p in players:
        max_repeats = 0
        if len(p.opponents) > 0:
            max_repeats = max(p.opponents.values())
        for key in p.opponents.keys():
            if p.opponents[key] == max_repeats:
                g.remove_edge(p, key)
    calculate_weights()
    nodes = g.get_nodes()

    next_round = Graph.Graph()
    for node in nodes:
        next_round.add_node(node)
    while True:
        nodes_min_degree = []
        nodes = g.get_nodes()
        if len(nodes) == 0:
            break
        min_node_degree = min([g.get_node_degree(x) for x in nodes])
        for node in nodes:
            if g.get_node_degree(node) == min_node_degree:
                nodes_min_degree.append(node)
        start_node = random.choice(nodes_min_degree)
        edges = g.get_edges_by_node(start_node)
        min_edge_weight = min([edge[2] for edge in edges])
        edges_min_weight = []
        for edge in edges:
            if edge[2] == min_edge_weight:
                edges_min_weight.append(edge)
        pair = random.choice(edges_min_weight)
        next_round.add_edge(pair[0], pair[1], pair[2])
        if pair[1] in pair[0].opponents.keys():
            pair[0].opponents[pair[1]] += 1
            pair[1].opponents[pair[0]] += 1
        else:
            pair[0].opponents[pair[1]] = 1
            pair[1].opponents[pair[0]] = 1
        g.remove_node(pair[0])
        g.remove_node(pair[1])
    return next_round


def assign_random_wins():
    for edge in g.get_edges():
        random.choice(edge[0:1]).wins += 1


g = Graph.Graph()

num_players = eval(input("How many players would you like to simulate? "))
num_rounds = eval(input("How many rounds would you like to simulate?"))
display_rounds = input("Would you like to see the graphs for each round? (y/n)")
display_rounds = True if str.lower(display_rounds)[0] == 'y' else False

players = [Player(x) for x in range(num_players)]  # x represents player id. 0 is number of wins.

for player in players:
    g.add_node(player)

for i in range(num_rounds):
    g = pair_players()
    print("Round " + str(i) + "| Average = " + str(g.calculate_avg_weight()) + "| Maximum = " + str(g.get_max_weight()))
    print("================================")
    g.print_edges()
    print()
    if display_rounds:
        g.display()
    assign_random_wins()

for x in sorted(players, reverse=True):
    print(x)

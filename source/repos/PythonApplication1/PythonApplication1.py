graph = {
    "A": ["B", "S"],
    "B": ["A"],
    "S": ["A", "C", "G"],
    "C": ["S", "D", "E", "F"],
    "G": ["S", "F", "H"],
    "D": ["C"],
    "F": ["C", "G"],
    "E": ["C", "H"],
    "H": ["E", "G"]
}

visited_nodes = {key: False for key in graph} 
node_parents = {key: None for key in graph}
dfs_result = []

def depth_first_search(start_node):
    stack = [start_node] 
    while stack:
        current_node = stack.pop()
        if not visited_nodes[current_node]:
            visited_nodes[current_node] = True
            dfs_result.append(current_node)
            for neighbor in reversed(graph[current_node]): 
                if not visited_nodes[neighbor]:
                    node_parents[neighbor] = current_node
                    stack.append(neighbor)

start = "A"
depth_first_search(start)

print("dfs_result:", dfs_result)
print("visited_nodes:", visited_nodes)
print("node_parents:", node_parents)

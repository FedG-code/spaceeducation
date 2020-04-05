---
layout: post
title: Pathfinding, and Unit Movement
description: Using Dijkstras Algorithm, to calculate shortest path
image: assets/images/unitmovement.jpg
---


A core theme in game making is how a player moves from one position to another. Because we have designed our game using a hexagonal tile based system where, for each turn the player unit moves from one tile to any connecting tile.  We chose to create a path queue, so that when a tile has been clicked; the shortest path is found using Dijkstras algorithm from the Players units current tile, to the destination tile.

<iframe width="560" height="315" src="https://www.youtube.com/embed/glupcI_zy58?rel=0&showinfo=0" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>


# Dijkstras Algorithm, and Hex Nodes and Edged

Dijkstra’s  algorithm uses graph theory to solve the shortest path from one initial node to one final node. By implementing this algorithm, we were able to take into acccount the weighted nature of each tile; enabling the shortest path to be found around elements such as a black holes, which might need to be avoided.

The following code was taken from [_Introduction to Algorithms_](#Cormen:2009aa), pp 658-662

```
DIJKSTRA (G,w,s)
  INITIALIZE-SINGLE-SOURCE.G;
  s = 0
  Q = G .V
  while Q != 0:
    u = EXTRACT-MIN(Q)
    S = S ∪ {u}
    for each ν ∈ G.Adj[u]
      RELAX(u,ν,w)
```

# Movement Queue

In Unity 3D, each game objects class has an `Start()` and `Update()` function. They are used to initialise data and variables at the beginning of the object instantiation in the game world, and then a hook in order to run certain functionality each time the frame is rendered; often called a running loop. The function Update() is called once per frame render, and is used to automate certain functionality.  We used the Update() function to implement a movement or destination queue, where the selectedUnit has a

 - `currentPath`: A List of Nodes from the selectedUnit's current position to a final destination node
 - `currentPathV3List`: A List of Vector3 to create a visual of the destination path.
 - `destinationList`: A List of Nodes which the update function will automatically iterate over, taking the first item, converting it to a vector, and setting it to destination.
 - `destination`: A Vector3 which the current Node is moving towards.


## Queue iteration

 1. When a Hex tile has been clicked, the function `GeneratePath` will create the shortest path, and pass it to the selectedUnit, and run selectedUnit.RenderPath; which willl create a line showing the current path.
 2. When selectedUnit recieves the path, a UI button is loaded (to do; currently we have hard coded to button to always be present, but will work on it only appeaaring when a path has been registerd).
 3. When the UI move button is pressed, this tells the map to tell the selectedUnit to add the next item from currentPath to destinationList, and remove that same item from currentPath. Then the currentPath is regenerated.
 4. selectedUnit Update hook noticies that there has been a new destination Vector3, and begins moving towards it. At this time, when the selectedUnit is between hex tiles, the user can add the next nodes to the queue by clicking the move button, and it will iterate through the destinationList rather than moving directly to the final destination.
 5. Each time a destination has been arrived at, the selectedUnit tile position (Q and R) are updated, so that a new path can be generated from that position, rather than the initial starting position.




<h1>Bibliography</h1>
<ol>
  <li id="Cormen:2009aa">
	<span class="Author"><strong>Cormen, T. H..</strong></span> &nbsp;
	<span class="Date">2009</span>&nbsp;
	<span class="Title">"Introduction to algorithms"</span>&nbsp;
	<span class="Publisher">MIT Press</span>&nbsp;
</li>

</ol>

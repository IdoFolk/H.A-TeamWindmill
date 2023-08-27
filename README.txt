Node Based Map Home Assignment By. Ido Folkman

The Following Project is a Node Based Level map that simulates a Map Transitioning between levels.

I have added a character that moves inside the map and can enter a level by standing close to it and pressing [space].
The levels will unlock when you complete the previous level and bonus levels will unlock by completing their parent level.

The Code is Pretty Simple with comments on all of the classes and methods.
Basically it is divded into 3 class: LevelManager, PlayerHandeler & NodeBehavior.
1. PlayerHandeler controls the movement of the character and the interaction with the level nodes.
2. NodeBehavior Stores the State of the Node (Open, Locked or Complete) and changes the node accordingly.
3. LevelManager Controls all of the nodes and their states and changes them accordingly.

The way nodes know which node is at what state is i gave each node a parent node (usually the previous level) 
and so all of the nodes exept the starting node are connected to each other and they simply check the State of their parent node
to determine their State Accordingly.

Thank You for the opportunity!
ido.
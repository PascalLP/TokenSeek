# TokenSeek
 COMP 476 A2
Made by Pascal Labelle Poblette

This Token Seek game consists of a Player and a Seeker. They will compete to obtain more tokens than the other. 
The game plays in a maze environment filled with Chasers, which will prevent the Player and Seeker from obtaining the tokens.

The Seeker's AI makes it go look for the closest token, and the pathfinding is handled by a NavMesh.

The Chaser's AI is controlled by a behaviour tree, and the pathfinding is also handled by a NavMesh.
The Chaser's behaviour tree can be seen in the diagram attached to the submission.

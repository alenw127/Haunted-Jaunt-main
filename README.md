# Haunted-Jaunt-main
Group: Alen Wilson
 
Dot Product:
The dot product was used to figure out the whether the player is within the ghost's FOV. It compares the direction of the ghost and the player's position to check if the player is in the line of sight. When it is it will chase the player. To trigger this just let the ghost spot you and it will chase.

Linear interpolation:
Linear interpolation was used to adjust the ghost's speed base on the distance to the player. If the ghost is farther it will go fast but the closer it gets to the player the slower it becomes. To trigger this just let the ghost spot you.

Particle Effect:
A TrailRenderer was used as a particle effect to creat a trail behind the ghost when it chases the player. To trigger this just let the ghost spot you.

Sound Effect:
A sound effect is played when the ghost spots the player in the ghost's FOV. To trigger this just let the ghost spot you.

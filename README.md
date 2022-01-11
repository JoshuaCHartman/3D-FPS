# 3D-FPS
 3D first person shooter (FPS).

V1.0 Jan 03,2022 - Jan 10, 2022

Created in Unity3D, with C# scripting in Visual Studio Community 2019.

# Features / Principles :

 #OOP & Unity features -
Singleton design used to create an EnemyManager script for spawning of enemies after destroyed by player, with coroutines and logic to limit number of spawned enemies.
Awake() loop used for references, Start() for setting indexes and starting values and states
Code reused between player and enemies
A mouse controller script (MouseLook) translates mouse input into player’s facing and aiming direction. Mathf & Quaternion.Euler functions used for constraints and rotations.
Melee damage via “Attack Points” of overlapping spheres using layer filtering, and de-/activated with animation events
Ranged damage via raycasting & passing out hit data
Weapon switching between 6 different weapons
Weapons aimable using a layered animation
Variable fire rates and sound effects

# Player Features - 
Dynamic stamina & UI status bar affected by sprinting/rest
Dynamic health UI bar affected by damage
Movements- crouch, run, walk, jump
Footstep sounds change based on speed
Animations controlled via state machines and logic
Simple animations created with keyframes

# Enemy Features -
Enemies use same Attack Point melee system as player 
Simple AI - patrol / chase / attack
Navmesh used to determine boundaries of map, and to calculate random patrol destinations based on timer
Enemies respawn after being eliminated

# Camera Features - 
Nested cameras - one for main view, and one for weapons. Composite creates game view
Camera is attached to the direction player looks/mouse movements

# Board Features -
9 spawn points
No hard border, but navmesh prevents enemies from leaving map

# UI Features - 
Dynamic Health and Stamina bars
Target reticle that increases in size when zoomed with some weapons

# To be implemented -
Score system - collect points when eliminating Enemies. 
High Score system - data persistence between game sessions.
Load/Start screen
Music 


Based on Awesome Tuts project & assets.

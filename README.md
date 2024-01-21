# Curing-Cores
A side scroller action rpg game where you kill enemies and learn the story of the game as you progress.

## Character Mechanics
To implement various mechanics for both the player character and the AI characters, the state pattern is utilized. A state base class is implemented for input handling and structuring of the child classes. Every single agent is able to transition to its own special states, although agents in general implement basic mechanics such as movement.

  <img src= "https://github.com/Enesozdogan/Curing-Cores/assets/72387932/df9f1286-656d-45a9-9cb6-5858fda9b5ee" width=100 height=100/>
  <img src ="https://github.com/Enesozdogan/Curing-Cores/assets/72387932/d3a79954-c2a9-48dc-a1c1-375392d6f208" width=100 height=100/>
  <img src ="https://github.com/Enesozdogan/Curing-Cores/assets/72387932/b420c52b-0025-4bce-ac15-9d972b3ab70d" width=100 height=100/>

## Special Abilites
The main character has 3 special abilites which are: Teleportation, BloodSlash and Lightning hook. These special abilites are integrated to state pattern. Also for outline shader and general ability checks, special ability classes called rings are implemented.
Player has to switch between abilites to perform. The outline shader is the indication for which ability is available to use.


<img src="https://github.com/Enesozdogan/Curing-Cores/assets/72387932/3643aa22-9a68-43d2-9f90-ba83ce8368d7" width=200 height=100/>
<img src="https://github.com/Enesozdogan/Curing-Cores/assets/72387932/91bde8fd-dfaf-4a9a-996a-1b436ff057df" width=200 height=100/>
<img src="https://github.com/Enesozdogan/Curing-Cores/assets/72387932/584cbd7f-fdf4-48c2-8c8c-76e630ce76f0" width=200 height=100/>


# Setting up UI for a level

## Prefabs

* Healthbar
* Hungerbar
* PauseMenu
* GameOverScreen
* GoalTracker
* LevelTransition

## Dependencies

* ex: GameObject > ChildObject
	- Dependency

* GameStateManager
	- PauseMenu
	- GameOverScreen
* PauseMenu > NavigationMenu
	- LevelTransition
* GameOverScreen > NavigationMenu
	- LevelTransition
* Healthbar
	- Player

## Events:

* ex: GameObject > Event
	- GameObject > Method

* Health Manager > Death Event
	- GameStateManager > SetGameOver
* Health Manager > Health Event
	- Healthbar > SetHealth
* Health Manager > Max Health Event
	- Healthbar > SetMaxHealth
* Stomach > Digestion Timer Event
	- Hungerbar > SetDigestionTimer
* Stomach > Stomach Contents Event
	- Hungerbar > SetStomachContents
* TBD
	- Goal Tracker > SetTracker(int currentDigestedFood, int targetDigestedFood)

# THE MAZE CHALLENGE

## Notes by Pablo 

This code resvolves a Maze of 25x25 which consumes the API mentioned in the section [API Specs](#API-specs).

For solving the Maze, I currently have implemented 2 algorithms: 

1. Wall Follower Algorithm
2. One created by me called Direction Algorithm in de code

I have 3 remaining implementations left: 

1. DeadEnd Algorithm 
2. Recursive Algorithm 
3. Treamaux Algorithm.

Four out of those 4 algorithms were algorithms I found after googling Maze Theory. The algorithm named Direction Algorithm was created by me. This is proof that I am capable of creating algorithms on my own, but also I am dedicated enough to read documentation and be wise enough to not re-invent the wheel.

The person that executes the code can choose which algorithm to use simply by going to the class `PlayGame` and editing line `20` with the name of the algorithm that you want to use.


### How to execute 

To execute the program, make sure you have .NET 7 
And run the following commands in a Terminal: 

```
dotnet clean
dotnet build
dotnet run
```

## Given explanation

### Problem 
------------------

There is an API where you can:

- Generate mazes.
- Start a game inside a previously generated maze.
- Inside a game, move around the maze until you reach the end of the maze.
(see APISpecs.txt for more info about the API)


In this test you have to choose one of two options.

- Make a console app where automatically (without user interaction):
  - creates a maze of 25x25
  - starts a game
  - send movements to reach the end of the maze with some kind of clever algorithm.


### API specs 

ENDPOINT: https://mazerunnerapi6.azurewebsites.net/api/
    
    ¡¡ IMPORTANT ¡¡
    for security reasons, all requests must have the parameter code at the end of the query, example:	
    https://mazerunnerapi6.azurewebsites.net/api/Maze?code=CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==
                                                    ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^


Step 1) Create a new Maze

[POST] https://mazerunnerapi6.azurewebsites.net/api/Maze

Body request example (json):
{
    "Width":15,
    "Height":15
}

Width and Height represents the size of the maze. The range accepted is 5-150

Response example:

{
    "MazeUid": "4e029a62-ec89-4ce3-a05f-19d553f5e000",
    "Height": 15,
    "Width": 15
}

The mazeUid is needed for the next step

Step 2) Create a new game

[POST] https://mazerunnerapi6.azurewebsites.net/api/Game/{mazeUid}

Body request example (json):

{
    "Operation":"Start"
}

Response example:

{
    "MazeUid": "4e029a62-ec89-4ce3-a05f-19d553f5e000",
    "GameUid": "101ada35-6b81-4ef3-be36-ee38ae754000",
    "Completed": false,
    "CurrentPositionX": 0,
    "CurrentPositionY": 0
}

With this call we are starting a new game inside the maze specified. You will nedd gameUid for the next step

Step 3) Take a look

[GET] https://mazerunnerapi6.azurewebsites.net/api/Game/{mazeUid}/{gameUid}

Response example:

{
    "Game": {
        "MazeUid": "4e029a62-ec89-4ce3-a05f-19d553f5ec80",
        "GameUid": "f203e09c-4603-46b5-8cd5-04b9aa3805c4",
        "Completed": false,
        "CurrentPositionX": 4,
        "CurrentPositionY": 0
    },
    "MazeBlockView": {
        "CoordX": 4,
        "CoordY": 0,
        "NorthBlocked": true,
        "SouthBlocked": true,
        "WestBlocked": false,
        "EastBlocked": false
    }
}

With this call, you can see:
- if the game is completed because you reach the end of the maze 
  (the end of the maze is always where the coord x and y are equal to the width-1 and height-1 of the created maze)
- your current coordenades (the game always starts at 0,0)
- If north, south, west  or east is blocked
  
Maze representation:  

|------------------|
|   x->            |    
| y 0,0         9,0|     
| |                |
| ^       north    |
|       west east  | 
|         south    |
|                  |
|                  |
|                  |
|  9,0         9,9 |         
|------------------|  
  note: if you go to the south -> y+1 
        if you go to the east  -> x+1
		

Step 4) Move next cell

[POST] https://mazerunnerapi6.azurewebsites.net/api/Game/{mazeUid}/{gameUid}

Body request example (json):

{
    "Operation":"GoEast"
}

Response example:

{
    "Game": {
        "MazeUid": "4e029a62-ec89-4ce3-a05f-19d553f5e000",
        "GameUid": "f203e09c-4603-46b5-8cd5-04b9aa380000",
        "Completed": false,
        "CurrentPositionX": 4,
        "CurrentPositionY": 0
    },
    "MazeBlockView": {
        "CoordX": 4,
        "CoordY": 0,
        "NorthBlocked": true,
        "SouthBlocked": true,
        "WestBlocked": false,
        "EastBlocked": false
    }
}

Operation can have the next values:
	"GoNorth"
    "GoSouth"
    "GoEast"
    "GoWest"
	
If you try to go to a blocked direction, an error will be returned, dont try it.

Step 5) Reset the game

[POST] https://mazerunnerapi6.azurewebsites.net/api/Game/{mazeUid}/{gameUid}

Body request example (json):

{
    Operation:"Start"
}

Extra step) See maze information for debuging purpouses (dont use it, dont be a cheater)
[GET] https://mazerunnerapi6.azurewebsites.net/api/Maze/mazeUid

Response example:

{
    "MazeUid": "4e029a62-ec89-4ce3-a05f-19d553f5ec80",
    "Width": 15,
    "Height": 15,
    "Blocks": [
        {
            "CoordX": 0,
            "CoordY": 0,
            "NorthBlocked": true,
            "SouthBlocked": false,
            "WestBlocked": true,
            "EastBlocked": false
        },
        {
            "CoordX": 1,
            "CoordY": 0,
            "NorthBlocked": true,
            "SouthBlocked": true,
            "WestBlocked": false,
            "EastBlocked": false
        }.......
		
		
		
EXAMPLE OF GAME SESSION
------------------------

// create a new random maze
[POST] https://mazerunnerapi6.azurewebsites.net/api/Maze?code=CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==
[BODY] 
{
    Width:100,
    Height:100
}
[RESPONSE]
{
    "MazeUid": "8d6ce136-655b-48b7-ba4e-bd7b01004c90",
    "Height": 100,
    "Width": 100
}

// create a new game using the new maze
[POST] https://mazerunnerapi6.azurewebsites.net/api/Game/4e029a62-ec89-4ce3-a05f-19d553f5ec80?code=CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==
[BODY]
{
    Operation:"Start"
}
[RESPONSE]
{
    "MazeUid": "8d6ce136-655b-48b7-ba4e-bd7b01004c90",
    "GameUid": "22a48db0-3296-4c0b-94f5-ca857d6ff26e",
    "Completed": false,
    "CurrentPositionX": 0,
    "CurrentPositionY": 0
}

// look around where you are
[GET] https://mazerunnerapi6.azurewebsites.net/api/Game/8d6ce136-655b-48b7-ba4e-bd7b01004c90/22a48db0-3296-4c0b-94f5-ca857d6ff26e?code=CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==
[RESPONSE]
{
    "Game": {
        "MazeUid": "8d6ce136-655b-48b7-ba4e-bd7b01004c90",
        "GameUid": "22a48db0-3296-4c0b-94f5-ca857d6ff26e",
        "Completed": false,
        "CurrentPositionX": 0,
        "CurrentPositionY": 0
    },
    "MazeBlockView": {
        "CoordX": 0,
        "CoordY": 0,
        "NorthBlocked": true,
        "SouthBlocked": false,
        "WestBlocked": true,
        "EastBlocked": false
    }
}

// because north and west are blocked, we can go to the south or the east, lest go east.
[POST] https://mazerunnerapi6.azurewebsites.net/api/Game/8d6ce136-655b-48b7-ba4e-bd7b01004c90/22a48db0-3296-4c0b-94f5-ca857d6ff26e?code=CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==
[BODY]
{
    Operation:"GoEast"
}
[RESPONSE]
{
    "Game": {
        "MazeUid": "8d6ce136-655b-48b7-ba4e-bd7b01004c90",
        "GameUid": "22a48db0-3296-4c0b-94f5-ca857d6ff26e",
        "Completed": false,
        "CurrentPositionX": 1,
        "CurrentPositionY": 0
    },
    "MazeBlockView": {
        "CoordX": 1,
        "CoordY": 0,
        "NorthBlocked": true,
        "SouthBlocked": false,
        "WestBlocked": false,
        "EastBlocked": false
    }
}

// try to "jump" the wall
[POST] https://mazerunnerapi6.azurewebsites.net/api/Game/8d6ce136-655b-48b7-ba4e-bd7b01004c90/22a48db0-3296-4c0b-94f5-ca857d6ff26e?code=CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==
[BODY]
{
    Operation:"GoNorth"
}
[RESPONSE]
¡¡¡ERROR¡¡¡

// go south
[POST] https://mazerunnerapi6.azurewebsites.net/api/Game/8d6ce136-655b-48b7-ba4e-bd7b01004c90/22a48db0-3296-4c0b-94f5-ca857d6ff26e?code=CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==
[BODY]
{
    Operation:"GoSouth"
}
[RESPONSE]
{
    "Game": {
        "MazeUid": "8d6ce136-655b-48b7-ba4e-bd7b01004c90",
        "GameUid": "22a48db0-3296-4c0b-94f5-ca857d6ff26e",
        "Completed": false,
        "CurrentPositionX": 1,
        "CurrentPositionY": 1
    },
    "MazeBlockView": {
        "CoordX": 1,
        "CoordY": 1,
        "NorthBlocked": false,
        "SouthBlocked": false,
        "WestBlocked": true,
        "EastBlocked": true
    }
}

// go south
[POST] https://mazerunnerapi6.azurewebsites.net/api/Game/8d6ce136-655b-48b7-ba4e-bd7b01004c90/22a48db0-3296-4c0b-94f5-ca857d6ff26e?code=CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==
[BODY]
{
    Operation:"GoSouth"
}
[RESPONSE]
{
    "Game": {
        "MazeUid": "8d6ce136-655b-48b7-ba4e-bd7b01004c90",
        "GameUid": "22a48db0-3296-4c0b-94f5-ca857d6ff26e",
        "Completed": false,
        "CurrentPositionX": 1,
        "CurrentPositionY": 2
    },
    "MazeBlockView": {
        "CoordX": 1,
        "CoordY": 2,
        "NorthBlocked": false,
        "SouthBlocked": true,
        "WestBlocked": false,
        "EastBlocked": false
    }
}
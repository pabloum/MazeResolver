using DTOs;
using Enums;
using Providers;

namespace MazeResolver;

public class PlayGame
{
    private readonly IMazeProvider _mazeProvider;
    private readonly IGameProvider _gameProvider;
    private readonly Dictionary<Operation, (int xIncrease, int yIncrease)> _steps = new Dictionary<Operation, (int XIncrease, int yIncrease)>
    {
        { Operation.GoNorth, (0,-1)},
        { Operation.GoSouth, (0,1)},
        { Operation.GoWest,  (-1,0)},
        { Operation.GoEast,  (1,0)},
    };

    private HashSet<(int x, int y)> _alreadySteppedMazeCoordinates = new HashSet<(int x, int y)>();
    
    public PlayGame(IMazeProvider mazeProvider, IGameProvider gameProvider)
    {
        _mazeProvider = mazeProvider;
        _gameProvider = gameProvider;
    }

    public async Task Play()
    {
        var maze = await CreateMaze();
        var game = await CreateGame(maze.MazeUid);
        var gameState = await TakeALook(game);
        
        Console.WriteLine($"You are in a maze of {maze.Width} x {maze.Height}");

        while (!game.Completed)
        {
            var wasMoveSuccesful = await MoveToNextCell(gameState);
            gameState = await TakeALook(game);
            InformUser(gameState, wasMoveSuccesful);
            UpdateGame(game, gameState);

            if (ShouldReset())
                await ResetGame(game);
            
            WaitForUser();
        }
    }

    private void WaitForUser()
    {
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    private void InformUser(GameLookDto gameLook, bool wasMoveSuccesful)
    {
        var blockSentences =  new Dictionary<bool, string>
        {   
            {true, "is blocked"},
            {false, "is not blocked"},
        };

        if (wasMoveSuccesful) 
        {
            Console.WriteLine("");
            Console.WriteLine("Move was succesful");
        }

        Console.WriteLine("");
        Console.WriteLine($"Your current position is X:{gameLook.Game.CurrentPositionX} x Y:{gameLook.Game.CurrentPositionY}");
        Console.WriteLine($"North {blockSentences[gameLook.MazeBlockView.NorthBlocked]}");
        Console.WriteLine($"South {blockSentences[gameLook.MazeBlockView.SouthBlocked]}");
        Console.WriteLine($"West {blockSentences[gameLook.MazeBlockView.WestBlocked]}");
        Console.WriteLine($"East {blockSentences[gameLook.MazeBlockView.EastBlocked]}");
        Console.WriteLine("");
    }

    private void DrawCurrentState(GameLookDto gameLook, MazeDto mazeDto)
    {
        for (int x = 0; x < mazeDto.Width; x++) 
        {
            for (int y = 0; y < mazeDto.Height; y++)
            {
                Console.Write($"");
            }
            Console.Write("\n");
        }
    }

    private void UpdateGame(GameDto game, GameLookDto gameLookDto)
    {
        game.CurrentPositionX = gameLookDto.MazeBlockView.CoordX;
        game.CurrentPositionY = gameLookDto.MazeBlockView.CoordY;
        game.Completed = gameLookDto.Game.Completed;
    }

    private async Task<MazeDto> CreateMaze()
    {
        var width = 25; // According to Instructions. We could ask this to the user 
        var height = 25; // According to Instructions. We could ask this to the user 
        var maze = await _mazeProvider.CreateMaze(width, height);
        return maze;
    } 

    private async Task<GameDto> CreateGame(Guid mazeUid)
    {
        var game = await _gameProvider.CreateNewGame(mazeUid, Operation.Start);
        return game;
    } 

    private async Task<bool> MoveToNextCell(GameLookDto currentState)
    {
        var direction = ChooseNextDirection(currentState);
        var game = await _gameProvider.MoveNextCell(currentState.Game.MazeUid, currentState.Game.GameUid, direction);

        return game != null &&
            (game.Game.CurrentPositionX != currentState.Game.CurrentPositionX ||  game.Game.CurrentPositionY != currentState.Game.CurrentPositionY);
    }

    private async Task<GameLookDto> TakeALook(GameDto currentState)
    {
        var gameDetails = await _gameProvider.TakeALook(currentState.MazeUid, currentState.GameUid);
        _alreadySteppedMazeCoordinates.Add((gameDetails.Game.CurrentPositionX, gameDetails.Game.CurrentPositionY));
        return gameDetails;
    }

    private async Task ResetGame(GameDto currentState)
    {
        await _gameProvider.ResetGame(currentState.MazeUid, currentState.GameUid, Operation.Start);
        _alreadySteppedMazeCoordinates.Clear();
    }

    private bool ShouldReset()
    {
        // Todo: implement logic here
        return false;
    }

    private Operation ChooseNextDirection(GameLookDto currentState)
    {
        var directions = new Dictionary<Operation, bool>()
        {
            {Operation.GoSouth, currentState.MazeBlockView.SouthBlocked},
            {Operation.GoWest, currentState.MazeBlockView.WestBlocked},
            {Operation.GoNorth, currentState.MazeBlockView.NorthBlocked},
            {Operation.GoEast, currentState.MazeBlockView.EastBlocked},
        };
        
        var possibleDirections = directions.Where(d => !d.Value).Select(d => d.Key);

        return BestMove(possibleDirections, currentState.Game);
    }

    /// <summary>
    /// Among all possible directions, check one by one if we have already been there
    /// </summary>
    /// <param name="possibleDirections"></param>
    /// <param name="game"></param>
    /// <returns></returns>
    private Operation BestMove(IEnumerable<Operation> possibleDirections, GameDto game)
    {
        foreach (var direction in possibleDirections)
        {
            var estimatedCoordX = game.CurrentPositionX + _steps[direction].xIncrease;
            var estimatedCoordy = game.CurrentPositionY + _steps[direction].yIncrease;

            if (!_alreadySteppedMazeCoordinates.Contains((estimatedCoordX, estimatedCoordy)))
            {
                return direction;
            }
        }

        return possibleDirections.First();
    }
}
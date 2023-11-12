using DTOs;
using Enums;
using Providers;

namespace MazeResolver;

public class PlayGame
{
    private readonly IMazeProvider _mazeProvider;
    private readonly IGameProvider _gameProvider;
    
    public PlayGame(IMazeProvider mazeProvider, IGameProvider gameProvider)
    {
        _mazeProvider = mazeProvider;
        _gameProvider = gameProvider;
    }

    public async Task Play()
    {
        var maze = await CreateMaze();
        var game = await CreateGame(maze.MazeUid);

        while (!game.Completed)
        {
            await MoveToNextCell(game);
            var gameState = await TakeALook(game);
            UpdateGame(game, gameState);

            if (ShouldReset())
                await ResetGame(game);
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

    private async Task<GameLookDto> MoveToNextCell(GameDto currentState)
    {
        var direction = ChooseNextDirection();
        var game = await _gameProvider.MoveNextCell(currentState.MazeUid, currentState.GameUid, direction);
        return game;
    }

    private async Task<GameLookDto> TakeALook(GameDto currentState)
    {
        var gameDetails = await _gameProvider.TakeALook(currentState.MazeUid, currentState.GameUid);
        return gameDetails;
    }

    private async Task ResetGame(GameDto currentState)
    {
        await _gameProvider.ResetGame(currentState.MazeUid, currentState.GameUid, Operation.Start);
    }

    private bool ShouldReset()
    {
        // TODO: Create logic for deciding this
        return false;
    }

    private Operation ChooseNextDirection()
    {
        // TODO: Create logic for decding this
        return Operation.GoNorth;
    }
}
using DTOs;
using Enums;
using MazeResolver;

namespace Providers;

public class GameProvider : IGameProvider
{
    private readonly IRequestHandler _requestHandler;

    public GameProvider(IRequestHandler requestHandler)
    {
        _requestHandler = requestHandler;
    }

    public async Task<GameDto> CreateNewGame(Guid mazeUuid, Operation operation)
    {
        var url = $"Game/{mazeUuid}";
        var payload = $"{{\"Operation\": \"{operation}\"}}";
        var game = await _requestHandler.Post<GameDto>(url, payload);

        return game;
    }

    public async Task<GameLookDto> TakeALook(Guid mazeUuid, Guid gameUuid) 
    {
        var url = $"Game/{mazeUuid}/{gameUuid}";
        var game = await _requestHandler.Get<GameLookDto>(url);

        return game;
    }

    public async Task<GameLookDto> MoveNextCell(Guid mazeUuid, Guid gameUuid, Operation operation)
    {
        var url = $"Game/{mazeUuid}/{gameUuid}";
        var payload = $"{{\"Operation\": \"{operation}\"}}";
        var game = await _requestHandler.Post<GameLookDto>(url, payload);
        return game;
    }

    public async Task ResetGame(Guid mazeUuid, Guid gameUuid, Operation operation)
    {
        var url = $"Game/{mazeUuid}/{gameUuid}";
        var payload = $"{{\"Operation\": \"{operation}\"}}";
        await _requestHandler.Post<GameDto>(url, payload);
    }
}
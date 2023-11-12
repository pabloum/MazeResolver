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
        var url = $"/Game/{mazeUuid}";
        var payload = $"\"Operation\": \"{operation}\"";

        var game = await _requestHandler.Post<GameDto>(url, payload);

        return game;
    }

    public async Task<GameDto> TakeALook(Guid mazeUuid, Guid gameUuid) 
    {
        var url = $"/Game/{mazeUuid}/{gameUuid}";
        var game = await _requestHandler.Get<GameDto>(url);

        return game;
    }

    public async Task<GameDto> MoveNextCell(Guid mazeUuid, Guid gameUuid, Operation operation)
    {
        var url = $"/Game/{mazeUuid}/{gameUuid}";
        var payload = $"\"Operation\": \"{operation}\"";

        var game = await _requestHandler.Post<GameDto>(url, payload);

        return game;
    }

    public async Task<GameDto> ResetGame(Guid mazeUuid, Guid gameUuid, Operation operation)
    {
        var url = $"/Game/{mazeUuid}/{gameUuid}";
        var payload = $"\"Operation\": \"{operation}\"";

        var game = await _requestHandler.Post<GameDto>(url, payload);

        return game;
    }
}
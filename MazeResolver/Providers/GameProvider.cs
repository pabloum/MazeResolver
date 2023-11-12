using DTOs;
using MazeResolver;

namespace Providers;

public class GameProvider : IGameProvider
{
    private readonly IRequestHandler _requestHandler;

    public GameProvider(IRequestHandler requestHandler)
    {
        _requestHandler = requestHandler;
    }

    public async Task<GameDto> CreateNewGame(Guid mazeUuid)
    {
        var url = "";
        var payload = "";

        var game = await _requestHandler.Post<GameDto>(url, payload);

        return game;
    }

    public async Task<GameDto> TakeALook(Guid mazeUuid, Guid gameUuid) 
    {
        var url = "";
        var game = await _requestHandler.Get<GameDto>(url);

        return game;
    }

    public async Task<GameDto> MoveNextCell(Guid mazeUuid, Guid gameUuid)
    {
        var url = "";
        var payload = "";

        var game = await _requestHandler.Post<GameDto>(url, payload);

        return game;
    }

    public async Task<GameDto> ResetGame(Guid mazeUuid, Guid gameUuid)
    {
        var url = "";
        var payload = "";

        var game = await _requestHandler.Post<GameDto>(url, payload);

        return game;
    }
}
using DTOs;
using MazeResolver;

namespace Providers;

public class MazeProvider : IMazeProvider
{
    private readonly IRequestHandler _requestHandler;

    public MazeProvider(IRequestHandler requestHandler)
    {
        _requestHandler = requestHandler;
    }

    public async Task<MazeDto> CreateMaze()
    {
        var url = "";
        var payload = "";

        var game = await _requestHandler.Post<MazeDto>(url, payload);

        return game;
    }

    public async Task<MazeDetailsDto> SeeMaze(Guid mazeUuid)
    {
        var url = "";
        var payload = "";

        var game = await _requestHandler.Post<MazeDetailsDto>(url, payload);

        return game;
    }
}
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

    public async Task<MazeDto> CreateMaze(int width, int height)
    {
        var url = "Maze";
        var payload = $"{{\"Width\": {width}, \"Height\": {height}}}";

        var game = await _requestHandler.Post<MazeDto>(url, payload);

        return game;
    }

    public async Task<MazeDetailsDto> SeeMaze(Guid mazeUuid)
    {
        var url = @"Maze/{mazeUuid}";
        var game = await _requestHandler.Get<MazeDetailsDto>(url);
        return game;
    }
}
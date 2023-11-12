using DTOs;

namespace Providers;

public interface IMazeProvider
{
    Task<MazeDto> CreateMaze(int width, int height);
    Task<MazeDetailsDto> SeeMaze(Guid mazeUuid);
}
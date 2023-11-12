using DTOs;

namespace Providers;

public interface IMazeProvider
{
    Task<MazeDto> CreateMaze();
    Task<MazeDetailsDto> SeeMaze(Guid mazeUuid);
}
using DTOs;

namespace Providers;

public interface IGameProvider
{
    Task<GameDto> CreateNewGame(Guid mazeUuid);
    Task<GameDto> TakeALook(Guid mazeUuid, Guid gameUuid);
    Task<GameDto> MoveNextCell(Guid mazeUuid, Guid gameUuid);
    Task<GameDto> ResetGame(Guid mazeUuid, Guid gameUuid);
}
using DTOs;
using Enums;

namespace Providers;

public interface IGameProvider
{
    Task<GameDto> CreateNewGame(Guid mazeUuid, Operation operation);
    Task<GameDto> TakeALook(Guid mazeUuid, Guid gameUuid);
    Task<GameDto> MoveNextCell(Guid mazeUuid, Guid gameUuid, Operation operation);
    Task<GameDto> ResetGame(Guid mazeUuid, Guid gameUuid, Operation operation);
}
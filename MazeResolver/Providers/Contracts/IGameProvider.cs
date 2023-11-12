using DTOs;
using Enums;

namespace Providers;

public interface IGameProvider
{
    Task<GameDto> CreateNewGame(Guid mazeUuid, Operation operation);
    Task<GameLookDto> TakeALook(Guid mazeUuid, Guid gameUuid);
    Task<GameLookDto> MoveNextCell(Guid mazeUuid, Guid gameUuid, Operation operation);
    Task ResetGame(Guid mazeUuid, Guid gameUuid, Operation operation);
}
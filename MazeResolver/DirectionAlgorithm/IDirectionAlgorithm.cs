using DTOs;
using Enums;

namespace MazeResolver.DirectionsFinder;

public interface IDirectionAlgorithm
{
    Operation ChooseDirection(IEnumerable<Operation> possibleDirections, GameDto game, HashSet<(int x, int y)> alreadySteppedMazeCoordinates);
}

    

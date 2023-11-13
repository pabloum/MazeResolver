using DTOs;
using Enums;

namespace MazeResolver.DirectionsFinder;

public class RecursiveAlgorithm : IDirectionAlgorithm
{
    public Operation ChooseDirection(IEnumerable<Operation> possibleDirections, GameDto game, HashSet<(int x, int y)> alreadySteppedMazeCoordinates)
    {
        throw new NotImplementedException();
    }
}
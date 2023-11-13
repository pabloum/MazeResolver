using DTOs;
using Enums;

namespace MazeResolver.DirectionsFinder;

public static class MoveCalculations
{
    public static readonly  Dictionary<Operation, (int xIncrease, int yIncrease)>  _steps = new Dictionary<Operation, (int XIncrease, int yIncrease)>
    {
        { Operation.GoNorth, (0,-1)},
        { Operation.GoSouth, (0,1)},
        { Operation.GoWest,  (-1,0)},
        { Operation.GoEast,  (1,0)},
    };
}

public class DirectionAlgorithm : IDirectionAlgorithm
{

    public Operation ChooseDirection(IEnumerable<Operation> possibleDirections, GameDto game, HashSet<(int x, int y)> alreadySteppedMazeCoordinates)
    {
        foreach (var direction in possibleDirections)
        {
            var estimatedCoordX = game.CurrentPositionX + MoveCalculations._steps[direction].xIncrease;
            var estimatedCoordy = game.CurrentPositionY + MoveCalculations._steps[direction].yIncrease;

            if (!alreadySteppedMazeCoordinates.Contains((estimatedCoordX, estimatedCoordy)))
            {
                return direction;
            }
        }

         return possibleDirections.First();
    }
}
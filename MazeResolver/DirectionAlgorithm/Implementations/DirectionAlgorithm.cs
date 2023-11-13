using DTOs;
using Enums;

namespace MazeResolver.DirectionsFinder;

public class DirectionAlgorithm : IDirectionAlgorithm
{

    public Operation ChooseDirection(IEnumerable<Operation> possibleDirections, GameDto game, HashSet<(int x, int y)> alreadySteppedMazeCoordinates)
    {
        // NOTE:  This algorithm does NOT reach the end of the maze yet.
        
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
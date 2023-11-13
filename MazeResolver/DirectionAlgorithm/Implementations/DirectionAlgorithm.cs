using DTOs;
using Enums;

namespace MazeResolver.DirectionsFinder;

public class DirectionAlgorithm : IDirectionAlgorithm
{
    private Stack<(int x, int y)> _path = new Stack<(int x, int y)>();

    public Operation ChooseDirection(IEnumerable<Operation> possibleDirections, GameDto game, HashSet<(int x, int y)> alreadySteppedMazeCoordinates)
    {
        if (game.CurrentPositionX == 0 && game.CurrentPositionY == 0)
        {
            _path.Clear();
            _path.Push((0, 0));
        }

        foreach (var direction in possibleDirections)
        {
            var estimatedCoordX = game.CurrentPositionX + MoveCalculations._steps[direction].xIncrease;
            var estimatedCoordy = game.CurrentPositionY + MoveCalculations._steps[direction].yIncrease;

            if (!alreadySteppedMazeCoordinates.Contains((estimatedCoordX, estimatedCoordy)))
            {
                _path.Push((estimatedCoordX, estimatedCoordy));
                return direction;
            }
        }

        if (_path.Count < 1)
        {
            return possibleDirections.First();
        }

        return ReturnToPreviousPosition();
    }

    private Operation ReturnToPreviousPosition()
    {
        var currentPosition = _path.Pop();
        var previousPosition = _path.First();

        var diffX = previousPosition.x - currentPosition.x;
        var diffY = previousPosition.y - currentPosition.y;

        var operation = MoveCalculations._steps.Where(s => s.Value == (diffX, diffY)).Select(s => s.Key).FirstOrDefault();

        if (operation == Operation.Start)
        {
            _path.Clear();
        }

        return operation;
    }
}
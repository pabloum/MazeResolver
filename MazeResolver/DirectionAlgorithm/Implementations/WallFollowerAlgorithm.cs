using DTOs;
using Enums;

namespace MazeResolver.DirectionsFinder;

public class WallFollowerAlgorithm : IDirectionAlgorithm
{
    private Operation _facingTowards;

    private readonly Dictionary<Operation, Operation> _whereToTurn = new Dictionary<Operation, Operation>()
    {
        {Operation.GoEast, Operation.GoSouth},
        {Operation.GoSouth, Operation.GoWest},
        {Operation.GoWest, Operation.GoNorth},
        {Operation.GoNorth, Operation.GoEast},
    };

    private readonly Dictionary<Operation, Operation> _faceBackwards = new Dictionary<Operation, Operation>()
    {
        {Operation.GoNorth, Operation.GoSouth},
        {Operation.GoSouth, Operation.GoNorth},
        {Operation.GoEast, Operation.GoWest},
        {Operation.GoWest, Operation.GoEast},
    };

    public Operation ChooseDirection(IEnumerable<Operation> possibleDirections, GameDto game, HashSet<(int x, int y)> alreadySteppedMazeCoordinates)
    {
        if (game.CurrentPositionX == 0 && game.CurrentPositionY == 0 && alreadySteppedMazeCoordinates.Count <= 1)
        {
            _facingTowards = Operation.GoEast;
        }

        if (possibleDirections.Any())
        {
            return FindLeftTurn(possibleDirections);
        }

        return possibleDirections.First();
    }

    private Operation FindLeftTurn(IEnumerable<Operation> possibleDirections)
    {
        while (true)
        {
            if (possibleDirections.Contains(_whereToTurn[_facingTowards]))
            {
                var direction = _whereToTurn[_facingTowards];
                _facingTowards = direction;
                return direction;
            }
            else if (possibleDirections.Contains(_facingTowards))
            {
                return _facingTowards;
            }
            else
            {
                _facingTowards = _faceBackwards[_whereToTurn[_facingTowards]];
            }
        }
    }
}
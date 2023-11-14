using DTOs;
using Enums;

namespace MazeResolver.DirectionsFinder;

public class WallFollowerAlgorithm : IDirectionAlgorithm
{
    private Operation _facingTowards;

    private Dictionary<Operation, Operation> _whereToTurn = new Dictionary<Operation, Operation>()
    {
        {Operation.GoEast, Operation.GoSouth},
        {Operation.GoSouth, Operation.GoWest},
        {Operation.GoWest, Operation.GoNorth},
        {Operation.GoNorth, Operation.GoEast},
    };

    public Operation ChooseDirection(IEnumerable<Operation> possibleDirections, GameDto game, HashSet<(int x, int y)> alreadySteppedMazeCoordinates)
    {
        if (game.CurrentPositionX == 0 && game.CurrentPositionY == 0)
        {
            _facingTowards = Operation.GoEast;
        }

        if (possibleDirections.Any())
        {
            return FindLeftTurn(possibleDirections);
        }

        return Operation.Start;
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
                _facingTowards = ContratyDirection(_whereToTurn[_facingTowards]);
            }
        }
    }

    private Operation ContratyDirection(Operation operation)
    {
        switch (operation)
        {
            case Operation.GoEast:
                return Operation.GoWest;
            case Operation.GoSouth:
                return Operation.GoNorth;
            case Operation.GoWest:
                return Operation.GoEast;
            case Operation.GoNorth:
                return Operation.GoSouth;
            default:
                return Operation.Start;
        }
    }
}
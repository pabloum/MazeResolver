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
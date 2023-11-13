using Enums;

namespace MazeResolver.DirectionsFinder;

public interface IAlgorithmFactory
{
    IDirectionAlgorithm GetAlgorithm(TypeOfAlgorithm typeOfAlgorithm);
}

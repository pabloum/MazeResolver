using Enums;

namespace MazeResolver.DirectionsFinder;

public class AlgorithmFactory : IAlgorithmFactory
{
    private readonly Dictionary<TypeOfAlgorithm, Type> _algorithmImplementation = new Dictionary<TypeOfAlgorithm, Type>
    {
        { TypeOfAlgorithm.TremauxAlgorithm, typeof(TremauxAlgorithm) },
        { TypeOfAlgorithm.DeadEndAlgorithm, typeof(DeadEndAlgorithm) },
        { TypeOfAlgorithm.DirectionAlgorithm, typeof(DirectionAlgorithm) },
        { TypeOfAlgorithm.RecursiveAlgorithm, typeof(RecursiveAlgorithm) },
        { TypeOfAlgorithm.WallFollowerAlgorithm, typeof(WallFollowerAlgorithm) },
    };

    public IDirectionAlgorithm GetAlgorithm(TypeOfAlgorithm typeOfAlgorithm)
    {
        return (IDirectionAlgorithm)Activator.CreateInstance(_algorithmImplementation[typeOfAlgorithm]);
    }
}
using Enums;

namespace MazeResolver.DirectionsFinder;

public class AlgorithmFactory : IAlgorithmFactory
{
    private IEnumerable<IDirectionAlgorithm> _algos;

    public AlgorithmFactory(IEnumerable<IDirectionAlgorithm> algos)
    {
        _algos = algos;
    }

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
        return _algos.FirstOrDefault(a => a.GetType() == _algorithmImplementation[typeOfAlgorithm]) ?? throw new Exception("No implementation available");
    }
}
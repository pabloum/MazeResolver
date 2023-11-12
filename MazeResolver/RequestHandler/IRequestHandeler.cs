

namespace MazeResolver;

public interface IRequestHandler 
{
    Task<T> Get<T>(string url);
    Task<T> Post<T>(string url, string payload);
}
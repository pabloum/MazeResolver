using MazeResolver.DirectionsFinder;
using Providers;

namespace MazeResolver;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("This is the Maze Resolver Challenge!");

        var serviceProvider = SetUpDependencyInjection();

        var mazeProvider = serviceProvider.GetRequiredService<IMazeProvider>();
        var gameProvider = serviceProvider.GetRequiredService<IGameProvider>();
        var algorithmFactory = serviceProvider.GetRequiredService<IAlgorithmFactory>();

        var playGame = new PlayGame(mazeProvider, gameProvider, algorithmFactory);
        await playGame.Play();

        Console.WriteLine("Press any key to exit.");
    }

    private static ServiceProvider SetUpDependencyInjection()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddHttpClient()
            .AddTransient<IRequestHandler, RequestHandler>() 
            .AddTransient<IMazeProvider, MazeProvider>() 
            .AddTransient<IGameProvider, GameProvider>() 
            .AddScoped<IAlgorithmFactory, AlgorithmFactory>() 
            .BuildServiceProvider();

        return serviceProvider;
    }
}
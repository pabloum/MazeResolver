using System;
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

        var playGame = new PlayGame(mazeProvider, gameProvider);
        await playGame.Play();

        Console.WriteLine("Press any key to exit.");
    }

    private static ServiceProvider SetUpDependencyInjection()
    {
        var serviceProvider = new ServiceCollection()
            .AddTransient<IRequestHandler, RequestHandler>() 
            .AddTransient<IMazeProvider, MazeProvider>() 
            .AddTransient<IGameProvider, GameProvider>() 
            .BuildServiceProvider();

        return serviceProvider;
    }
}
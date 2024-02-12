using Autofac;
using Sibur.BuggyBot.App;
using Sibur.BuggyBot.Di;

class Program
{
    static async Task Main(string[] args)
    {
        var container = new DI().Build();

        var app = container.Resolve<IApp>();
        await app.RunAsync();
    }
}
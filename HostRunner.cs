using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Repro33612;

public sealed class HostRunner(EfQueryRunner ef, ILogger<HostRunner> logger)
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public Task RunAsync(IHost app)
    {
        Console.CancelKeyPress += (_, _) => { _cancellationTokenSource.Cancel(); };

        logger.LogInformation("Press R to run the query.");

        var cancellationToken = _cancellationTokenSource.Token;
        return Task.WhenAny(app.RunAsync(cancellationToken), KeyListenerAsync(cancellationToken));
    }

    private async Task KeyListenerAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.R)
                        await ef.RunAsync(cancellationToken);
                }
                await Task.Delay(100, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled Exception");
        }
    }
}

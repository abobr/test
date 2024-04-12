using Domain.Interface;

namespace UserTaskApi.Services
{
    class PeriodicHostedService(ILogger<PeriodicHostedService> logger, IServiceScopeFactory factory) : BackgroundService
    {
#if DEBUG
        private readonly TimeSpan _period = TimeSpan.FromSeconds(8);
#else
        private readonly TimeSpan _period = TimeSpan.FromMinutes(2);
#endif
        private int _executionCount = 0;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new(_period);

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    await using AsyncServiceScope asyncScope = factory.CreateAsyncScope();
                    var service = asyncScope.ServiceProvider.GetRequiredService<ITaskService>();
                    await service.ReassignTasksAsync(stoppingToken);

                    _executionCount++;
                    logger.LogInformation($"Executed PeriodicHostedService - Count: {_executionCount}");
                }
                catch (Exception ex)
                {
                    logger.LogInformation($"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
                }
            }
        }
    }
}

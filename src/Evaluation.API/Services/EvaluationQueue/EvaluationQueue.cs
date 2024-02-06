using AutomaticInterviewEvaluationSystem.Evaluation.API.Services.EvaluationQueue.Abstractions;
using Evaluation.API.Services.EvaluationQueue.Abstractions;
using Microsoft.Extensions.Options;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Services.EvaluationQueue
{
    public class EvaluationQueue(
        ILogger<EvaluationQueue> logger,
        IOptions<EvaluationQueueOptions> options,
        IServiceProvider serviceProvider) : IEvaluationQueue
    {
        private readonly EvaluationQueueOptions _queueInfo = options.Value;

        ///TODO: async integration execution...
        public async Task ExecuteQueue(EvaluationUnitOfWork unitOfWork) 
        {
            try
            {
                await using var scope = serviceProvider.CreateAsyncScope();

                if (logger.IsEnabled(LogLevel.Trace))
                {
                    logger.LogTrace("Processing UnitOfWork: \\\"{Type}\\\"", unitOfWork.GetType());
                }

                await ProcessUpdates(unitOfWork, scope);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error Processing UnitOfWork \\\"{Type}\\\"", unitOfWork.GetType());
            }   
        }

        private async Task ProcessUpdates(EvaluationUnitOfWork unitOfWork, IServiceScope scope)
        {
            foreach (var (_, type) in _queueInfo.EvaluationUnitTypes)
            {
                if (logger.IsEnabled(LogLevel.Trace))
                {
                    logger.LogTrace("Processing EvaluationUnit: {Type}", type);
                }

                var unit = (IEvaluationUnit)scope.ServiceProvider.GetRequiredService(type);

                await unit.Execute(unitOfWork);
            }
        }
    }
}

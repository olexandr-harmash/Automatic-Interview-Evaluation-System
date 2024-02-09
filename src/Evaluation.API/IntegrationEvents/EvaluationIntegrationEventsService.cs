using AutomaticInterviewEvaluationSystem.EventBus.Abstractions;
using AutomaticInterviewEvaluationSystem.EventBus.Events;
using Microsoft.EntityFrameworkCore;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.IntegrationEvents
{
    public class EvaluationIntegrationEventService(ILogger<EvaluationIntegrationEventService> logger,
    IEventBus eventBus,
    EvaluationContext evaluationContext)
        : IEvaluationIntegrationEventService, IDisposable
    {
        /// TODO: dispose method...
        public void Dispose()
        {
            
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent @event)
        {
            try
            {
                logger.LogInformation("Publishing integration event: {IntegrationEventId_published} - ({@IntegrationEvent})", @event.Id, @event);

                await eventBus.PublishAsync(@event);
            } catch (Exception ex)
            {
                logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
            }
        }

        public async Task SaveEvaluationContextChangesAsync(IntegrationEvent @event)
        {
            logger.LogInformation("CatalogIntegrationEventService - Saving changes for integrationEvent: {IntegrationEventId}", @event.Id);

            var strategy = evaluationContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await evaluationContext.SaveChangesAsync();
            });
        }
    }
}

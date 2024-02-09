using AutomaticInterviewEvaluationSystem.EventBus.Events;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.IntegrationEvents
{
    public interface IEvaluationIntegrationEventService
    {
        Task SaveEvaluationContextChangesAsync(IntegrationEvent @event);
        Task PublishThroughEventBusAsync(IntegrationEvent @event);
    }
}

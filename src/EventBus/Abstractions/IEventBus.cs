namespace AutomaticInterviewEvaluationSystem.EventBus.Abstractions
{
    public interface IEventBus
    {
        Task PublishAsync(IntegrationEvent @event);
    }
}

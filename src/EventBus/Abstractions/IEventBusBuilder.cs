using Microsoft.Extensions.DependencyInjection;

namespace AutomaticInterviewEvaluationSystem.EventBus.Abstractions
{
    public interface IEventBusBuilder
    {
        public IServiceCollection Services { get; }
    }
}

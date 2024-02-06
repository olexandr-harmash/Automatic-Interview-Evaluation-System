using AutomaticInterviewEvaluationSystem.Evaluation.API.Services.EvaluationQueue.Abstractions;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Models
{
    public class EvaluationServices(
    EvaluationContext context,
    ILogger<EvaluationServices> logger,
    IEvaluationIntegrationEventService eventService,
    IEvaluationQueue evaluationQueueService)
    {
        public EvaluationContext Context { get; } = context;
        public ILogger<EvaluationServices> Logger { get; } = logger;
        public IEvaluationIntegrationEventService EventService { get; } = eventService;
        public IEvaluationQueue evaluationQueueService { get; } = evaluationQueueService;
    };
}

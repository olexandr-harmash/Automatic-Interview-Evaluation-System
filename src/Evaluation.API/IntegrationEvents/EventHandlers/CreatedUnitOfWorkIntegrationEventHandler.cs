using AutomaticInterviewEvaluationSystem.Evaluation.API.Services.EvaluationQueue.Abstractions;
using AutomaticInterviewEvaluationSystem.EventBus.Events;
using Microsoft.EntityFrameworkCore;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.IntegrationEvents.EventHandlers
{
    public class CreatedUnitOfWorkIntegrationEventHandler(
    IEvaluationIntegrationEventService evaluationIntegrationEventService,
    IEvaluationQueue evaluationQueueService,
    ILogger<CreatedUnitOfWorkIntegrationEventHandler> logger,
    EvaluationContext context) :
    IIntegrationEventHandler<CreatedUnitOfWorkIntegrationEvent>
    {
        public async Task Handle(CreatedUnitOfWorkIntegrationEvent @event)
        {
            logger.LogInformation("Handling event with Id: {0}", @event.Id);

            var unitOfWork = context.EvaluationUnitOfWorks
                .Include(u => u.Requirements)
                .FirstOrDefault(u => u.Id == @event.UnitOfWorkId);

            ///TODO: Evaluation queue...
            await evaluationQueueService.ExecuteQueue(unitOfWork);

            context.EvaluationRequirements.Update(unitOfWork.Requirements);

            await evaluationIntegrationEventService.SaveEvaluationContextChangesAsync(@event);
        }
    }
}

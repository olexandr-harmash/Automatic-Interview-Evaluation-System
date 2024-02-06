using AutomaticInterviewEvaluationSystem.EventBus.Events;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.IntegrationEvents.Events
{
    public record CreatedUnitOfWorkIntegrationEvent : IntegrationEvent
    {
        public int UnitOfWorkId { get; set; }

        public CreatedUnitOfWorkIntegrationEvent(int unitOfWorkId)
        {
            UnitOfWorkId = unitOfWorkId;
        }
    }
}

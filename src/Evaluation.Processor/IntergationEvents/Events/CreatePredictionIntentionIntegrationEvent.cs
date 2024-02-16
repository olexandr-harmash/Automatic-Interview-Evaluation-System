using AutomaticInterviewEvaluationSystem.EventBus.Events;
using System.Collections.Generic;

namespace Evaluation.Processor.IntergationEvents.Events
{
    public record CreatePredictionIntentionIntegrationEvent(
        int customerId,
        int positionId,
        int unitOfWorkId,
        Dictionary<string, double> requirements)
        : IntegrationEvent
    {
        public int UnitOfWorkId { get; set; } = unitOfWorkId;
        public int CustomerId { get; set; } = customerId;
        public int PositionId { get; set; } = positionId;
        public Dictionary<string, double> Scores { get; set; } = requirements;
    }
}

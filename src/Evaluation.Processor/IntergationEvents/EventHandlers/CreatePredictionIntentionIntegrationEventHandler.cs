using AutomaticInterviewEvaluationSystem.EventBus.Abstractions;
using Evaluation.Processor.IntergationEvents.Events;
using Evaluation.Processor.Models;
using Evaluation.Processor.Services.AI;

namespace Evaluation.Processor.IntergationEvents.EventHandlers
{
    public class CreatePredictionIntentionIntegrationEventHandler : IIntegrationEventHandler<CreatePredictionIntentionIntegrationEvent>
    {
        private readonly HopfieldProcessor _processor;
        private readonly IRepository _predictionRepository;
        private readonly ILogger<CreatePredictionIntentionIntegrationEventHandler> _logger;

        public CreatePredictionIntentionIntegrationEventHandler(
            HopfieldProcessor processor,
            IRepository predictionRepository,
            ILogger<CreatePredictionIntentionIntegrationEventHandler> logger)
        {
            _processor = processor;
            _predictionRepository = predictionRepository;
            _logger = logger;
        }

        public async Task Handle(CreatePredictionIntentionIntegrationEvent @event)
        {
            var prediction = new Prediction(
                positionId: @event.PositionId,
                customerId: @event.CustomerId,
                unitOfWorkId: @event.UnitOfWorkId,
                requirements: @event.Scores);

            try
            {
                await _processor.Predict(prediction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not predict result for event {0}", @event.Id);
                // TODO: Create failure event...
            }
            finally
            {
                await _predictionRepository.SetPredictionAsync(prediction);
            }
        }
    }
}

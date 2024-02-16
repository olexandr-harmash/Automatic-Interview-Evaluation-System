using Evaluation.Processor.Models;
using Evaluation.Processor.Services.EvaluationProcessor.Abstractions;

namespace Evaluation.Processor.Services.AI
{
    public class HopfieldProcessor
    {
        private readonly INetwork _network;

        private enum PredictionStatuses
        {
            PERFECT,
            FAILURE,
            AVERAGE,
            TERRIBLE
        }

        public HopfieldProcessor(INetwork network)
        {
            _network = network;
        }

        public Task Predict(Prediction prediction)
        {
            try
            {
                var output = _network.Predict(prediction.GetScoreArray());
                prediction.Status = PredictionStatuses.PERFECT.ToString();
            }
            catch (Exception ex)
            {
                prediction.Status = PredictionStatuses.FAILURE.ToString();
                throw ex;
            }

            return Task.CompletedTask;
        }
    }
}

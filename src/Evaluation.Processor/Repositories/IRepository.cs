using Evaluation.Processor.Models;

namespace Evaluation.Processor.Repositories
{
    public interface IRepository
    {
        Task<Prediction> GetPredictionAsync(int unitOfWorkId);

        Task<Prediction> SetPredictionAsync(Prediction prediction);
    }
}

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Services.EvaluationQueue.Abstractions
{
    public interface IEvaluationQueue
    {
        public Task ExecuteQueue(EvaluationUnitOfWork unitOfWork);
    }
}

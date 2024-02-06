namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Services.EvaluationQueue.Abstractions
{
    public interface IEvaluationUnit
    {
        public Task Execute(EvaluationUnitOfWork unitOfWork);
    }
}

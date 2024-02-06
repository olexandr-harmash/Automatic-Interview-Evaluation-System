using AutomaticInterviewEvaluationSystem.Evaluation.API.Services.EvaluationQueue.Abstractions;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Infrastructure.Integrations.BasicProgrammingSkillsEvaluationUnit
{
    public class BasicProgrammingSkillsEvaluationUnit : IEvaluationUnit
    {
        public Task Execute(EvaluationUnitOfWork unitOfWork)
        {
            unitOfWork.EvaluateBasicProgrammingSkills(new Random().Next(-1, 1));

            return Task.CompletedTask;
        }
    }
}

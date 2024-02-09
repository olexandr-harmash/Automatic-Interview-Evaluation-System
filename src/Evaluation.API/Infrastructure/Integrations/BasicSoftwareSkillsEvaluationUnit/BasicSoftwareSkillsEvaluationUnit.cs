using AutomaticInterviewEvaluationSystem.Evaluation.API.Services.EvaluationQueue.Abstractions;

namespace Evaluation.API.Infrastructure.Integrations.BasicSoftwareSkillsEvaluationUnit
{
    public class BasicSoftwareSkillsEvaluationUnit : IEvaluationUnit
    {
        public Task Execute(EvaluationUnitOfWork unitOfWork)
        {
            unitOfWork.EvaluateBasicSoftwareSkills(new Random().Next(-1,1));

            return Task.CompletedTask;
        }
    }
}

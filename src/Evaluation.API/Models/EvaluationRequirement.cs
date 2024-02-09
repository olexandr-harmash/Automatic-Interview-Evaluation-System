using System.Text.Json.Serialization;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Models
{
    public class EvaluationRequirement
    {
        public int Id { get; set; }
        public int BasicProgramingSkills { get; set; } = 0;
        public int SoftwarePatternsKnowledge { get; set; } = 0;
        public int UnitOfWorkId { get; set; }

        [JsonIgnore]
        public EvaluationUnitOfWork EvaluationUnitOfWork { get; set; }
        public EvaluationRequirement() { }
    }
}

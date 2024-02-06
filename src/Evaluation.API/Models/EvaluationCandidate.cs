using System.Text.Json.Serialization;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Models
{
    public class EvaluationCandidate
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public ICollection<EvaluationUnitOfWork> UnitOfWorks { get; set; }

        public void AddUnitOfWork(EvaluationUnitOfWork unitOfWork)
        {
            UnitOfWorks.Add(unitOfWork);
        }

        public int PositionId { get; set; }

        [JsonIgnore]
        public EvaluationPosition EvaluationPosition { get; set; }

        public EvaluationCandidate() { }
    }
}

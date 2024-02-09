namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Models
{
    public class EvaluationPosition
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public ICollection<EvaluationCandidate> Candidates { get; set; }

        public void AddCandidate(EvaluationCandidate candidate)
        {
            Candidates.Add(candidate);
        }

        public EvaluationPosition() { }
    }
}

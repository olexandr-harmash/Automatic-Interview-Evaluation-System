using Microsoft.AspNetCore.Routing.Matching;
using System.Text.Json.Serialization;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Models
{
    public class EvaluationUnitOfWork
    {
        public int Id { get; set; }
        public EvaluationRequirement Requirements { get; set; }
        public int CandidateId { get; set; }

        [JsonIgnore]
        public EvaluationCandidate EvaluationCandidate { get; set; }

        public EvaluationUnitOfWork() { }

        public void EvaluateBasicSoftwareSkills(int score)
        {
            Requirements.SoftwarePatternsKnowledge = score;
        }
        public void EvaluateBasicProgrammingSkills(int score)
        {
            Requirements.BasicProgramingSkills = score;
        }
    }
}

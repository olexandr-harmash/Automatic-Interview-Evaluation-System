using System.Text.Json.Serialization;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Models
{
    public class EvaluationPositionEntiresResult
    {
        [JsonInclude]
        public int PositionId;

        [JsonInclude]
        public IEnumerable<EvaluationUnitOfWork> UnitOfWorks;

        public EvaluationPositionEntiresResult(int positionId, IEnumerable<EvaluationUnitOfWork> unitOfWork)
        {
            PositionId = positionId;
            UnitOfWorks = unitOfWork;
        }
    }
}

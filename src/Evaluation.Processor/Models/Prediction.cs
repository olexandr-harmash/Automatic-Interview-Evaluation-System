using System.Collections.Generic;
using System.Linq;

namespace Evaluation.Processor.Models
{
    public class Prediction
    {
        public int PositionId { get; set; }
        public int CustomerId { get; set; }
        public int UnitOfWorkId { get; set; }
        public Dictionary<string, double> Scores { get; set; }
        public string Status { get; set; }

        public Prediction(int positionId, int customerId, int unitOfWorkId, Dictionary<string, double> requirements, string status = "")
        {
            PositionId = positionId;
            CustomerId = customerId;
            UnitOfWorkId = unitOfWorkId;
            Scores = requirements;
            Status = status;
        }

        public double[] GetScoreArray()
        {
            return Scores.Values.ToArray();
        }
    }
}

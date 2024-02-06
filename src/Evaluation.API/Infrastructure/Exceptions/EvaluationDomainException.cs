namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Infrastructure.Exceptions
{
    public class EvaluationDomainException : Exception
    {
        public EvaluationDomainException()
        { }

        public EvaluationDomainException(string message)
            : base(message)
        { }

        public EvaluationDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

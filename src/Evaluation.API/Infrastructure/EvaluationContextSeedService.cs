namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Services
{
    public class EvaluationContextSeedService : IDbSeeder<EvaluationContext>
    {
        public Task SeedAsync(EvaluationContext context)
        {
            return Task.CompletedTask;
        }
    }
}

using AutomaticInterviewEvaluationSystem.Evaluation.API.Services.EvaluationQueue.Abstractions;
using Evaluation.API.Services.EvaluationQueue.Abstractions;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Services.EvaluationQueue.Extensions
{
    public static class EvaluationQueueBuilderExtensions
    {
        public static IEvaluationQueueBuilder AddEvaluationQueue(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IEvaluationQueue, EvaluationQueue>();

            return new EvaluationBuilder(builder.Services);
        }

        public static IEvaluationQueueBuilder AddEvaluationUnit<T>(this IEvaluationQueueBuilder evaluationBuilder)
        {
            evaluationBuilder.Services.AddTransient(typeof(T));

            evaluationBuilder.Services.Configure<EvaluationQueueOptions>(o =>
            {
                o.EvaluationUnitTypes[typeof(T).Name] = typeof(T);
            });

            return evaluationBuilder;
        } 
    }

    public class EvaluationBuilder(IServiceCollection services) : IEvaluationQueueBuilder
    {
        public IServiceCollection Services => services;
    }
}

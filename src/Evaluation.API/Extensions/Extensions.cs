using AutomaticInterviewEvaluationSystem.Evaluation.API.Infrastructure.Integrations.BasicProgrammingSkillsEvaluationUnit;
using AutomaticInterviewEvaluationSystem.Evaluation.API.Services.EvaluationQueue.Extensions;
using Evaluation.API.Infrastructure.Integrations.BasicSoftwareSkillsEvaluationUnit;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Extensions
{
    public static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder) 
        {
            builder.AddNpgsqlDbContext<EvaluationContext>("EvaluationDB");

            if(builder.Environment.IsDevelopment())
            {
                builder.Services.AddMigrations<EvaluationContext, EvaluationContextSeedService>();
            }

            builder.Services.AddTransient<IEvaluationIntegrationEventService, EvaluationIntegrationEventService>();

            builder.AddRabbitMqEventBus()
                .AddSubscription<CreatedUnitOfWorkIntegrationEvent, CreatedUnitOfWorkIntegrationEventHandler>();

            builder.AddEvaluationQueue()
                .AddEvaluationUnit<BasicProgrammingSkillsEvaluationUnit>()
                .AddEvaluationUnit<BasicSoftwareSkillsEvaluationUnit>();
        }
    }
}

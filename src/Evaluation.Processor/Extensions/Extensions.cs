namespace AutomaticInterviewEvaluationSystem.Evaluation.Processor.Extensions
{
    public static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.AddRabbitMqEventBus()
                .AddSubscription<CreatePredictionIntentionIntegrationEvent, CreatePredictionIntentionIntegrationEventHandler>();

            if (builder.Configuration.GetConnectionString("RedisConnection") is string connectionString)
            {
                builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionString));
            }
            else
            {
                throw new Exception($"Connection string does not exist with name RedisConnection");
            }

            builder.Services.AddSingleton<IRepository, RedisRepository>();

            builder.AddEvaluationProcessor();
        }
    }
}

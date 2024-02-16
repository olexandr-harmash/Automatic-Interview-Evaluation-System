using Evaluation.Processor.Services.EvaluationProcessor.Abstractions;

namespace Evaluation.Processor.Services.AI.Extensions
{
    public static class AIExtensions
    {
        public static IHostApplicationBuilder AddEvaluationProcessor(this IHostApplicationBuilder builder)
        {
            builder.Services.Configure<NetworkOptions>(builder.Configuration.GetSection(NetworkOptions.OptionsSection));

            builder.Services.AddSingleton<INetwork, HopfieldNetwork>();
            builder.Services.AddSingleton<HopfieldProcessor>();

            return builder;
        }
    }
}

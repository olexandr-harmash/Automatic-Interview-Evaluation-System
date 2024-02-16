namespace Evaluation.Processor.Services.EvaluationProcessor.Abstractions
{
    public class NetworkOptions
    {
        public int Length { get; set; }
        public double[][] Patterns { get; set; }

        public static string OptionsSection = "NetworkOptions";
    }
}

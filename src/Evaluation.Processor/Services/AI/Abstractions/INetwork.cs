namespace Evaluation.Processor.Services.EvaluationProcessor.Abstractions
{
    public interface INetwork
    {
        double[]? Predict(double[] pattern, int maxIterations = 100);
    }
}

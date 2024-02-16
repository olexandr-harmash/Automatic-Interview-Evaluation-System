using Evaluation.Processor.Services.EvaluationProcessor.Abstractions;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.Extensions.Options;

namespace Evaluation.Processor.Services.AI
{
    /// <summary>
    /// Implements a Hamming network for pattern recognition.
    /// </summary>
    public class HopfieldNetwork : INetwork
    {
        private readonly int _neurons;    // Number of neurons in the network
        private Matrix<double> _weights;  // Weight matrix

        /// <summary>
        /// Initializes a new instance of the <see cref="HopfieldNetwork"/> class.
        /// </summary>
        /// <param name="neurons">Number of neurons in the network.</param>
        public HopfieldNetwork(int neurons)
        {
            _neurons = neurons;
            _weights = Matrix<double>.Build.Dense(neurons, neurons);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HopfieldNetwork"/> class using the specified options.
        /// </summary>
        /// <param name="options">The options containing network parameters and patterns.</param>
        public HopfieldNetwork(IOptions<NetworkOptions> options)
        {
            _neurons = options.Value.Length;

            _weights = Matrix<double>.Build.Dense(_neurons, _neurons);

            Train(options.Value.Patterns);
        }


        /// <summary>
        /// Trains the network using the provided input patterns.
        /// </summary>
        /// <param name="patterns">Input patterns for training.</param>
        public void Train(double[][] _patterns)
        {
            foreach (var pattern in _patterns)
            {
                var patternVector = Vector<double>.Build.DenseOfArray(pattern);
                _weights += patternVector.ToColumnMatrix().Multiply(patternVector.ToRowMatrix());
            }
            _weights.SetDiagonal(Vector<double>.Build.Dense(_neurons, (i) => 0));
        }

        /// <summary>
        /// Predicts the output pattern for the given input pattern.
        /// </summary>
        /// <param name="pattern">Input pattern to predict.</param>
        /// <param name="maxIterations">Maximum number of iterations for prediction.</param>
        /// <returns>A tuple containing the predicted output pattern and any prediction exceptions.</returns>
        public double[]? Predict(double[] pattern, int maxIterations = 100)
        {
            var result = _Predict(pattern, maxIterations);

            if (result is not null)
            {
                throw new HopfieldException("Pattern is unrecognized");
            }

            return result;
        }

        /// <summary>
        /// Propagates the input pattern through the network to predict the output pattern.
        /// 
        /// Initialization: Convert the input pattern into a dense vector.
        /// 
        /// Iteration: Repeat the following steps for a maximum number of iterations:
        ///     a.Calculate the output pattern by multiplying the weight matrix with the input pattern.
        ///     
        ///     b.Apply the activation function to each element of the output pattern.
        ///     
        ///     c.Check if the output pattern converges to the input pattern.
        ///        
        ///     d.Update the input pattern with the output pattern for the next iteration.
        ///         
        /// Termination: If the network does not converge within the maximum iterations, return null.
        /// 
        /// </summary>
        /// <param name="pattern">Input pattern to propagate.</param>
        /// <param name="maxIterations">Maximum number of iterations for propagation.</param>
        /// <returns>The predicted output pattern, or null if the pattern is unrecognized.</returns>
        private double[]? _Predict(double[] pattern, int maxIterations)
        {
            var inputPattern = Vector<double>.Build.DenseOfArray(pattern);

            for (int i = 0; i < maxIterations; i++)
            {
                // Calculate the output by multiplying the weight matrix with the input
                var outputPattern = _weights.Multiply(inputPattern)
                    // Apply the activation function to each element of the output pattern
                    .Map(v => _activation(v));

                if (outputPattern.Equals(inputPattern))
                    return outputPattern.ToArray();

                inputPattern = outputPattern;
            }

            return null;
        }

        /// <summary>
        /// Activation function for the network.
        /// </summary>
        /// <param name="v">Input value.</param>
        /// <returns>Output of the activation function.</returns>
        private double _activation(double v)
        {
            if (v > 0)
                return 1;
            else if (v < 0)
                return -1;
            else
                return 0;
        }
    }

    /// <summary>
    /// Represents exceptions that occur during prediction.
    /// </summary>
    public class HopfieldException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PredictExceptions"/> class with a specified reason.
        /// </summary>
        /// <param name="reason">The reason for the exception.</param>
        public HopfieldException(string reason) : base(reason)
        {

        }
    }
}

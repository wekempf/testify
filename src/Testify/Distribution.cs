using System;

namespace Testify
{
    /// <summary>
    /// Provides random number generation using a specified distribution algorithm.
    /// </summary>
    /// <remarks>
    /// Methods that take a <see cref="Distribution"/> should generally treat <c>null</c> values as
    /// <see cref="P:Distribution.Uniform"/>.</remarks>
    public abstract class Distribution
    {
        private const double MEAN = 0.0;
        private const int NSIGMA = 3;
        private const double STDDEV = 1.0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Distribution"/> class.
        /// </summary>
        protected internal Distribution()
        {
        }

        /// <summary>
        /// Gets a <see cref="Distribution"/> that uses an inverted normal distribution algorithm.
        /// </summary>
        public static Distribution InvertedNormal { get; } = new InvertedNormalDistribution();

        /// <summary>
        /// Gets a <see cref="Distribution"/> that uses a negative normal distribution algorithm.
        /// </summary>
        public static Distribution NegativeNormal { get; } = new NegativeNormalDistribution();

        /// <summary>
        /// Gets a <see cref="Distribution"/> that uses a positive normal distribution algorithm.
        /// </summary>
        public static Distribution PositiveNormal { get; } = new PositiveNormalDistribution();

        /// <summary>
        /// Gets a <see cref="Distribution"/> that uses an uniform distribution algorithm.
        /// </summary>
        public static Distribution Uniform { get; } = new UniformDistribution();

        /// <summary>
        /// Get the next double in the random distribution.
        /// </summary>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>The next double in the random distribution.</returns>
        public abstract double NextDouble(Random random);

        /// <summary>
        /// Get the next double in the Gaussian distribution.
        /// </summary>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>The next double in the Gaussian distribution.</returns>
        protected double NextGausian(Random random) => NextGausian(random, NSIGMA);

        /// <summary>
        /// Get the next double in the Gaussian distribution.
        /// </summary>
        /// <param name="random">The random number generator to use.</param>
        /// <param name="sigma">The sigma to use.</param>
        /// <returns>The next double in the Gaussian distribution.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="random"/> is <c>null</c>.</exception>
        protected double NextGausian(Random random, int sigma)
        {
            Argument.NotNull(random, nameof(random));

            double u1 = random.NextDouble();
            double u2 = random.NextDouble();
            double stdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double gausian = MEAN + (STDDEV * stdNormal);
            return (gausian % sigma) / sigma;
        }

        private sealed class InvertedNormalDistribution : Distribution
        {
            public override double NextDouble(Random random)
            {
                Argument.NotNull(random, nameof(random));

                double next = NextGausian(random, NSIGMA * 2);
                return (next < 0) ? 1 + next : next;
            }
        }

        private sealed class NegativeNormalDistribution : Distribution
        {
            public override double NextDouble(Random random) => Math.Abs(-1 + Math.Abs(NextGausian(random)));
        }

        private sealed class PositiveNormalDistribution : Distribution
        {
            public override double NextDouble(Random random) => Math.Abs(NextGausian(random));
        }

        private sealed class UniformDistribution : Distribution
        {
            public override double NextDouble(Random random) => random.NextDouble();
        }
    }
}
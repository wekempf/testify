using System;
using System.Collections.Generic;

namespace Testify
{
    /// <summary>
    /// Classifies values.
    /// </summary>
    /// <typeparam name="TValue">The type of values to be classified.</typeparam>
    public sealed class Classifier<TValue>
    {
        private readonly Dictionary<string, Classification> classifications =
            new Dictionary<string, Classification>();

        /// <summary>
        /// Gets the number of values that have been classified.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the number of values that have been classified in the specified category.
        /// </summary>
        /// <param name="key">The classification category.</param>
        /// <returns>The number of values that were classified in the specified category.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key does not exist in the collection.</exception>
        public double this[string key] =>
            Count == 0 ? 0 : (double)classifications[key].Count / (double)Count;

        /// <summary>
        /// Adds a classification category.
        /// </summary>
        /// <param name="name">The name of the classification category.</param>
        /// <param name="predicate">The predicate values must pass in order to be classified in this category.</param>
        /// <exception cref="ArgumentNullException">Either <paramref name="name"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="ArgumentException"><para><paramref name="name"/> is empty.</para>
        /// <para>-or-</para>
        /// <para>A category with the same name already exists in the <see cref="Classifier{TValue}"/>.</para></exception>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        /// <exception cref="InvalidOperationException">Values have already been classified.</exception>
        public void AddClassification(string name, Predicate<TValue> predicate)
        {
            Argument.NotNullOrEmpty(name, nameof(name));
            Argument.NotNull(predicate, nameof(predicate));

            if (Count > 0)
            {
                throw new InvalidOperationException("Cannot add classifications after classifying any values.");
            }

            classifications.Add(name, new Classification(predicate));
        }

        /// <summary>
        /// Adds the value to every valid classification category.
        /// </summary>
        /// <param name="value">The value to classify.</param>
        public void Classify(TValue value)
        {
            foreach (var classification in classifications.Values)
            {
                if (classification.Predicate(value))
                {
                    classification.Count++;
                }
            }

            Count++;
        }

        /// <summary>
        /// Produces and classifies 1000 values.
        /// </summary>
        /// <param name="producer">The delegate used to produce values.</param>
        /// <exception cref="ArgumentNullException"><paramref name="producer"/> is null.</exception>
        public void Classify(Func<TValue> producer)
        {
            Argument.NotNull(producer, nameof(producer));

            Classify(1000, producer);
        }

        /// <summary>
        /// Produces and classifies values.
        /// </summary>
        /// <param name="runs">The number of values to produce.</param>
        /// <param name="producer">The delegate used to produce values.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="runs"/> is negative.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="producer"/> is null.</exception>
        public void Classify(int runs, Func<TValue> producer)
        {
            Argument.InRange(runs, 0, int.MaxValue, nameof(runs));
            Argument.NotNull(producer, nameof(producer));

            for (var i = 0; i < runs; ++i)
            {
                Classify(producer());
            }
        }

        private sealed class Classification
        {
            internal Classification(Predicate<TValue> predicate)
            {
                Argument.NotNull(predicate, nameof(predicate));

                Predicate = predicate;
            }

            public int Count { get; set; }

            public Predicate<TValue> Predicate { get; }
        }
    }
}
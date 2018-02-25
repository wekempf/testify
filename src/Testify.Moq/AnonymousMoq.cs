using System;
using System.ComponentModel;
using Moq;

namespace Testify
{
    /// <summary>
    /// Provides extension methods for registring mock behavior using Moq.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AnonymousMoq
    {
        /// <summary>
        /// Freezes a mock instance as the result for any further calls to <see cref="AnonymousData.Any"/>
        /// for the specified type.
        /// </summary>
        /// <typeparam name="T">The type to freeze.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="config">The delegate invoked to configure the mock behavior.</param>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static void FreezeMock<T>(this IRegisterAnonymousData anon, Action<Mock<T>> config)
            where T : class
        {
            var mock = new Mock<T>();
            config?.Invoke(mock);
            anon.Freeze(mock);
        }

        /// <summary>
        /// Freezes a mock instance as the result for any further calls to <see cref="AnonymousData.Any"/>
        /// for the specified type.
        /// </summary>
        /// <typeparam name="T">The type to freeze.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="instance">The mock instance to freeze.</param>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static void FreezeMock<T>(this IRegisterAnonymousData anon, Mock<T> instance)
            where T : class =>
            anon.Freeze(instance);

        /// <summary>
        /// Freezes a mock instance with default behavior as the result for any further calls to <see cref="AnonymousData.Any"/>
        /// for the specified type.
        /// </summary>
        /// <typeparam name="T">The type to freeze.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="anon"/> is <see langword="null"/>.</exception>
        public static void FreezeMock<T>(this IRegisterAnonymousData anon)
            where T : class =>
            anon.FreezeMock(new Mock<T>());

        /// <summary>
        /// Registers the mock object with a delegate to use to configure the mock behavior.
        /// </summary>
        /// <typeparam name="T">The type to mock.</typeparam>
        /// <param name="anon">The anonymous data provider to use.</param>
        /// <param name="config">The delegate invoked to configure the mock behavior.</param>
        public static void RegisterMock<T>(this IRegisterAnonymousData anon, Action<Mock<T>> config)
            where T : class
        {
            Argument.NotNull(anon, nameof(anon));

            anon.Register<Mock<T>>(_ =>
            {
                var mock = new Mock<T>();
                config?.Invoke(mock);
                return mock;
            });
        }
    }
}
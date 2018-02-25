using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Testify
{
    /// <summary>
    /// Provides extension methods for performing reflection based operations.
    /// </summary>
    internal static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the add method.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns>The Add method.</returns>
        internal static MethodInfo GetAddMethod(this Type sourceType)
        {
            Argument.NotNull(sourceType, nameof(sourceType));

            return Enumerable.Repeat(sourceType, 1)
                .Concat(sourceType.GetInterfaces().Where(t => t.Is(typeof(ICollection<>))))
                .Concat(sourceType.GetInterfaces().Where(t => t.Is(typeof(ICollection))))
                .Select(t => t.GetTypeInfo().DeclaredMethods.FirstOrDefault(m => m.Name == "Add" && m.GetParameters().Length == 1))
                .FirstOrDefault(t => t != null);
        }

        /// <summary>
        /// Gets the base type of the <paramref name="sourceType"/>.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <returns>The base type.</returns>
        internal static Type GetBaseType(this Type sourceType)
        {
            Argument.NotNull(sourceType, nameof(sourceType));

            return sourceType == typeof(object)
                ? null
                : sourceType.GetBaseType();
        }

        /// <summary>
        /// Gets the constructor.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The constructor.</returns>
        internal static ConstructorInfo GetConstructor(this Type sourceType, params Type[] parameterTypes)
        {
            Argument.NotNull(sourceType, nameof(sourceType));
            Argument.NotNull(parameterTypes, nameof(parameterTypes));

            return sourceType.GetTypeInfo().DeclaredConstructors
                .FirstOrDefault(
                    c => c.GetParameters().Length == parameterTypes.Length &&
                        c.GetParameters()
                        .Select(p => p.ParameterType)
                        .Zip(parameterTypes, (t1, t2) => Tuple.Create(t1, t2))
                        .All(t => t.Item1.Is(t.Item2)));
        }

        /// <summary>
        /// Gets the constructors.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <returns>The constructors.</returns>
        internal static IEnumerable<ConstructorInfo> GetConstructors(this Type sourceType)
        {
            Argument.NotNull(sourceType, nameof(sourceType));

            return sourceType.GetTypeInfo().DeclaredConstructors;
        }

        /// <summary>
        /// Gets the generic type argument for the <paramref name="sourceType" />.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="index">The index of the argument.</param>
        /// <returns>The generic type arguments for the <paramref name="sourceType" />.</returns>
        internal static Type GetGenericTypeArgument(this Type sourceType, int index)
        {
            Argument.NotNull(sourceType, nameof(sourceType));

            return sourceType.GetTypeInfo().GenericTypeArguments[index];
        }

        /// <summary>
        /// Gets the interfaces implemented by the <paramref name="sourceType"/>.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <returns>The interfaces implemented by the <paramref name="sourceType"/>.</returns>
        internal static IEnumerable<Type> GetInterfaces(this Type sourceType)
        {
            Argument.NotNull(sourceType, nameof(sourceType));

            return sourceType.GetTypeInfo().ImplementedInterfaces;
        }

        /// <summary>
        /// Gets the member information.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The member information.</returns>
        internal static MemberExpression GetMemberInfo(Expression method)
        {
            var lambda = method as LambdaExpression;
            if (lambda == null)
            {
                return null;
            }

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            return memberExpr;
        }

        /// <summary>
        /// Gets the method function.
        /// </summary>
        /// <typeparam name="T">The parameters type.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="sourceType">The source type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>The <see cref="Func{T, TResult}"/>.</returns>
        internal static Func<T, TResult> GetMethodFunc<T, TResult>(this Type sourceType, string methodName)
        {
            Argument.NotNull(sourceType, nameof(sourceType));
            Argument.NotNullOrEmpty(methodName, nameof(methodName));

            var typeInfo = sourceType.GetTypeInfo();
            var method = typeInfo.DeclaredMethods.Single(m => m.Name == methodName);
            return (Func<T, TResult>)(object)method.CreateDelegate(typeof(Func<T, TResult>));
        }

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>The method.</returns>
        internal static MethodInfo GetMethod(this Type sourceType, string methodName)
        {
            Argument.NotNull(sourceType, nameof(sourceType));
            Argument.NotNull(methodName, nameof(methodName));

            var typeInfo = sourceType.GetTypeInfo();
            return typeInfo.DeclaredMethods.SingleOrDefault(m => m.Name == methodName);
        }

        /// <summary>
        /// Creates the function.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>The func.</returns>
        internal static Func<TSource, TResult> CreateFunc<TSource, TResult>(this MethodInfo methodInfo)
        {
            Argument.NotNull(methodInfo, nameof(methodInfo));

            return (Func<TSource, TResult>)methodInfo.CreateDelegate(typeof(Func<TSource, TResult>));
        }

        /// <summary>
        /// Creates the function.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>The func.</returns>
        internal static Func<TSource, T1, TResult> CreateFunc<TSource, T1, TResult>(this MethodInfo methodInfo)
        {
            Argument.NotNull(methodInfo, nameof(methodInfo));

            return (Func<TSource, T1, TResult>)methodInfo.CreateDelegate(typeof(Func<TSource, T1, TResult>));
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="index">The index.</param>
        /// <returns>The parameter.</returns>
        internal static ParameterInfo GetParameter(this MethodBase method, int index)
        {
            Argument.NotNull(method, nameof(method));

            return method.GetParameters()[index];
        }

        /// <summary>
        /// Determines whether or not the <paramref name="sourceType"/> is the specified type.
        /// </summary>
        /// <typeparam name="TType">The type to check for.</typeparam>
        /// <param name="sourceType">The source type.</param>
        /// <returns><see langword="true"/> if the <paramref name="sourceType"/> is the specified type;
        /// otherwise, <see langword="false"/>.</returns>
        internal static bool Is<TType>(this Type sourceType)
        {
            Argument.NotNull(sourceType, nameof(sourceType));

            var type = typeof(TType);
            return sourceType.Is(type);
        }

        /// <summary>
        /// Determines whether or not the <paramref name="sourceType"/> is the specified type.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="type">The type to check.</param>
        /// <returns><see langword="true"/> if the <paramref name="sourceType"/> is the specified type;
        /// otherwise, <see langword="false"/>.</returns>
        internal static bool Is(this Type sourceType, Type type)
        {
            Argument.NotNull(sourceType, nameof(sourceType));
            Argument.NotNull(type, nameof(type));

            if (type.IsGenericTypeDefinition())
            {
                return sourceType.IsGenericType() && sourceType.GetGenericTypeDefinition() == type;
            }

            return sourceType == type;
        }

        /// <summary>
        /// Determines whether the specified source type is abstract.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns><see langword="true"/> if the <paramref name="sourceType"/> is abstract;
        /// otherwise, <see langword="false"/>.</returns>
        internal static bool IsAbstract(this Type sourceType)
        {
            Argument.NotNull(sourceType, nameof(sourceType));

            return sourceType.GetTypeInfo().IsAbstract;
        }

        /// <summary>
        /// Determines whether or not the <paramref name="sourceType"/> is assignable from the specified type.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="type">The type.</param>
        /// <returns><see langword="true"/> if the <paramref name="sourceType"/> is assignable to the
        /// specified type; otherwise, <see langword="false"/>.</returns>
        internal static bool IsAssignableFrom(this Type sourceType, Type type)
        {
            Argument.NotNull(sourceType, nameof(sourceType));
            Argument.NotNull(type, nameof(type));

            return IsAssignable(type, sourceType);
        }

        /// <summary>
        /// Determines whether this instance is a collection type.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified source type is collection; otherwise, <see langword="false"/>.
        /// </returns>
        internal static bool IsCollectionType(this Type sourceType)
        {
            Argument.NotNull(sourceType, nameof(sourceType));

            return sourceType.Is(typeof(ICollection<>)) ||
                sourceType.Is(typeof(ICollection)) ||
                sourceType.GetInterfaces().Any(t => t.IsCollectionType());
        }

        /// <summary>
        /// Determines whether or not the specified source type is an enum.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <returns><see langword="true"/> if the <paramref name="sourceType"/> is an enum type; otherwise,
        /// <see langword="false"/>.</returns>
        internal static bool IsEnum(this Type sourceType)
        {
            Argument.NotNull(sourceType, nameof(sourceType));

            return sourceType.GetTypeInfo().IsEnum;
        }

        /// <summary>
        /// Determines whether or not the type is a generic type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><see langword="true"/> if the type is a generic type definition; otherwise,
        /// <see langword="false"/>.</returns>
        internal static bool IsGenericType(this Type type)
        {
            Argument.NotNull(type, nameof(type));

            return type.GetTypeInfo().IsGenericType;
        }

        /// <summary>
        /// Determines whether or not the type is a generic type definition.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><see langword="true"/> if the type is a generic type definition; otherwise,
        /// <see langword="false"/>.</returns>
        internal static bool IsGenericTypeDefinition(this Type type)
        {
            Argument.NotNull(type, nameof(type));

            return type.GetTypeInfo().IsGenericTypeDefinition;
        }

        /// <summary>
        /// Determines whether the specified object is an instance of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="obj">The object.</param>
        /// <returns><see langword="true"/> if the object is an instance of the specified type;
        /// otherwise <see langword="false"/>.</returns>
        internal static bool IsInstanceOfType(this Type type, object obj)
        {
            Argument.NotNull(type, nameof(type));

            var typeInfo = type.GetTypeInfo();
            return obj == null
                ? typeInfo.IsClass
                : typeInfo.IsAssignableFrom(obj.GetType().GetTypeInfo());
        }

        /// <summary>
        /// Determines whether or not the <paramref name="sourceType"/> is an interface.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <returns><see langword="true"/> if the <paramref name="sourceType"/> is an interface;
        /// otherwise, <see langword="false"/>.</returns>
        internal static bool IsInterface(this Type sourceType)
        {
            Argument.NotNull(sourceType, nameof(sourceType));

            return sourceType.GetTypeInfo().IsInterface;
        }

        /// <summary>
        /// Determines whether the specified instance is the specified type.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="type">The type.</param>
        /// <returns><see langword="true"/> if the instance is the specified type; otherwise,
        /// <see langword="false"/>.</returns>
        internal static bool IsType(this Type sourceType, Type type)
        {
            Argument.NotNull(sourceType, nameof(sourceType));
            Argument.NotNull(type, nameof(type));

            if (sourceType.IsTypeHelper(type))
            {
                return true;
            }

            if (sourceType.GetTypeInfo().ImplementedInterfaces.Any(t => t.IsTypeHelper(type)))
            {
                return true;
            }

            if (sourceType == typeof(object))
            {
                return type == typeof(object);
            }

            if (sourceType.IsInterface())
            {
                return false;
            }

            return sourceType.GetTypeInfo().BaseType.IsType(type);
        }

        private static bool IsAssignable(Type from, Type to)
        {
            Argument.NotNull(from, nameof(from));
            Argument.NotNull(to, nameof(to));

            if (from.Is(to))
            {
                return true;
            }

            if (to.IsInterface())
            {
                return from.GetInterfaces().Any(i => i.Is(to));
            }

            for (var baseType = from.GetBaseType(); baseType != null; baseType.GetBaseType())
            {
                if (baseType.Is(to))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsTypeHelper(this Type sourceType, Type type)
        {
            Argument.NotNull(sourceType, nameof(sourceType));
            Argument.NotNull(type, nameof(type));

            if (type.IsGenericTypeDefinition() && sourceType.IsGenericType())
            {
                return sourceType.GetTypeInfo().GetGenericTypeDefinition() == type;
            }

            return sourceType == type;
        }
    }
}
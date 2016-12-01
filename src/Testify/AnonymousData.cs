using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Factory = System.Func<Testify.IAnonymousData, object>;

namespace Testify
{
    /// <summary>
    /// Provides an object factory that can be used to create anonymous values for use in unit tests.
    /// </summary>
    public sealed class AnonymousData : IAnonymousData, IRegisterAnonymousData
    {
        private static readonly Dictionary<Type, Factory> GlobalFactories = new Dictionary<Type, Factory>();
        private static readonly Dictionary<PropertyInfo, Factory> GlobalPropertyFactories = new Dictionary<PropertyInfo, Factory>();
        private readonly List<IAnonymousDataCustomization> customizations = new List<IAnonymousDataCustomization>();
        private readonly Dictionary<Type, Factory> factories = new Dictionary<Type, Factory>();
        private readonly Dictionary<PropertyInfo, Factory> propertyFactories = new Dictionary<PropertyInfo, Factory>();
        private readonly Random random;

        static AnonymousData()
        {
            RegisterDefault(f => f.AnyBool());
            RegisterDefault(f => f.AnyByte());
            RegisterDefault(f => f.AnyChar());
            RegisterDefault(f => f.AnyDateTime());
            RegisterDefault(f => f.AnyDateTimeOffset());
            RegisterDefault(f => f.AnyDouble());
            RegisterDefault(f => f.AnySingle());
            RegisterDefault(f => f.AnyInt32());
            RegisterDefault(f => f.AnyInt64());
            RegisterDefault(f => f.AnyInt16());
            RegisterDefault(f => f.AnyDecimal());
            RegisterDefault(f => f.AnyString());
            RegisterDefault(f => f.AnyTimeSpan());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousData"/> class.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This constructor initializes the <see cref="AnonymousData"/> instance with a constant seed value.
        /// </para>
        /// <para>
        /// <see cref="AnonymousData"/> generates random objects. While the intent is for only values that you intend
        /// to not have any impact on your test it is still a best practice to create a new instance per test to
        /// ensure consistent test runs. You can factor out the creation and configuration instead of sharing an
        /// instance in order to use the same configuration in multiple tests.
        /// </para>
        /// </remarks>
        public AnonymousData()
            : this(0x07357FAC)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousData"/> class.
        /// </summary>
        /// <param name="seed">The seed to provide to the random number generator.</param>
        /// <remarks>
        /// <para>
        /// <see cref="AnonymousData"/> generates random objects. While the intent is for only values that you intend
        /// to not have any impact on your test it is still a best practice to create a new instance per test to
        /// ensure consistent test runs. You can factor out the creation and configuration instead of sharing an
        /// instance in order to use the same configuration in multiple tests.
        /// </para>
        /// </remarks>
        public AnonymousData(int seed)
        {
            this.random = new Random(seed);
        }

        /// <summary>
        /// Register a factory method for the specified type that will be used by all <see cref="AnonymousData"/>
        /// instances.
        /// </summary>
        /// <param name="type">The type of object the factory method creates.</param>
        /// <param name="factory">The factory method.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="factory"/> is <c>null</c>.</exception>
        public static void RegisterDefault(Type type, Factory factory)
        {
            Argument.NotNull(type, nameof(type));
            Argument.NotNull(factory, nameof(factory));

            GlobalFactories[type] = factory;
        }

        /// <summary>
        /// Registers a factory method for the specified propertythat will be used by all <see cref="AnonymousData"/>
        /// instances.
        /// </summary>
        /// <param name="property">The property to populate.</param>
        /// <param name="factory">The factory method.</param>
        public static void RegisterDefault(PropertyInfo property, Factory factory)
        {
            Argument.NotNull(property, nameof(property));
            Argument.NotNull(factory, nameof(factory));

            GlobalPropertyFactories[property] = factory;
        }

        /// <summary>
        /// Register a factory method for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object the factory method creates.</typeparam>
        /// <param name="factory">The factory method.</param>
        /// <exception cref="ArgumentNullException"><paramref name="factory"/> is <c>null</c>.</exception>
        public static void RegisterDefault<T>(Func<IAnonymousData, T> factory)
        {
            Argument.NotNull(factory, nameof(factory));

            RegisterDefault(typeof(T), f => factory(f));
        }

        /// <summary>
        /// Registers a factory method for the specified property.
        /// </summary>
        /// <typeparam name="T">The type that declares the property.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">An expression representing the property to populate.</param>
        /// <param name="factory">The factory method.</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyExpression"/> or
        /// <paramref name="factory"/> is <c>null</c>.</exception>
        public static void RegisterDefault<T, TProperty>(
            Expression<Func<T, TProperty>> propertyExpression,
            Func<IAnonymousData, TProperty> factory)
        {
            Argument.NotNull(propertyExpression, nameof(propertyExpression));
            Argument.NotNull(factory, nameof(factory));

            var member = ReflectionExtensions.GetMemberInfo(propertyExpression);
            var property = member?.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("Invalid property expression.");
            }

            RegisterDefault(property, f => factory(f));
        }

        /// <summary>
        /// Creates an instance of the specified type of object.
        /// </summary>
        /// <param name="type">The type to create.</param>
        /// <param name="populateOption">The populate option.</param>
        /// <returns>
        /// An instance of the specified type.
        /// </returns>
        /// <exception cref="AnonymousDataException">The specified type could not be created.</exception>
        public object Any(Type type, PopulateOption populateOption)
        {
            Argument.NotNull(type, nameof(type));

            if (type.Is<AnonymousData>() || type.Is<IAnonymousData>())
            {
                return this;
            }

            Factory factory;
            if (this.factories.TryGetValue(type, out factory) || GlobalFactories.TryGetValue(type, out factory))
            {
                object value;
                try
                {
                    value = factory(this);
                    this.Populate(value, populateOption);
                }
                catch (Exception e)
                {
                    throw new AnonymousDataException(type, e);
                }

                return value;
            }

            var context = new AnonymousDataContext(this, type);
            object result;
            try
            {
                if (context.CallNextCustomization(out result))
                {
                    return this.Populate(result, populateOption);
                }
            }
            catch (Exception e)
            {
                throw new AnonymousDataException(type, e);
            }

            throw new AnonymousDataException(type);
        }

        /// <summary>
        /// Creates a random double value within a specified range using the specified distribution algorithm.
        /// </summary>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="distribution">The distribution algorithm to use.</param>
        /// <returns>A random double value.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
        public double AnyDouble(double minimum, double maximum, Distribution distribution)
        {
            Argument.InRange(maximum, minimum, double.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");
            if (double.IsInfinity(maximum - minimum))
            {
                throw new ArgumentOutOfRangeException($"The range of {nameof(maximum)} - {nameof(minimum)} is Infinity.");
            }

            var next = (distribution ?? Distribution.Uniform).NextDouble(this.random);
            return minimum + (next * (maximum - minimum));
        }

        /// <summary>
        /// Customizes how the <see cref="AnonymousData"/> creates objects.
        /// </summary>
        /// <param name="customization">The customization to apply.</param>
        /// <returns>The current <see cref="AnonymousData"/> to allow for method call chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="customization"/> is <c>null</c>.</exception>
        public AnonymousData Customize(IAnonymousDataCustomization customization)
        {
            Argument.NotNull(customization, nameof(customization));

            this.customizations.Add(customization);
            return this;
        }

        /// <summary>
        /// Populates the specified instance by assigning all properties to anonymous values.
        /// </summary>
        /// <typeparam name="TInstance">The type of the instance to populate.</typeparam>
        /// <param name="instance">The instance to populate.</param>
        /// <param name="deep">If set to <see langword="true"/> then properties are assigned recursively, populating
        /// the entire object tree.</param>
        /// <returns>The populated instance.</returns>
        public TInstance Populate<TInstance>(TInstance instance, bool deep)
        {
            var queue = deep ? new Queue<object>() : null;
            object current = instance;
            while (true)
            {
                if (current != null)
                {
                    var type = current.GetType();
                    var properties =
                        from prop in type.GetRuntimeProperties()
                        where !prop.PropertyType.GetTypeInfo().IsPrimitive &&
                            (prop.PropertyType.IsCollectionType() || (prop.CanWrite && prop.SetMethod.IsPublic && IsDefaultValue(current, prop))) &&
                            !(prop.GetIndexParameters()?.Any() ?? false)
                        select prop;
                    foreach (var prop in properties)
                    {
                        try
                        {
                            if (prop.PropertyType.IsCollectionType())
                            {
                                var collection = prop.GetValue(current);
                                if (collection != null)
                                {
                                    var method = prop.PropertyType.GetAddMethod();
                                    if (method != null)
                                    {
                                        foreach (var item in this.AnyEnumerable(method.GetParameters()[0].ParameterType))
                                        {
                                            method.Invoke(collection, new[] { item });
                                            if (deep && !item.GetType().GetTypeInfo().IsPrimitive)
                                            {
                                                queue.Enqueue(item);
                                            }
                                        }
                                    }
                                }
                                else if (prop.CanWrite)
                                {
                                    var value = this.Any(prop.PropertyType);
                                    prop.SetValue(current, value);
                                    if (deep)
                                    {
                                        foreach (var item in (IEnumerable)value)
                                        {
                                            if (!item.GetType().GetTypeInfo().IsPrimitive)
                                            {
                                                queue.Enqueue(item);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                object value;
                                Factory factory;
                                if (this.propertyFactories.TryGetValue(prop, out factory) ||
                                    GlobalPropertyFactories.TryGetValue(prop, out factory))
                                {
                                    value = factory(this);
                                }
                                else
                                {
                                    value = this.Any(prop.PropertyType);
                                }

                                prop.SetValue(current, value);
                                if (deep && !prop.PropertyType.GetTypeInfo().IsPrimitive)
                                {
                                    queue.Enqueue(value);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw new AnonymousDataException(prop, e);
                        }
                    }
                }

                if (!deep || !queue.Any())
                {
                    break;
                }

                current = queue.Dequeue();
            }

            return instance;
        }

        /// <summary>
        /// Register a factory method for the specified type.
        /// </summary>
        /// <param name="type">The type of object the factory method creates.</param>
        /// <param name="factory">The factory method.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="factory"/> is <c>null</c>.</exception>
        public void Register(Type type, Factory factory)
        {
            Argument.NotNull(type, nameof(type));
            Argument.NotNull(factory, nameof(factory));

            this.factories[type] = factory;
        }

        /// <summary>
        /// Registers a factory method for the specified property.
        /// </summary>
        /// <param name="property">The property to populate.</param>
        /// <param name="factory">The factory method.</param>
        public void Register(PropertyInfo property, Factory factory)
        {
            Argument.NotNull(property, nameof(property));
            Argument.NotNull(factory, nameof(factory));

            this.propertyFactories[property] = factory;
        }

        private static bool IsDefaultValue<TInstance>(TInstance instance, PropertyInfo prop)
        {
            var defaultValue = prop.PropertyType.GetTypeInfo().IsValueType
                ? Activator.CreateInstance(prop.PropertyType)
                : null;
            return object.Equals(defaultValue, prop.GetValue(instance));
        }

        private bool Any(Type type, out object result)
        {
            Argument.NotNull(type, nameof(type));

            if (type.IsEnum())
            {
                result = this.AnyEnumValue(type);
                return true;
            }

            var constructors =
                from constructor in type.GetConstructors()
                let parameters = constructor.GetParameters()
                orderby parameters.Length ascending
                select new { Constructor = constructor, Parameters = parameters };
            var info = constructors.FirstOrDefault();
            if (info == null)
            {
                throw new AnonymousDataException(type);
            }

            var args = info.Parameters.Select(a => this.Any(a.ParameterType)).ToArray();
            try
            {
                result = info.Constructor.Invoke(args);
            }
            catch (Exception e)
            {
                throw new AnonymousDataException(type, e);
            }

            return true;
        }

        private object BuildArray(Type type)
        {
            Argument.NotNull(type, nameof(type));

            var genericFactoryType = typeof(AnonymousArray<>);
            var factoryType = genericFactoryType.MakeGenericType(type);
            var factory = factoryType.GetMethodFunc<IAnonymousData, object>(nameof(AnonymousArray<int>.AnyArray));
            return factory(this);
        }

        private bool BuildCollection(Type type, out object result)
        {
            Argument.NotNull(type, nameof(type));

            if (type.IsArray)
            {
                var itemType = type.GetElementType();
                result = this.BuildArray(itemType);
                return true;
            }

            if (type.Is(typeof(IEnumerable<>)))
            {
                var itemType = type.GetGenericTypeArgument(0);
                result = this.BuildEnumerable(itemType);
                return true;
            }

            var ctor = type.GetConstructor(typeof(IEnumerable<>));
            if (ctor != null)
            {
                var items = this.Any(ctor.GetParameter(0).ParameterType);
                result = ctor.Invoke(new[] { items });
                return true;
            }

            ctor = type.GetConstructor();
            if (ctor != null)
            {
                result = ctor.Invoke(new object[0]);
                var method = type.GetAddMethod();
                if (method != null)
                {
                    foreach (var item in this.AnyEnumerable(method.GetParameters()[0].ParameterType))
                    {
                        method.Invoke(result, new[] { item });
                    }
                }

                return true;
            }

            result = null;
            return false;
        }

        private object BuildEnumerable(Type type)
        {
            Argument.NotNull(type, nameof(type));

            var genericFactoryType = typeof(AnonymousArray<>);
            var factoryType = genericFactoryType.MakeGenericType(type);
            var factory = factoryType.GetMethodFunc<IAnonymousData, object>(nameof(AnonymousArray<int>.AnyEnumerable));
            return factory(this);
        }

        private object Populate(object instance, PopulateOption populateOption)
        {
            if (populateOption != PopulateOption.None)
            {
                this.Populate(instance, populateOption == PopulateOption.Deep);
            }

            return instance;
        }

        private class AnonymousDataContext : IAnonymousDataContext
        {
            private readonly AnonymousData factory;
            private int current;

            public AnonymousDataContext(AnonymousData factory, Type resultType)
            {
                Argument.NotNull(factory, nameof(factory));
                Argument.NotNull(resultType, nameof(resultType));

                this.factory = factory;
                this.ResultType = resultType;
                this.current = this.factory.customizations.Count;
            }

            public Type ResultType { get; }

            public object Any(Type type, PopulateOption populateOption)
            {
                Argument.NotNull(type, nameof(type));

                return this.factory.Any(type, populateOption);
            }

            public double AnyDouble(double minimum, double maximum, Distribution distribution)
            {
                Argument.InRange(maximum, minimum, double.MaxValue, nameof(maximum), "The maximum value must be greater than the minimum value.");

                return this.factory.AnyDouble(minimum, maximum, distribution);
            }

            public bool CallNextCustomization(out object result)
            {
                if (this.current != 0)
                {
                    return this.factory.customizations[--this.current].Create(this, out result);
                }

                if (this.ResultType.IsType(typeof(IEnumerable)))
                {
                    return this.factory.BuildCollection(this.ResultType, out result);
                }

                if (this.ResultType.IsInterface() || this.ResultType.IsAbstract())
                {
                    result = null;
                    return false;
                }

                return this.factory.Any(this.ResultType, out result);
            }

            /// <summary>
            /// Populates the specified instance by assigning all properties to anonymous values.
            /// </summary>
            /// <typeparam name="TInstance">The type of the instance to populate.</typeparam>
            /// <param name="instance">The instance to populate.</param>
            /// <param name="deep">If set to <see langword="true" /> then properties are assigned recursively, populating
            /// the entire object tree.</param>
            /// <returns>The populated instance.</returns>
            public TInstance Populate<TInstance>(TInstance instance, bool deep)
            {
                return this.factory.Populate(instance, deep);
            }
        }
    }
}
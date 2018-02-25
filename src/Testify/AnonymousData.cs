using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Factory = System.Func<Testify.IAnonymousData, object>;
using PropertyFactories = System.Collections.Generic.Dictionary<System.Reflection.PropertyInfo, System.Func<Testify.IAnonymousData, object>>;

namespace Testify
{
    /// <summary>
    /// Provides an object factory that can be used to create anonymous values for use in unit tests.
    /// </summary>
    public sealed class AnonymousData : IAnonymousData, IRegisterAnonymousData
    {
        private static readonly object[] EmptyArgs = new object[0];

        private static readonly Dictionary<Type, Factory> GlobalFactories = new Dictionary<Type, Factory>();
        private static readonly PropertyFactories GlobalPropertyFactories = new PropertyFactories();

        private readonly List<IAnonymousDataCustomization> customizations = new List<IAnonymousDataCustomization>();
        private readonly Dictionary<Type, Factory> factories = new Dictionary<Type, Factory>();
        private readonly PropertyFactories propertyFactories = new PropertyFactories();
        private readonly Random random;
        private readonly Dictionary<string, object> registeredValues = new Dictionary<string, object>();

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
        public AnonymousData(int seed) => random = new Random(seed);

        /// <summary>
        /// Register a factory method for the specified type that will be used by all <see cref="AnonymousData"/>
        /// instances.
        /// </summary>
        /// <param name="type">The type of object the factory method creates.</param>
        /// <param name="factory">The factory method.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="factory"/> is
        /// <see langword="null"/>.</exception>
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
        /// <exception cref="ArgumentNullException"><paramref name="factory"/> is <see langword="null"/>.</exception>
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
        /// <paramref name="factory"/> is <see langword="null"/>.</exception>
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

            if (factories.TryGetValue(type, out Factory factory) || GlobalFactories.TryGetValue(type, out factory))
            {
                object value;
                try
                {
                    value = factory(this);
                    Populate(value, populateOption);
                }
                catch (Exception e)
                {
                    throw new AnonymousDataException(type, e);
                }

                return value;
            }

            var context = new AnonymousDataContext(this, type);
            try
            {
                if (context.CallNextCustomization(out object result))
                {
                    return Populate(result, populateOption);
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
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximum"/> is less than
        /// <paramref name="minimum"/>.</exception>
        public double AnyDouble(double minimum, double maximum, Distribution distribution)
        {
            Argument.InRange(
                maximum,
                minimum,
                double.MaxValue,
                nameof(maximum),
                "The maximum value must be greater than the minimum value.");
            if (double.IsInfinity(maximum - minimum))
            {
                throw new ArgumentOutOfRangeException(
                    $"The range of {nameof(maximum)} - {nameof(minimum)} is Infinity.");
            }

            var next = (distribution ?? Distribution.Uniform).NextDouble(random);
            return minimum + (next * (maximum - minimum));
        }

        /// <summary>
        /// Customizes how the <see cref="AnonymousData"/> creates objects.
        /// </summary>
        /// <param name="customization">The customization to apply.</param>
        /// <returns>The current <see cref="AnonymousData"/> to allow for method call chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="customization"/> is <see langword="null"/>.</exception>
        public AnonymousData Customize(IAnonymousDataCustomization customization)
        {
            Argument.NotNull(customization, nameof(customization));

            customizations.Add(customization);
            return this;
        }

        /// <summary>
        /// Gets the value with the registered key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value with the registered key.</returns>
        public object GetValue(string key)
        {
            Argument.NotNull(key, nameof(key));

            if (registeredValues.TryGetValue(key, out object value))
            {
                return value;
            }

            return null;
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
                        where (prop.PropertyType.IsCollectionType()
                            || (prop.CanWrite && prop.SetMethod.IsPublic && IsDefaultValue(current, prop)))
                            && !(prop.GetIndexParameters()?.Any() ?? false)
                        select prop;
                    foreach (var prop in properties)
                    {
                        try
                        {
                            if (prop.PropertyType.IsCollectionType())
                            {
                                var collection = (IEnumerable)prop.GetValue(current);
                                if (collection != null)
                                {
                                    var items = collection.Cast<object>();
                                    if (!items.Any())
                                    {
                                        var method = prop.PropertyType.GetAddMethod();
                                        if (method != null)
                                        {
                                            var valueType = method.GetParameters().First().ParameterType;
                                            foreach (var item in this.AnyEnumerable(valueType))
                                            {
                                                try
                                                {
                                                    method.Invoke(collection, new[] { item });
                                                }
                                                catch (Exception)
                                                {
                                                    break;
                                                }

                                                if (deep && !item.GetType().GetTypeInfo().IsPrimitive)
                                                {
                                                    queue.Enqueue(item);
                                                }
                                            }
                                        }
                                    }
                                    else if (deep)
                                    {
                                        foreach (var item in items)
                                        {
                                            if (item != null && !item.GetType().GetTypeInfo().IsPrimitive)
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
                                            if (item != null && !item.GetType().GetTypeInfo().IsPrimitive)
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
                                if (propertyFactories.TryGetValue(prop, out Factory factory)
                                    || GlobalPropertyFactories.TryGetValue(prop, out factory))
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
        /// <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="factory"/> is
        /// <see langword="null"/>.</exception>
        public void Register(Type type, Factory factory)
        {
            Argument.NotNull(type, nameof(type));
            Argument.NotNull(factory, nameof(factory));

            factories[type] = factory;
        }

        /// <summary>
        /// Registers a factory method for the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property to populate.</param>
        /// <param name="factory">The factory method.</param>
        public void Register(PropertyInfo propertyInfo, Factory factory)
        {
            Argument.NotNull(propertyInfo, nameof(propertyInfo));
            Argument.NotNull(factory, nameof(factory));

            propertyFactories[propertyInfo] = factory;
        }

        /// <summary>
        /// Sets the value with the registered key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetValue(string key, object value) => registeredValues[key] = value;

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
                result = BuildArray(itemType);
                return true;
            }

            if (type.Is(typeof(IEnumerable<>)))
            {
                var itemType = type.GetGenericTypeArgument(0);
                result = BuildEnumerable(itemType);
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
                result = ctor.Invoke(EmptyArgs);
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
                Populate(instance, populateOption == PopulateOption.Deep);
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
                ResultType = resultType;
                current = this.factory.customizations.Count;
            }

            public Type ResultType { get; }

            public object Any(Type type, PopulateOption populateOption)
            {
                Argument.NotNull(type, nameof(type));

                return factory.Any(type, populateOption);
            }

            public double AnyDouble(double minimum, double maximum, Distribution distribution)
            {
                Argument.InRange(
                    maximum,
                    minimum,
                    double.MaxValue,
                    nameof(maximum),
                    "The maximum value must be greater than the minimum value.");

                return factory.AnyDouble(minimum, maximum, distribution);
            }

            public bool CallNextCustomization(out object result)
            {
                if (current != 0)
                {
                    return factory.customizations[--current].Create(this, out result);
                }

                if (ResultType.IsType(typeof(IEnumerable)))
                {
                    return factory.BuildCollection(ResultType, out result);
                }

                if (ResultType.IsInterface() || ResultType.IsAbstract())
                {
                    result = null;
                    return false;
                }

                return factory.Any(ResultType, out result);
            }

            /// <summary>
            /// Gets the value with the registered key.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <returns>The value with the registered key.</returns>
            public object GetValue(string key) => factory.GetValue(key);

            /// <summary>
            /// Populates the specified instance by assigning all properties to anonymous values.
            /// </summary>
            /// <typeparam name="TInstance">The type of the instance to populate.</typeparam>
            /// <param name="instance">The instance to populate.</param>
            /// <param name="deep">If set to <see langword="true" /> then properties are assigned recursively,
            /// populating the entire object tree.</param>
            /// <returns>The populated instance.</returns>
            public TInstance Populate<TInstance>(TInstance instance, bool deep) => factory.Populate(instance, deep);
        }

        private class PropertyComparer : IEqualityComparer<PropertyInfo>
        {
            public static PropertyComparer Default { get; } = new PropertyComparer();

            public bool Equals(PropertyInfo x, PropertyInfo y) => GetData(x).Equals(GetData(y));

            public int GetHashCode(PropertyInfo obj) => GetData(obj).GetHashCode();

            private static (Type, string) GetData(PropertyInfo info) => (info.ReflectedType, info.Name);
        }
    }
}
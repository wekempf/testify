using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;
using Xunit.Abstractions;

namespace Testify
{
    public class AnonymousDataTestsFixture
    {
        public AnonymousDataTestsFixture()
        {
            AnonymousData.RegisterDefault<GloballyRegisteredModel>(f => new GloballyRegisteredModel { Value = "xyzzy" });
            AnonymousData.RegisterDefault((GloballyRegisteredModel m) => m.Value, f => "xyzzy");
        }

        public class GloballyRegisteredModel
        {
            public string Value { get; set; }
        }
    }

    public class AnonymousDataTests : IClassFixture<AnonymousDataTestsFixture>
    {
        private readonly ITestOutputHelper tracer;

        public AnonymousDataTests(ITestOutputHelper tracer)
        {
            this.tracer = tracer;
        }

        [Fact]
        public void Any_ArrayOfType()
        {
            var anon = new AnonymousData();

            var result = (Model[])anon.Any(typeof(Model[]));

            Assert.NotNull(result);
        }

        [Fact]
        public void Any_ConcreteTypeNotRegistered()
        {
            var anon = new AnonymousData();

            var result = (Model)anon.Any(typeof(Model));

            Assert.NotNull(result);
        }

        [Fact]
        public void Any_Double()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<double>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => (double)anon.Any(typeof(double)));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void Any_EnumerableOfType()
        {
            var anon = new AnonymousData();

            var result = (IEnumerable<Model>)anon.Any(typeof(IEnumerable<Model>));

            Assert.NotNull(result);
        }

        [Fact]
        public void Any_FlagsEnum()
        {
            var anon = new AnonymousData();

            var result = (FlagsEnum)anon.Any(typeof(FlagsEnum));
        }

        [Fact]
        public void Any_ListOfType()
        {
            var anon = new AnonymousData();

            var result = (List<Model>)anon.Any(typeof(List<Model>));

            Assert.NotNull(result);
        }

        [Fact]
        public void Any_ModelList()
        {
            var anon = new AnonymousData();

            var result = (ModelList)anon.Any(typeof(ModelList));

            Assert.NotNull(result);
        }

        [Fact]
        public void Any_SimpleEnum()
        {
            var anon = new AnonymousData();

            var result = (SimpleEnum)anon.Any(typeof(SimpleEnum));
        }

        [Fact]
        public void Any_TupleIntString()
        {
            var anon = new AnonymousData();

            var result = (Tuple<int, string>)anon.Any(typeof(Tuple<int, string>));

            Assert.NotNull(result);
        }

        [Fact]
        public void AnyAnonymousDataReturnsSelf()
        {
            var anon = new AnonymousData();

            var result = anon.Any<AnonymousData>();

            Assert.Same(anon, result);
        }

        [Fact]
        public void AnyDouble_MinMaxDistribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<double>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            classifier.Classify(() => anon.AnyDouble(float.MinValue, float.MaxValue, Distribution.Uniform));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyDoubleNullDistribution()
        {
            var anon = new AnonymousData();
            var classifier = new Classifier<double>();
            classifier.AddClassification("GT", d => d > 0);
            classifier.AddClassification("LT", d => d < 0);

            var rand = new Random();
            classifier.Classify(() => anon.AnyDouble(float.MinValue, float.MaxValue, null));

            Assert.True(classifier["GT"] > 0.4);
            Assert.True(classifier["LT"] > 0.4);
        }

        [Fact]
        public void AnyIAnonymousDataReturnsSelf()
        {
            var anon = new AnonymousData();

            var result = anon.Any<IAnonymousData>();

            Assert.Same(anon, result);
        }

        [Fact]
        public void AnyType()
        {
            var anon = new AnonymousData();

            var result = anon.Any(typeof(IAnonymousData));

            Assert.Same(anon, result);
        }

        [Fact]
        public void AnyUnregisteredInterfaceShouldThrow()
        {
            var anon = new AnonymousData();

            try
            {
                anon.Any<INotifyPropertyChanged>();
            }
            catch (AnonymousDataException e)
            {
                Assert.Equal(typeof(INotifyPropertyChanged), e.AnonymousType);
                return;
            }

            Assertions.Fail("Any did not throw.");
        }

        [Fact]
        public void FreezeTypeValue()
        {
            var anon = new AnonymousData();

            anon.Freeze(typeof(int), 42);
            var result = anon.Any<int>();

            Assert.Equal(42, result);
        }

        [Fact]
        public void FreezeValue()
        {
            var anon = new AnonymousData();

            anon.Freeze(42);
            var result = anon.Any<int>();

            Assert.Equal(42, result);
        }

        [Fact]
        public void Customize()
        {
            var anon = new AnonymousData();
            anon.Customize(new Customization());

            var result = (IModel)anon.Any(typeof(IModel));

            Assert.NotNull(result);
        }

        [Fact]
        public void Register()
        {
            var anon = new AnonymousData();

            anon.Register<IModel>(f => new Model());
            var result = (IModel)anon.Any(typeof(IModel));

            Assert.NotNull(result);
        }

        [Fact]
        public void RegisterDefault()
        {
            var anon = new AnonymousData();

            // Registration happens in the class fixture AnonymousDataTestsFixture.
            var result = anon.Any<AnonymousDataTestsFixture.GloballyRegisteredModel>();

            Assert.Equal("xyzzy", result.Value);
        }

        [Fact]
        public void Register_PropertyExpression()
        {
            var anon = new AnonymousData();
            var model = anon.Any<Model>();

            anon.Register((DeepModel m) => m.Model, a => model);
            var result = anon.Any<DeepModel>(PopulateOption.Deep);

            Assert.Same(model, result.Model);
        }

        [Fact]
        public void RegisterDefault_PropertyExpression()
        {
            var anon = new AnonymousData();
            var model = new AnonymousDataTestsFixture.GloballyRegisteredModel();

            // Registration happens in the class fixture AnonymousDataTestsFixture.
            anon.Populate(model);

            Assert.Equal("xyzzy", model.Value);
        }

        [Fact]
        public void Any_Populate_ShouldPopulateFirstLevel()
        {
            var anon = new AnonymousData();

            var result = anon.Any<DeepModel>();
            anon.Populate(result);

            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.Equal(result.Model.Value, nameof(Model));
        }

        [Fact]
        public void Any_PopulateShallow_ShouldPopulateFirstLevel()
        {
            var anon = new AnonymousData();

            var result = anon.Any<DeepModel>(PopulateOption.Shallow);

            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.Equal(result.Model.Value, nameof(Model));
        }

        [Fact]
        public void Any_PopulateDeep_ShouldPopulateAllLevels()
        {
            var anon = new AnonymousData();
            anon.Freeze("xyzzy");

            var result = anon.Any<DeepModel>(PopulateOption.Deep);

            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.Equal("Model", result.Model.Value);
            Assert.Equal("xyzzy", result.Model.Unset);
            Assert.True(result.Model.PrimitiveWasSet);
        }

        [Fact]
        public void Any_PopulateDeepWithCollections_ShouldPopulateCollections()
        {
            var anon = new AnonymousData();

            var result = anon.Any<DeepModelWithCollection>(PopulateOption.Deep);

            Assert.NotNull(result);
            Assert.NotEmpty(result.ReadOnlyNames);
            Assert.NotEmpty(result.InitializedNames);
            Assert.NotEmpty(result.UninitializedNames);
            Assert.NotEmpty(result.Dictionary);
        }

        [Flags]
        private enum FlagsEnum
        {
            None = 0x0,
            First = 0x1,
            Second = 0x2,
            Third = 0x4
        }

        private enum SimpleEnum
        {
            One,
            Two,
            Three
        }

        private interface IModel
        {
            string Value { get; set; }
        }

        private class Customization : IAnonymousDataCustomization
        {
            public bool Create(IAnonymousDataContext context, out object result)
            {
                if (context.ResultType == typeof(IModel))
                {
                    var model = new Model();
                    model.Value = context.AnyDouble(0, 1, Distribution.Uniform).ToString()
                        + context.Any(typeof(string));
                    result = model;
                    return true;
                }

                return context.CallNextCustomization(out result);
            }
        }

        private class Model : IModel
        {
            private int primitive;

            public string Value { get; set; } = nameof(Model);

            public string Unset { get; set; }

            public int Primitive
            {
                get
                {
                    return primitive;
                }

                set
                {
                    primitive = value;
                    PrimitiveWasSet = true;
                }
            }

            public bool PrimitiveWasSet { get; private set; }
        }

        private class ModelList : List<Model>
        {
        }

        private class DeepModel
        {
            public Model Model { get; set; }
        }

        private class DeepModelWithCollection
        {
            public List<string> ReadOnlyNames { get; } = new List<string>();

            public List<string> InitializedNames { get; set; } = new List<string>();

            public List<string> UninitializedNames { get; set; }

            public Model[] InitializedArray { get; set; } = new Model[2];

            public Model[] UninitializedArray { get; set; }

            public Dictionary<string, int> Dictionary { get; } = new Dictionary<string, int>();
        }
    }
}
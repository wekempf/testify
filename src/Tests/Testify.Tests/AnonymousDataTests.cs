using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace Testify
{
    public class ObjectFactoryTests
    {
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

            classifier.Classify(() => anon.AnyDouble(double.MinValue, double.MaxValue, Distribution.Uniform));

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

            classifier.Classify(() => anon.AnyDouble(double.MinValue, double.MaxValue, null));

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
            Assert.Equal(result.Model.Value, "xyzzy");
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
            public string Value { get; set; } = nameof(Model);
        }

        private class ModelList : List<Model>
        {
        }

        private class DeepModel
        {
            public Model Model { get; set; }
        }
    }
}
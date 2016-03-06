using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Testify
{
    public class EnumerableFactoryTests
    {
        [Fact]
        public void TestAnyEnumerable_Type()
        {
            var anon = new AnonymousData();

            var result = anon.AnyEnumerable(typeof(int));

            Assert.NotNull(result);
        }

        [Fact]
        public void TestAnyEnumerable_TypeMinMaxLength()
        {
            var anon = new AnonymousData();

            var result = anon.AnyEnumerable(typeof(int), 4, 10);
            var count = result.OfType<int>().Count();

            Assert.NotNull(result);
            Assert.True(count >= 4 && count <= 10);
        }

        [Fact]
        public void TestAnyEnumerableOfT()
        {
            var anon = new AnonymousData();

            var result = anon.AnyEnumerable<int>();

            Assert.NotNull(result);
        }

        [Fact]
        public void TestAnyEnumerableT_MinMaxLength()
        {
            var anon = new AnonymousData();

            var result = anon.AnyEnumerable<int>(4, 10);
            var count = result.Count();

            Assert.NotNull(result);
            Assert.True(count >= 4 && count <= 10);
        }

        [Fact]
        public void AnyIEnumerableCreatesItems()
        {
            var anon = new AnonymousData();

            var result = anon.Any<IEnumerable<int>>();

            Assert.NotNull(result);
        }

        [Fact]
        public void AnyListCreatesItems()
        {
            var anon = new AnonymousData();

            var result = anon.Any<List<int>>();

            Assert.NotNull(result);
        }

        [Fact]
        public void AnyCustomCollectionCreatesItems()
        {
            var anon = new AnonymousData();

            var result = anon.Any<CustomCollection>();

            Assert.True(result.Any());
        }

        [Fact]
        public void AnyItem()
        {
            var anon = new AnonymousData();

            var result = anon.AnyItem(Enumerable.Range(0, 10));

            Assert.InRange(result, 0, 10);
        }

        private class CustomCollection : IEnumerable<int>
        {
            private readonly List<int> items = new List<int>();

            public void Add(int item)
            {
                this.items.Add(item);
            }

            public IEnumerator<int> GetEnumerator() => this.items.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }
    }
}
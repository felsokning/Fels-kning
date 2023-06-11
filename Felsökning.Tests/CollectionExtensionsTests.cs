// ----------------------------------------------------------------------
// <copyright file="CollectionExtensionsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public async Task ListOfType_ToAsyncEnumerable_ShouldReturnIAsyncEnumerable()
        {
            var sut = new List<int> { 1, 2, 3, 4 };

            var results = sut.ToIAsyncEnumerable<int>();

            await foreach (var item in results)
            {
                item.Should().BeGreaterThan(0);
            }
        }

        [TestMethod]
        public async Task CollectionOfType_ToAsyncEnumerable_ShouldReturnIAsyncEnumerable()
        {
            var collection = new Collection<int> { 1, 2, 3, 4 };

            var sut = (ICollection<int>)collection;

            var results = sut.ToIAsyncEnumerable<int>();

            await foreach (var item in results)
            {
                item.Should().BeGreaterThan(0);
            }
        }

        [TestMethod]
        public async Task EnumerableOfType_ToAsyncEnumerable_ShouldReturnIAsyncEnumerable()
        {
            var list = new List<int> { 1, 2, 3, 4 };

            var sut = (IEnumerable<int>)list;

            var results = sut.ToIAsyncEnumerable();

            await foreach (var item in results)
            {
                item.Should().BeGreaterThan(0);
            }
        }
    }
}
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

        [TestMethod]
        public void CollectionOfType_OrderByChained_ShouldReturnIOrderedEnumerable()
        {
            var list = new Collection<SampleJson>
            {
                new() { Id = 1, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "B Test Title", UserId = 1 },
            };

            var sut = (ICollection<SampleJson>)list;
            var result = sut.OrderByChained([x => x.Id, x => x.UserId]);
            result.Should().NotBeEmpty();
            var resultingList = result.ToList();
            var firstRow = resultingList[0];
            firstRow.Id.Should().Be(1);
            firstRow.UserId.Should().Be(0);
            var secondRow = resultingList[1];
            secondRow.Id.Should().Be(2);
            secondRow.UserId.Should().Be(0);
            var thirdRow = resultingList[2];
            thirdRow.Id.Should().Be(2);
            thirdRow.UserId.Should().Be(1);
        }

        [TestMethod]
        public void EnumerableOfType_OrderByChained_ShouldReturnIOrderedEnumerable()
        {
            var list = new List<SampleJson>
            {
                new() { Id = 1, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "B Test Title", UserId = 1 },
            };

            var sut = (IEnumerable<SampleJson>)list;

            var result = sut.OrderByChained([x => x.Id, x => x.UserId]);
            result.Should().NotBeEmpty();
            var resultingList = result.ToList();
            var firstRow = resultingList[0];
            firstRow.Id.Should().Be(1);
            firstRow.UserId.Should().Be(0);
            var secondRow = resultingList[1];
            secondRow.Id.Should().Be(2);
            secondRow.UserId.Should().Be(0);
            var thirdRow = resultingList[2];
            thirdRow.Id.Should().Be(2);
            thirdRow.UserId.Should().Be(1);
        }

        [TestMethod]
        public void ListOfType_OrderByChained_ShouldReturnIOrderedEnumerable()
        {
            var list = new List<SampleJson>
            {
                new() { Id = 1, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "B Test Title", UserId = 1 },
            };

            var result = list.OrderByChained([x => x.Id, x => x.UserId]);
            result.Should().NotBeEmpty();
            var resultingList = result.ToList();
            var firstRow = resultingList[0];
            firstRow.Id.Should().Be(1);
            firstRow.UserId.Should().Be(0);
            var secondRow = resultingList[1];
            secondRow.Id.Should().Be(2);
            secondRow.UserId.Should().Be(0);
            var thirdRow = resultingList[2];
            thirdRow.Id.Should().Be(2);
            thirdRow.UserId.Should().Be(1);
        }

        [TestMethod]
        public void CollectionOfType_OrderByDescendingChained_ShouldReturnIOrderedEnumerable()
        {
            var list = new Collection<SampleJson>
            {
                new() { Id = 1, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "B Test Title", UserId = 1 },
            };

            var sut = (ICollection<SampleJson>)list;
            var result = sut.OrderByDescendingChained([x => x.Id, x => x.UserId]);
            result.Should().NotBeEmpty();
            var resultingList = result.ToList();
            var firstRow = resultingList[0];
            firstRow.Id.Should().Be(2);
            firstRow.UserId.Should().Be(1);
            var secondRow = resultingList[1];
            secondRow.Id.Should().Be(2);
            secondRow.UserId.Should().Be(0);
            var thirdRow = resultingList[2];
            thirdRow.Id.Should().Be(1);
            thirdRow.UserId.Should().Be(0);
        }

        [TestMethod]
        public void EnumerableOfType_OrderByDescendingChained_ShouldReturnIOrderedEnumerable()
        {
            var list = new List<SampleJson>
            {
                new() { Id = 1, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "B Test Title", UserId = 1 },
            };

            var sut = (IEnumerable<SampleJson>)list;

            var result = sut.OrderByDescendingChained([x => x.Id, x => x.UserId]);
            result.Should().NotBeEmpty();
            var resultingList = result.ToList();
            var firstRow = resultingList[0];
            firstRow.Id.Should().Be(2);
            firstRow.UserId.Should().Be(1);
            var secondRow = resultingList[1];
            secondRow.Id.Should().Be(2);
            secondRow.UserId.Should().Be(0);
            var thirdRow = resultingList[2];
            thirdRow.Id.Should().Be(1);
            thirdRow.UserId.Should().Be(0);
        }

        [TestMethod]
        public void ListOfType_OrderByDescendingChained_ShouldReturnIOrderedEnumerable()
        {
            var list = new List<SampleJson>
            {
                new() { Id = 1, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "A Test Title", UserId = 0 },
                new() { Id = 2, Completed = true, Title = "B Test Title", UserId = 1 },
            };

            var result = list.OrderByDescendingChained([x => x.Id, x => x.UserId]);
            result.Should().NotBeEmpty();
            var resultingList = result.ToList();
            var firstRow = resultingList[0];
            firstRow.Id.Should().Be(2);
            firstRow.UserId.Should().Be(1);
            var secondRow = resultingList[1];
            secondRow.Id.Should().Be(2);
            secondRow.UserId.Should().Be(0);
            var thirdRow = resultingList[2];
            thirdRow.Id.Should().Be(1);
            thirdRow.UserId.Should().Be(0);
        }
    }
}
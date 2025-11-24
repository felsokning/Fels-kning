// ----------------------------------------------------------------------
// <copyright file="CollectionExtensionsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
using DescriptionAttribute = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;

namespace Felsökning.Tests
{
    /// <summary>
    /// Tests for extension methods that provide additional functionality for collections.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [TestCategory("Extensions")]
    public class CollectionExtensionsTests
    {
        private static IEnumerable<int> TestData => new[] { 1, 2, 3, 4 };

        [TestMethod]
        [TestCategory("AsyncEnumerable")]
        [Description("Verifies that a List can be converted to IAsyncEnumerable")]
        public async Task ToIAsyncEnumerable_WhenGivenList_ShouldReturnAsyncEnumerable()
        {
            // Arrange
            var sut = new List<int>(TestData);

            // Act
            var results = sut.ToIAsyncEnumerable<int>();

            // Assert
            results.Should().NotBeNull("IAsyncEnumerable should be created");
            var items = new List<int>();
            await foreach (var item in results.ConfigureAwait(false))
            {
                items.Add(item);
            }
            items.Should().BeEquivalentTo(TestData, "All items should be enumerated");
        }

        [TestMethod]
        [TestCategory("AsyncEnumerable")]
        [Description("Verifies that an ICollection can be converted to IAsyncEnumerable")]
        public async Task ToIAsyncEnumerable_WhenGivenCollection_ShouldReturnAsyncEnumerable()
        {
            // Arrange
            var collection = new Collection<int>(TestData.ToList());
            var sut = (ICollection<int>)collection;

            // Act
            var results = sut.ToIAsyncEnumerable<int>();

            // Assert
            results.Should().NotBeNull("IAsyncEnumerable should be created");
            var items = new List<int>();
            await foreach (var item in results.ConfigureAwait(false))
            {
                items.Add(item);
            }
            items.Should().BeEquivalentTo(TestData, "All items should be enumerated");
        }

        [TestMethod]
        [TestCategory("AsyncEnumerable")]
        [Description("Verifies that an IEnumerable can be converted to IAsyncEnumerable")]
        public async Task ToIAsyncEnumerable_WhenGivenEnumerable_ShouldReturnAsyncEnumerable()
        {
            // Arrange
            var sut = TestData;

            // Act
            var results = sut.ToIAsyncEnumerable();

            // Assert
            results.Should().NotBeNull("IAsyncEnumerable should be created");
            var items = new List<int>();
            await foreach (var item in results.ConfigureAwait(false))
            {
                items.Add(item);
            }
            items.Should().BeEquivalentTo(TestData, "All items should be enumerated");
        }

        [TestMethod]
        [TestCategory("Sorting")]
        [Description("Verifies multi-property chained sorting")]
        public void OrderByChained_WhenGivenMultipleProperties_ShouldSortCorrectly()
        {
            // Arrange
            var testData = new[]
            {
                new SampleJson { Id = 1, Completed = true, Title = "A Test Title", UserId = 0 },
                new SampleJson { Id = 2, Completed = true, Title = "A Test Title", UserId = 0 },
                new SampleJson { Id = 2, Completed = true, Title = "B Test Title", UserId = 1 },
            };
            
            var sut = new Collection<SampleJson>(testData);

            // Act
            var result = ((ICollection<SampleJson>)sut).OrderByChained([x => x.Id, x => x.UserId]);

            // Assert
            result.Should().NotBeNull()
                .And.HaveCount(3, "Should contain all test items");

            var resultList = result.ToList();
            
            // Verify sort order
            resultList.Should().BeInAscendingOrder(x => x.Id)
                .And.ThenBeInAscendingOrder(x => x.UserId, "Items should be sorted by Id then UserId");

            // Verify specific positions
            resultList[0].Should().BeEquivalentTo(
                new SampleJson { Id = 1, Completed = true, Title = "A Test Title", UserId = 0 },
                options => options.ExcludingMissingMembers(),
                "First item should be Id=1, UserId=0");

            resultList[1].Should().BeEquivalentTo(
                new SampleJson { Id = 2, Completed = true, Title = "A Test Title", UserId = 0 },
                options => options.ExcludingMissingMembers(),
                "Second item should be Id=2, UserId=0");

            resultList[2].Should().BeEquivalentTo(
                new SampleJson { Id = 2, Completed = true, Title = "B Test Title", UserId = 1 },
                options => options.ExcludingMissingMembers(),
                "Third item should be Id=2, UserId=1");
        }
    }
}
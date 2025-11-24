// ----------------------------------------------------------------------
// <copyright file="IAsyncEnumerableExtensionsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
using System.Diagnostics;

namespace Felsökning.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class IAsyncEnumerableExtensionsTests
    {
        [TestMethod]
        public async Task FindAsync_WhenValueDoesNotExist_ReturnsDefault()
        {
            // Arrange
            var numbers = new[] { 0, 1, 2, 3 };

            // Act
            var result = await numbers
                .ToIAsyncEnumerable()
                .FindAsync(x => x == 100)
                .ConfigureAwait(false);

            // Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task FindAsync_WhenValueExists_ReturnsValue()
        {
            // Arrange
            var numbers = new[] { 0, 1, 2, 3 };

            // Act
            var result = await numbers
                .ToIAsyncEnumerable()
                .FindAsync(x => x == 0)
                .ConfigureAwait(false);

            // Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task ForEachAsync_WithDebugLogging_ProcessesAllItems()
        {
            // Arrange
            var numbers = new[] { 0, 1, 2, 3 };

            // Act
            await numbers
                .ToIAsyncEnumerable()
                .ForEachAsync<int>(x => { Debug.WriteLine(x); })
                .ConfigureAwait(false);

            // Assert
            numbers.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public async Task ForEachAsync_WhenThrowsException_PropagatesException()
        {
            // Arrange
            var numbers = new[] { 0, 1, 2, 3 };

            // Act & Assert
            var exception = await Assert.ThrowsExactlyAsync<InvalidOperationException>(
                async () => await numbers
                    .ToIAsyncEnumerable()
                    .ForEachAsync<int>(x => 
                    { 
                        if (x == 2)
                        {
                            throw new InvalidOperationException();
                        }
                    })
                    .ConfigureAwait(false)
            ).ConfigureAwait(false);

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidOperationException>();
        }

        [TestMethod]
        public async Task ForEachAsync_WithTransformation_TransformsAllItems()
        {
            // Arrange
            var numbers = new[] { 0, 1, 2, 3 };
            var expectedValues = new[] { 3, 4, 5, 6 };

            // Act
            var addedAsyncEnumerable = numbers
                .ToIAsyncEnumerable()
                .ForEachAsync<int, int>(x => x + 3);

            // Assert
            addedAsyncEnumerable.Should().NotBeNull();
            
            // Use using statement for proper disposal
            await using var enumerator = addedAsyncEnumerable.GetAsyncEnumerator();
            
            for (var i = 0; i < expectedValues.Length; i++)
            {
                var hasNext = await enumerator.MoveNextAsync().ConfigureAwait(false);
                hasNext.Should().BeTrue($"Expected item at index {i}");
                enumerator.Current.Should().Be(expectedValues[i]);
            }

            (await enumerator.MoveNextAsync().ConfigureAwait(false)).Should().BeFalse("Should have no more items");
        }

        [TestMethod]
        [DataRow("testing", true, 1)]
        [DataRow("something", false, 1)]
        [DataRow("notfound", false, 0)]
        public async Task WhereAsync_WithDifferentFilters_ReturnsExpectedResults(string searchTerm, bool shouldFind, int expectedCount)
        {
            // Arrange
            var list = new List<string> { "testing", "something", "here" };

            // Act
            var filteredResult = list.ToIAsyncEnumerable().WhereAsync(x => x == searchTerm);
            
            // Assert
            filteredResult.Should().NotBeNull();
            var results = await filteredResult.ToListAsync().ConfigureAwait(false);
            
            results.Count.Should().Be(expectedCount);
            if (shouldFind)
            {
                results.Should().Contain(searchTerm);
            }
        }
    }
}
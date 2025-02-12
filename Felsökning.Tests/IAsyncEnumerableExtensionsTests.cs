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
        public async Task FindAsync_DoesntFindValue()
        {
            var numbers = new[] { 0, 1, 2, 3, };

            var result = await numbers
                .ToIAsyncEnumerable()
                .FindAsync(x => x == 100);

            result.Should().Be(0);
        }

        [TestMethod]
        public async Task FindAsync_FindsValue()
        {
            var numbers = new[] { 0, 1, 2, 3, };

            var result = await numbers
                .ToIAsyncEnumerable()
                .FindAsync(x => x == 0);

            result.Should().Be(0);
        }

        [TestMethod]
        public async Task ForEachAsync_Type_DebugLog()
        {
            var numbers = new[] { 0, 1, 2, 3, };

            await numbers
                .ToIAsyncEnumerable()
                .ForEachAsync<int>(x => { Debug.WriteLine(x); });

            numbers.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public async Task ForEachAsync_Type_Throws()
        {
            var numbers = new[] { 0, 1, 2, 3, };

            var exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await numbers.ToIAsyncEnumerable().ForEachAsync<int>(x => 
            { 
                if (x == 2)
                {
                    throw new InvalidOperationException();
                }
            }));


            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidOperationException>();
        }

        [TestMethod]
        public async Task ForEachAsync_Type_Type_AddNumbersAsynchronously()
        {
            var numbers = new[] { 0, 1, 2, 3, };

            var addedAsyncEnumerbale = numbers
                .ToIAsyncEnumerable()
                .ForEachAsync<int, int>(x => { return x + 3; });

            addedAsyncEnumerbale.Should().NotBeNull();
            var enumerator = addedAsyncEnumerbale.GetAsyncEnumerator();
            await enumerator.MoveNextAsync();

            enumerator.Current.Should().BeGreaterThanOrEqualTo(3);

            await enumerator.MoveNextAsync();

            enumerator.Current.Should().BeGreaterThanOrEqualTo(4);

            await enumerator.MoveNextAsync();

            enumerator.Current.Should().BeGreaterThanOrEqualTo(5);

            await enumerator.MoveNextAsync();

            enumerator.Current.Should().BeGreaterThanOrEqualTo(6);

            await enumerator.MoveNextAsync();
        }

        [TestMethod]
        public void IAsyncEnumerable_Where_Succeeds()
        {
            var list = new List<string> { "testing", "something", "here" };

            var negativeResult = list.ToIAsyncEnumerable().WhereAsync(x => x == "Test");
            negativeResult.Should().NotBeNull();

            var negativeEnumerable = negativeResult.ToBlockingEnumerable();
            negativeEnumerable.Count().Should().Be(0);

            var positiveResult = list.ToIAsyncEnumerable().WhereAsync(x => x == "testing");
            positiveResult.Should().NotBeNull();
            
            var positiveEnumerable = positiveResult.ToBlockingEnumerable();
            positiveEnumerable.Count().Should().Be(1);
            positiveEnumerable.First().Should().Be("testing");
        }
    }
}
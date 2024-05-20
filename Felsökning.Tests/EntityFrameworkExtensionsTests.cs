namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class EntityFrameworkExtensionsTests
    {
        [TestMethod]
        public async Task EntityBy_ShouldSucceed()
        {
            using (var factory = new TestDbContextFactory())
            {
                // Create a context
                using (var context = factory.CreateContext())
                {
                    var consumer = new SetEntity
                    { 
                        Name = "InterfaceConsumer", 
                        Id = new Guid("02c53b6a-479b-4fb4-9840-258748a94e11"),
                        LastModified = DateTime.UnixEpoch
                    };

                    await context.Consumers.AddAsync(consumer);
                    await context.SaveChangesAsync();
                }

                // Create another context using the same connection
                using (var context = factory.CreateContext())
                {
                    var count = await context.Consumers.CountAsync();
                    count
                        .Should()
                        .Be(1);

                    var firstResultViaName = context.Consumers.EntityByPropertiesExists(
                        [
                            x => x.Name == "TotallyAFakeName", 
                            y => y.Id == new Guid("02c53b6a-479b-4fb4-9840-258748a94e11"),
                            z => z.LastModified == DateTime.UnixEpoch
                        ]);
                    firstResultViaName
                        .Should()
                        .BeFalse();

                    var seconResultViaName = context.Consumers.EntityByPropertiesExists(
                        [
                            x => x.Name == "InterfaceConsumer", 
                            y => y.Id == new Guid("02c53b6a-479b-4fb4-9840-258748a94e11"),
                            z => z.LastModified == DateTime.UnixEpoch
                        ]);
                    seconResultViaName
                        .Should()
                        .BeTrue();
                }
            }
        }
    }
}
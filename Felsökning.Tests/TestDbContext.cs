namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    public class TestDbContext : DbContext
    {
        public DbSet<SetEntity> Consumers {get;set;}

        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions<TestDbContext> options) 
            : base(options)
        {
        }
    }
}
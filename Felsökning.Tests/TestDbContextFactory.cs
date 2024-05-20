namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    public class TestDbContextFactory : IDisposable
    {
        private SqliteConnection? _connection;

        private DbContextOptions<TestDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlite(_connection!).Options;
        }

        public TestDbContext CreateContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                var options = CreateOptions();
                using (var context = new TestDbContext(options))
                {
                    context.Database.EnsureCreated();
                }
            }

            return new TestDbContext(CreateOptions());
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
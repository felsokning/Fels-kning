namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    public class SetEntity
    {
        public string Name { get;set; } = string.Empty;

        public Guid Id { get; set; } = Guid.Empty;

        public DateTime LastModified { get;set; }
    }
}
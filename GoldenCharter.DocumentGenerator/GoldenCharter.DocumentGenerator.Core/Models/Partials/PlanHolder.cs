
namespace GoldenCharter.DocumentGenerator.Core.Models.Partials
{
    public record PlanHolder
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public int Age { get; init; }
        public DateOnly Dob { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public Address? Address { get; init; }
    }
}

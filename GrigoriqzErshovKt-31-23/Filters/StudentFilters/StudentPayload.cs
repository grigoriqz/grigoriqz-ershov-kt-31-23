namespace GrigoriqzErshovKt_31_23.Filters.StudentFilters
{
    public class StudentPayload
    {
        public int? StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int GroupId { get; set; }
    }
}

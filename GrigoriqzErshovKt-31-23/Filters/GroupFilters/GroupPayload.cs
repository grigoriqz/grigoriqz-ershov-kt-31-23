namespace GrigoriqzErshovKt_31_23.Filters.GroupFilters
{
    public class GroupPayload
    {
        public int? GroupId { get; set; }
        public required string Name { get; set; }
        public int Course { get; set; }
        public int SpecialtyId { get; set; }
    }
}

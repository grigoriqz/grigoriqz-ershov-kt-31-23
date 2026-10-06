using System.Text.Json.Serialization;

namespace GrigoriqzErshovKt_31_23.Models
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }
        public required string Title { get; set; }
        public required string Code { get; set; }

        [JsonIgnore]
        public ICollection<Group> Groups { get; set; } = [];
    }
}

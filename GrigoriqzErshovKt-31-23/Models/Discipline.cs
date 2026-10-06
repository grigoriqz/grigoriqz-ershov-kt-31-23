using System.Text.Json.Serialization;

namespace GrigoriqzErshovKt_31_23.Models
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        public required string Name { get; set; }
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public ICollection<Grade> Grades { get; set; } = [];
    }
}

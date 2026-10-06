using System.Text.Json.Serialization;

namespace GrigoriqzErshovKt_31_23.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int Value { get; set; }
        public int StudentId { get; set; }
        public int DisciplineId { get; set; }

        [JsonIgnore]
        public Student Student { get; set; } = null!;

        [JsonIgnore]
        public Discipline Discipline { get; set; } = null!;

        public bool IsPassed()
        {
            return Value >= 3;
        }
    }
}

using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace GrigoriqzErshovKt_31_23.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public required string Name { get; set; }
        public int Course { get; set; }
        public int SpecialtyId { get; set; }
        public bool IsDeleted { get; set; }

        public Specialty Specialty { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Student> Students { get; set; } = [];

        public bool IsValidGroupName()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && Regex.Match(Name, @"\D*-\d*-\d\d").Success;
        }
    }
}

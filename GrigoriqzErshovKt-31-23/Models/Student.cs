namespace GrigoriqzErshovKt_31_23.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int GroupId { get; set; }
        public bool IsDeleted { get; set; }

        public Group Group { get; set; } = null!;
        public ICollection<Grade> Grades { get; set; } = [];
    }
}

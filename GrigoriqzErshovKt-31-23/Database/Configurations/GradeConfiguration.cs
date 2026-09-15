using GrigoriqzErshovKt_31_23.Database.Helpers;
using GrigoriqzErshovKt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrigoriqzErshovKt_31_23.Database.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");

            builder.HasKey(p => p.GradeId)
                .HasName("pk_grades_grade_id");

            builder.Property(p => p.GradeId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Value)
                .IsRequired()
                .HasColumnType(ColumnType.Int);

            builder.HasOne(p => p.Student)
                .WithMany(p => p.Grades)
                .HasForeignKey(p => p.StudentId)
                .HasConstraintName("fk_grades_student_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Discipline)
                .WithMany(p => p.Grades)
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_grades_discipline_id")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using GrigoriqzErshovKt_31_23.Database.Helpers;
using GrigoriqzErshovKt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrigoriqzErshovKt_31_23.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        private const int NameMaxLength = 100;

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(p => p.StudentId)
                .HasName("pk_students_student_id");

            builder.Property(p => p.StudentId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnType(ColumnType.String)
                .HasMaxLength(NameMaxLength);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnType(ColumnType.String)
                .HasMaxLength(NameMaxLength);

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false);

            builder.HasOne(p => p.Group)
                .WithMany(p => p.Students)
                .HasForeignKey(p => p.GroupId)
                .HasConstraintName("fk_students_group_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

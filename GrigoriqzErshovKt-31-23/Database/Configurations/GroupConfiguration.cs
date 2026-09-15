using GrigoriqzErshovKt_31_23.Database.Helpers;
using GrigoriqzErshovKt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrigoriqzErshovKt_31_23.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const int NameMaxLength = 100;

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");

            builder.HasKey(p => p.GroupId)
                .HasName("pk_groups_group_id");

            builder.Property(p => p.GroupId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType(ColumnType.String)
                .HasMaxLength(NameMaxLength);

            builder.Property(p => p.Course)
                .IsRequired()
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false);

            builder.HasOne(p => p.Specialty)
                .WithMany(p => p.Groups)
                .HasForeignKey(p => p.SpecialtyId)
                .HasConstraintName("fk_groups_specialty_id")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using GrigoriqzErshovKt_31_23.Database.Helpers;
using GrigoriqzErshovKt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrigoriqzErshovKt_31_23.Database.Configurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        private const int NameMaxLength = 200;

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.ToTable("Disciplines");

            builder.HasKey(p => p.DisciplineId)
                .HasName("pk_disciplines_discipline_id");

            builder.Property(p => p.DisciplineId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType(ColumnType.String)
                .HasMaxLength(NameMaxLength);

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false);
        }
    }
}

using GrigoriqzErshovKt_31_23.Database.Helpers;
using GrigoriqzErshovKt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrigoriqzErshovKt_31_23.Database.Configurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        private const int TitleMaxLength = 200;
        private const int CodeMaxLength = 50;

        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable("Specialtys");

            builder.HasKey(p => p.SpecialtyId)
                .HasName("pk_specialtys_specialty_id");

            builder.Property(p => p.SpecialtyId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnType(ColumnType.String)
                .HasMaxLength(TitleMaxLength);

            builder.Property(p => p.Code)
                .IsRequired()
                .HasColumnType(ColumnType.String)
                .HasMaxLength(CodeMaxLength);
        }
    }
}

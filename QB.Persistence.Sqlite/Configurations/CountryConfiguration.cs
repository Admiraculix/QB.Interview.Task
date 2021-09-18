using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QB.Domain.Models;

namespace QB.Persistence.Sqlite.Configurations
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.CountryId);

            builder.Property(c => c.CountryName)
                .HasMaxLength(2000)
                .HasColumnType("varchar");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QB.Domain.Models;

namespace QB.Persistence.Sqlite.Configurations
{
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.CityId);

            builder.Property(c => c.CityName)
                .HasMaxLength(2000)
                .HasColumnType("varchar");
                //.IsRequired();

            builder.Property(c => c.Population)
                .HasColumnType("int");

            builder
                .HasOne(s => s.State)
                .WithMany(c => c.Cities)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Cascade);
                //.IsRequired();
        }
    }
}

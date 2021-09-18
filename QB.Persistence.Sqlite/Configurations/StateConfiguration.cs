using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QB.Domain.Models;

namespace QB.Persistence.Sqlite.Configurations
{
    internal class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(c => c.StateId);

            builder.Property(c => c.StateName)
                .HasMaxLength(2000)
                .HasColumnType("varchar")
                .IsRequired();

            builder
                .HasOne(c => c.Country)
                .WithMany(s => s.States)
                .HasForeignKey(s => s.StateId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}

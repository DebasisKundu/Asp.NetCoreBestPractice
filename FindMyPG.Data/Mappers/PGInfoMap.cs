using FindMyPG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindMyPG.Data.Mappers
{
    public class PGInfoMap : IEntityTypeConfiguration<PGInfo>
    {
        public void Configure(EntityTypeBuilder<PGInfo> builder)
        {
            var table = builder.ToTable("PGInfo");

            table.HasKey(k => k.Id);

            table.HasMany(r => r.PGRooms)
                .WithOne(p => p.PGInfo)
                .HasForeignKey(k => k.PgInfoId);

            table.HasMany(p => p.PGPackages)
                .WithOne(p => p.PGInfo)
                .HasForeignKey(k => k.PGInfoId);

            table.HasMany(p => p.PGBookings)
                .WithOne(p => p.PGInfo)
                .HasForeignKey(k => k.PGInfoId);
        }
    }
}

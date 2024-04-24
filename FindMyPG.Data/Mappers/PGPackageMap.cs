using FindMyPG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Data.Mappers
{
    public class PGPackageMap : IEntityTypeConfiguration<PGPackage>
    {
        public void Configure(EntityTypeBuilder<PGPackage> builder)
        {
            var table = builder.ToTable("PGPackage");
            table.HasKey(k => k.Id);
            table.HasMany(b => b.PGBookings)
                .WithOne(b => b.PGPackage)
                .HasForeignKey(k => k.PackageId);
        }
    }
}

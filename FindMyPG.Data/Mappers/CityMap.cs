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
    public class CityMap : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            var table = builder.ToTable("City");

            table.HasKey(k => k.Id);

            table.HasMany(p => p.PGInfos)
                .WithOne(c => c.City)
                .HasForeignKey(k => k.CityId);

            table.HasMany(z => z.ZipCodes)
                .WithOne(c => c.City)
                .HasForeignKey(k => k.CityId);
        }
    }
}

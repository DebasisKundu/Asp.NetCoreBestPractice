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
    public class ZipCodeMap : IEntityTypeConfiguration<ZipCode>
    {
        public void Configure(EntityTypeBuilder<ZipCode> builder)
        {
            builder.ToTable("ZipCode");

            builder.HasKey(k => k.Id);

            builder.HasMany(p => p.PGInfos)
               .WithOne(z => z.ZipCode)
               .HasForeignKey(k => k.ZipId);
        }
    }
}

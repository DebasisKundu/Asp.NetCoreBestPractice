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
    public class PGRoomMap : IEntityTypeConfiguration<PGRoom>
    {
        public void Configure(EntityTypeBuilder<PGRoom> builder)
        {
            builder.ToTable("PGRoom");

            builder.HasKey(k => k.Id);

            builder.HasMany(b => b.PGBookings)
                .WithOne(p => p.PGRoom)
                .HasForeignKey(k => k.PGRoomId);
        }
    }
}

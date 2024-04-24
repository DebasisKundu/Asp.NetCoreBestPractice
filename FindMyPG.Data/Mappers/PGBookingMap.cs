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
    public class PGBookingMap : IEntityTypeConfiguration<PGBooking>
    {
        public void Configure(EntityTypeBuilder<PGBooking> builder)
        {
            builder.ToTable("PGBooking")
                .HasKey(k => k.Id);
        }
    }
}

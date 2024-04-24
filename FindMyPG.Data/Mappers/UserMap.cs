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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var table = builder.ToTable("User");

            table.HasKey(t => t.Id);

            table.HasIndex(p => p.PhoneNumber).IsUnique(true);

            table.HasOne(b => b.PGBooking)
                .WithOne(b => b.User)
                .HasForeignKey<PGBooking>(k => k.SeekerId);

            table.HasMany(p => p.PGInfos)
                .WithOne(u => u.User)
                .HasForeignKey(k => k.OwnerId);

        }
    }
}

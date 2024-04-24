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
    public class StateMap : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("State");

            builder.HasKey(k => k.Id);

            builder.HasMany(p => p.PGInfos)
                .WithOne(p => p.State)
                .HasForeignKey(s => s.StateId);

            builder.HasMany(p => p.Cities)
                .WithOne(c => c.State)
                .HasForeignKey(k => k.StateId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class JourneyConfiguration : IEntityTypeConfiguration<Journey>
    {
        public void Configure(EntityTypeBuilder<Journey> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.Origin).IsRequired().HasMaxLength(10);

            builder.Property(x => x.Destination).IsRequired().HasMaxLength(10);

            builder.Property(x => x.Price).HasColumnType("double");

            builder.HasMany(u => u.Flights).WithMany(r => r.Journeys).UsingEntity<JourneyFlight>(
                x => x.HasOne(ur => ur.Flights).WithMany(r => r.JourneyFlights).HasForeignKey(ur => ur.IdFlightFk),
                x => x.HasOne(ur => ur.Journeys).WithMany(u => u.JourneyFlights).HasForeignKey(ur => ur.IdJourneyFk),
                x =>
                {
                    x.ToTable("journeyflight");
                    x.HasKey(y => new { y.IdJourneyFk, y.IdFlightFk });
                }
            );
        }
    }
}
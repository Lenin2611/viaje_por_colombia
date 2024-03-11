using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class JourneyFlightConfiguration : IEntityTypeConfiguration<JourneyFlight>
    {
        public void Configure(EntityTypeBuilder<JourneyFlight> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.HasOne(x => x.Journeys).WithMany(x => x.JourneyFlights).HasForeignKey(x => x.IdJourneyFk);
            
            builder.HasOne(x => x.Flights).WithMany(x => x.JourneyFlights).HasForeignKey(x => x.IdFlightFk);
        }
    }
}
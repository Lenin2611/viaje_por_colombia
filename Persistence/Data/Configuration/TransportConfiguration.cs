using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class TransportConfiguration : IEntityTypeConfiguration<Transport>
    {
        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.FlightCarrier).IsRequired().HasMaxLength(10);

            builder.Property(x => x.FlightNumber).IsRequired().HasMaxLength(10);

            builder.HasOne(x => x.Flights).WithMany(x => x.Transports).HasForeignKey(x => x.IdFlightFk);
        }
    }
}
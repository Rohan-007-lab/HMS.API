using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable("Bills");

        builder.Property(b => b.ConsultationFee).HasColumnType("decimal(10,2)");
        builder.Property(b => b.MedicineFee).HasColumnType("decimal(10,2)");
        builder.Property(b => b.OtherCharges).HasColumnType("decimal(10,2)");
        builder.Property(b => b.TotalAmount).HasColumnType("decimal(10,2)");
        builder.Property(b => b.Status).HasConversion<string>().HasMaxLength(20);

        builder.HasOne(b => b.Patient)
            .WithMany(p => p.Bills)
            .HasForeignKey(b => b.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

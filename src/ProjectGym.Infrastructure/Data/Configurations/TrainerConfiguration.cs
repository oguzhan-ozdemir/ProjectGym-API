using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectGym.Domain.Entities;

namespace ProjectGym.Infrastructure.Data.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.ToTable("Trainers");

        builder.HasKey(t=>t.Id);

        builder.Property(t=>t.FirstName).IsRequired().HasMaxLength(50);

        builder.Property(t=>t.LastName).IsRequired().HasMaxLength(50);

        builder.Property(t=>t.Specialty).IsRequired().HasMaxLength(100);

        builder.HasMany(t=>t.WorkoutSessions)
            .WithOne(ws=>ws.Trainer)
            .HasForeignKey(ws=>ws.TrainerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

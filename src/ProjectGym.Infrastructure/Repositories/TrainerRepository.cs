using System;
using ProjectGym.Domain.Entities;
using ProjectGym.Domain.Interfaces;
using ProjectGym.Infrastructure.Data;

namespace ProjectGym.Infrastructure.Repositories;

public class TrainerRepository : Repository<Trainer>, ITrainerRepository
{
    public TrainerRepository(ProjectGymDbContext context) : base(context)
    {
    }
}

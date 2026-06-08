using System;
using ProjectGym.Domain.Entities;

namespace ProjectGym.Domain.Interfaces;

public interface IWorkoutSessionRepository : IRepository<WorkoutSession>
{
    Task<WorkoutSession?> GetWithTrainerAndAttendancesAsync(int id, CancellationToken cancellationToken = default);

    Task<(IEnumerable<WorkoutSession> Items, int TotalCount)> GetUpcomingPagedAsync(
        int pageNumber,
        int pageSize,
        DateTime? fromUtc = null,
        CancellationToken cancellationToken = default
    );
}

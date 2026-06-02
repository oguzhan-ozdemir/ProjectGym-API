using System;
using ProjectGym.Domain.Entities;

namespace ProjectGym.Domain.Interfaces;

public interface IAttendanceRepository : IRepository<Attendance>
{
    Task<int> CountRegisteredAsync(int workoutSessionId, CancellationToken cancellationToken = default);

    Task<bool> ExistsRegisteredAsync(int memberId, int workoutSessionId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Attendance>> GetByMemberIdAsync(int memberId, CancellationToken cancellationToken = default);
}

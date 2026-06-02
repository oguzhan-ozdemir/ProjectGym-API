using System;
using ProjectGym.Domain.Entities;

namespace ProjectGym.Domain.Interfaces;

public interface IMembershipRepository : IRepository<Membership>
{
    Task<bool> HasOverlappingActiveMembershipAsync(
        int memberId,
        DateTime startDate,
        DateTime endDate,
        int? excludeMembershipId = null,
        CancellationToken cancellationToken = default
    );

    Task<Membership?> GetActiveByMemberIdAsync(int memberId, CancellationToken cancellationToken = default);
}

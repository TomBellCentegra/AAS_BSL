using AAS_BSL.Domain.Dtos;

namespace AAS_BSL.Services.Subsription;

public interface ISubscriptionService
{
    Task<StatusResult> Process(SubscriptionRequestDto request);
}
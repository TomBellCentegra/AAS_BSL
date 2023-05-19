using AAS_BSL.Domain.Dtos;

namespace AAS_BSL.Services.HttpClient;

public interface IBslHttpClient
{
    Task<StatusResult> Subscribe(SubscriptionRequestDto request);

}
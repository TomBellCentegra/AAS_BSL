using AAS_BSL.Domain.Dtos;

namespace AAS_BSL.Services.Company;

public interface ICompanyService
{
    Task<Domain.Company.Company> CreateOrGet(SubscriptionRequest request);
    Task SetSubscribed(int companyId);
    Task<StatusResult> Update();
    Task<Domain.Company.Company> Get(int id);
    Task<StatusResult> UpdateSecret(SecretUpdateRequestDto request);
}
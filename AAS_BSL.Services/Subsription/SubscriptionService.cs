using AAS_BSL.Domain.Dtos;
using AAS_BSL.Domain.Enums;
using AAS_BSL.Services.Company;
using AAS_BSL.Services.HttpClient;

namespace AAS_BSL.Services.Subsription;

public class SubscriptionService: ISubscriptionService
{
    private readonly ICompanyService _companyService;
    private readonly IBslHttpClient _httpClient;

    public SubscriptionService(ICompanyService companyService, IBslHttpClient httpClient)
    {
        _companyService = companyService;
        _httpClient = httpClient;
    }

    public async Task<StatusResult> Process(SubscriptionRequestDto request)
    {
        if (request is null)
        {
            throw new ArgumentNullException("Request entity is null");
        }

        try
        {
            var company = await _companyService.CreateOrGet(request);

            if (company.IsSubscribed)
                return new StatusResult
                    { Status = Status.Done, Message = $"Company with id: {company.CompanyId} already subscribed" };
            
            var result = await _httpClient.Subscribe(request);

            if (result.Status != Status.Done)
                return new StatusResult { Status = Status.Failed, Message = result.Message };

            await _companyService.SetSubscribed(company.CompanyId);
            
            return new StatusResult
                { Status = Status.Done, Message = $"Company with id: {company.CompanyId} has been subscribed" };
            
        }
        catch (Exception e)
        {
            return new StatusResult { Status = Status.Failed, Message = e.Message };
        }
    }
}
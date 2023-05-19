namespace AAS_BSL.Domain.Dtos;

public class SubscriptionRequestDto
{
    public string CompanyName { get; set; }
    public string NepOrganization { get; set; }
    public string SharedKey { get; set; }
    public string SecretKey { get; set; }
}
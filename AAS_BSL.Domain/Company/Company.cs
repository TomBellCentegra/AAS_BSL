namespace AAS_BSL.Domain.Company;

public class Company
{
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string OrganizationId { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsSubscribed { get; set; }
    public Secret.Secret Secret { get; set; }
}
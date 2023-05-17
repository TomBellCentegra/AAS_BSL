namespace AAS_BSL.Domain.Secret;

public class Secret
{
    public int SecretId { get; set; }
    public string SharedKey { get; set; }
    public string SecretKey { get; set; }
    public IEnumerable<Company.Company> Companies { get; set; }
}
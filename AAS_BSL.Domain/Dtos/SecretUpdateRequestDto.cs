namespace AAS_BSL.Domain.Dtos;

public class SecretUpdateRequestDto
{
    public int CompanyId { get; set; }
    public string SharedKey { get; set; }
    public string SecretKey { get; set; }
}
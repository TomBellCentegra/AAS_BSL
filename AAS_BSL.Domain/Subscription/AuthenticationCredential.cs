namespace AAS_BSL.Domain.Subscription;

public class AuthenticationCredential
{
    public string authenticationType { get; set; } // Enum
    public string credentials { get; set; }
}
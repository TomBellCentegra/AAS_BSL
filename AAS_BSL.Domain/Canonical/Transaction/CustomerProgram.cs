namespace AAS_BSL.Domain.Canonical.Transaction;

public class CustomerProgram
{
    public string? id { get; set; }
    public string? accountId { get; set; }
    public string? accountName { get; set; }
    public IEnumerable<EligibleItem>? eligibleItems { get; set; }
    public string? maskedCardNumber { get; set; }
    public float? pointsAwarded { get; set; }
    public float? pointsRedeemed { get; set; }
    public string? programId { get; set; }
    public string? programType { get; set; } // Enum?
    public string? providerId { get; set; }
    public string? providerName { get; set; }
    
}
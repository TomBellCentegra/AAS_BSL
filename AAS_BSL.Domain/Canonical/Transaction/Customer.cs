namespace AAS_BSL.Domain.Canonical.Transaction;

public class Customer
{
    public string? id { get; set; }
    public DateWithOffset? birthdate { get; set; }
    public string? customerType { get; set; } // Enum?
    public string? customerTypeLabel { get; set; }
    public string? email { get; set; }
    public string? entryMethod { get; set; } // Enum?
    public string? entryMethodLabel { get; set; }
    public string? identifierData { get; set; }
    public string? infoValidationMeans { get; set; }// Enum?
    public string? infoValidationMeansLabel { get; set; }
    public string? name { get; set; }
    public string? phoneNumber { get; set; }
    public Address? postalAddress { get; set; }
    public Vehicle? vehicle { get; set; }
    
}
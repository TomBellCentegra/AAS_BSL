namespace AAS_BSL.Domain.Canonical.Transaction;

public class TaxExempt
{
    public Amount exemptAmount { get; set; }
    public string exemptHolderId { get; set; }
    public string exemptHolderName { get; set; }
    public Amount exemptTaxableAmount { get; set; }
    public bool isTenderExempt { get; set; }
    public string reasonCode { get; set; }
}
namespace AAS_BSL.Domain.Canonical.Transaction;

public class Totals
{
    public CustomProperty? customProperties { get; set; }
    public Amount? discountAmount { get; set; }
    public Amount? grandAmount { get; set; }
    public Amount? grossAmount { get; set; }
    public Amount? grossReturnsAmount { get; set; }
    public Amount? netAmount { get; set; }
    public Amount? returnItemsTaxExclusive { get; set; }
    public Amount? returnItemsTaxInclusive { get; set; }
    public Amount? taxExclusive { get; set; }
    public Amount? taxExemptAmount { get; set; }
    public Amount? taxInclusive { get; set; }
    public Amount? voidsAmount { get; set; }
    public WicInfo? wicInfo { get; set; }
}
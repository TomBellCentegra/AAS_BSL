namespace AAS_BSL.Domain.Canonical.Transaction;

public class Tax
{
    public Amount amount { get; set; }
    public CustomProperty? customProperties { get; set; }
    public string id { get; set; }
    public bool? isRefund { get; set; }
    public bool? isVoided { get; set; }
    public string name { get; set; }
    public string? sequenceNumber { get; set; }
    public string? taxAuthority { get; set; }
    public string? taxAuthorityDescription { get; set; }
    public TaxExempt? taxExempt { get; set; }
    public double? taxPercent { get; set; }
    public string? taxSaleTypeCode { get; set; }
    public string? taxType { get; set; }
    public string? taxTypeCode { get; set; }
    public Amount? taxableAmount { get; set; }
}
namespace AAS_BSL.Domain.Canonical.Transaction;

public class Order
{
    public string? orderChannel { get; set; }
    public string? orderChannelLabel { get; set; }
    public Category? orderMode { get; set; }
    public string? orderNumber { get; set; }
    public string? orderSource { get; set; }
    public string? referenceId { get; set; }
}
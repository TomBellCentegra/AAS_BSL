namespace AAS_BSL.Domain.Canonical.Transaction;

public class WicInfo
{
    public bool? isWICCVB { get; set; }
    public Amount notToExceedAmount { get; set; }
    public Amount notToExceedLostAmount { get; set; }
    public Amount wicAmount { get; set; }
    public UnitPriceQuantity? wicQuantity { get; set; }
}
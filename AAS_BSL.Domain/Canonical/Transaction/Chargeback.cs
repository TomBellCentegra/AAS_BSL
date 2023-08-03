namespace AAS_BSL.Domain.Canonical.Transaction;

public class Chargeback
{
    public Amount chargebackAmount { get; set; }
    public string chargebackId { get; set; }
    public string type { get; set; }
    public string typeLabel { get; set; }
}
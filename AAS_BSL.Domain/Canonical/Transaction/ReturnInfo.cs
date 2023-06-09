namespace AAS_BSL.Domain.Canonical.Transaction;

public class ReturnInfo
{
    public string disposalValue { get; set; }
    public string originalItemId { get; set; }
    public string returnId { get; set; }
    public string returnReason { get; set; }
}
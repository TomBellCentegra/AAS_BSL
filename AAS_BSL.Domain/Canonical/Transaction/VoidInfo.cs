namespace AAS_BSL.Domain.Canonical.Transaction;

public class VoidInfo
{
    public bool isAuto { get; set; }
    public bool isCostImpacted { get; set; }
    public DateWithOffset itemVoidTime { get; set; }
    public Person voidApprovedByEmployee { get; set; }
    public string voidReason { get; set; }
    public Person voidedByEmployee { get; set; }
}
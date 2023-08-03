namespace AAS_BSL.Domain.Canonical.Transaction;

public class Coupon
{
    public Amount amount { get; set; }
    public string description { get; set; }
    public string entryMethod { get; set; } // Enum?
    public string entryMethodLabel { get; set; }
    public string id { get; set; }
    public bool isVoided { get; set; }
    public string issueMethod { get; set; } // Enum?
    public string issueMethodLabel { get; set; }
    public IEnumerable<OperatorBypassApproval> operatorBypassApprovals { get; set; }
    public IEnumerable<string> participatingItems { get; set; } // TBD?
    public int quantity { get; set; }
    public string type { get; set; } // Enum?
    public string typeLabel { get; set; }
    public VoidInfo voidInfo { get; set; }
    
}
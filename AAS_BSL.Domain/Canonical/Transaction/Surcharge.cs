namespace AAS_BSL.Domain.Canonical.Transaction;

public class Surcharge
{
    public Amount amount { get; set; }
    public Amount amountUncollected { get; set; }
    public string id { get; set; }
    public bool isAutoApplied { get; set; }
    public bool isRefund { get; set; }
    public bool isVoided { get; set; }
    public string name { get; set; }
    public string surchargePriceType { get; set; }
    public List<Tax> surchargeTaxes { get; set; }
    public string surchargeType { get; set; }
    public string surchargeTypeLabel { get; set; }
    public VoidInfo voidInfo { get; set; }
}
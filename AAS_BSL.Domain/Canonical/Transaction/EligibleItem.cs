namespace AAS_BSL.Domain.Canonical.Transaction;

public class EligibleItem
{
    public string categoryCode { get; set; }
    public int claimPrice { get; set; }
    public bool isTenderBenefitEligible { get; set; }
    public string itemCode { get; set; }
    public int itemCodeLength { get; set; }
    public Amount itemDiscountAmount { get; set; }
    public IEnumerable<LineItemBreakdown> lineItemBreakdowns { get; set; }
    public int purchasedQuantity { get; set; }
    public int purchasedUnits { get; set; }
    public string subCategoryCode { get; set; }
    public string unitCategory { get; set; }
}
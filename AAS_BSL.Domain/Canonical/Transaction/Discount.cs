namespace AAS_BSL.Domain.Canonical.Transaction;

public class Discount
{
    public string id { get; set; }
    public Amount amount { get; set; }
    public DateWithOffset appliedDateTimeUtc { get; set; }
    public Person approvedByEmployee { get; set; }
    public string categoryId { get; set; }
    public IEnumerable<Chargeback> chargebacks { get; set; }
    public string couponId { get; set; }
    public CustomProperty customProperties { get; set; }
    public string customerProgramId { get; set; }
    public DiscountApprovalInfo discountApprovalInfo { get; set; }
    public string discountReason { get; set; }
    public string discountType { get; set; }//Enum?
    public string discountTypeLabel { get; set; }
    public string internalId { get; set; }
    public bool isTaxable { get; set; }
    public bool isVoided { get; set; }
    public IEnumerable<LineItemBreakdown> lineItemBreakdowns { get; set; }
    public string loyaltyAccountInfoId { get; set; }
    public string name { get; set; }
    public int pointsRedeemed { get; set; }
    public VoidInfo voidInfo { get; set; }
    
}
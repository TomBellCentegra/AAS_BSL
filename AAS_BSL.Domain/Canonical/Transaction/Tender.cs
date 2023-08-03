namespace AAS_BSL.Domain.Canonical.Transaction;

public class Tender
{
    public Amount? actualAmount { get; set; }
    public string? cardLastFourDigits { get; set; }
    public CheckInfo? checkInfo { get; set; }
    public string? couponId { get; set; }
    public Currency currency { get; set; }
    public CustomProperty? customProperties { get; set; }
    public string? customerPaymentId { get; set; }
    public string? customerProgramId { get; set; }
    public Person? employee { get; set; }
    public Amount? exchangeRate { get; set; }
    public int id { get; set; }
    public bool? isRefused { get; set; }
    public bool? isVoided { get; set; }
    public int lineNumber { get; set; }
    public string? maskedCardNumber { get; set; }
    public string name { get; set; }
    public string nameOnCard { get; set; }
    public List<OperatorBypassApproval> operatorBypassApprovals { get; set; }
    public Amount? originalAmount { get; set; }
    public Currency? originalCurrency { get; set; }
    public RefusalInfo? refusalInfo { get; set; }
    public List<Surcharge> surcharges { get; set; }
    public Amount tenderAmount { get; set; }
    public DateWithOffset? tenderEndDateTime { get; set; }
    public int? tenderLink { get; set; }
    public string? type { get; set; }
    public string usage { get; set; }
    public VoidInfo? voidInfo { get; set; }
    public int tenderCount { get; set; }
}
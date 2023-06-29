namespace AAS_BSL.Domain.Canonical.Transaction;

public class Item
{
    public string id { get; set; }
    public Amount actualAmount { get; set; }
    public Price? actualUnitPrice { get; set; }
    public DateWithOffset beginDateTimeUtc { get; set; }
    public string? catalogItemCode { get; set; }
    public Category category { get; set; }
    public string? conceptId { get; set; }
    public CustomProperty? customProperties { get; set; }
    public string? departmentId { get; set; }
    public Person? employee { get; set; }
    public DateWithOffset? endDateTimeUtc { get; set; }
    public Amount extendedAmount { get; set; }
    public Price extendedUnitPrice { get; set; }
    public string? inputIdentifierData { get; set; }
    public UnitPriceQuantity? inventoryQuantity { get; set; }
    public bool isInventoryAffectedByReturn { get; set; }
    public bool isItemNotOnFile { get; set; }
    public bool isNonSaleItem { get; set; }
    public bool isOverridden { get; set; }
    public bool isPriceLookUp { get; set; }
    public bool isRefused { get; set; }
    public bool isReturn { get; set; }
    public bool isVoided { get; set; }
    public bool isWeighted { get; set; }
    public IEnumerable<Discount> itemDiscounts { get; set; }
    public IEnumerable<Promotion> itemPromotions { get; set; }
    public string itemSellType { get; set; }//Enum?
    public string? itemSellTypeLabel { get; set; }
    public IEnumerable<Tax> itemTaxes { get; set; }
    public IEnumerable<Discount> itemTenderRewards { get; set; }
    public OperatorBypassApproval? operatorBypassApprovalInfo { get; set; }
    public IEnumerable<OperatorBypassApproval>? operatorBypassApprovals { get; set; }
    public string? orderNumber { get; set; }
    public string? parentItemId { get; set; }
    public string productId { get; set; }
    public string productName { get; set; }
    public UnitPriceQuantity? quantity { get; set; }
    public RefusalInfo? refusalInfo { get; set; }
    public Price regularUnitPrice { get; set; }
    public ReturnInfo? returnInfo { get; set; }
    public IEnumerable<Surcharge> surcharges { get; set; }
    public IEnumerable<Category> variations { get; set; }
    public VoidInfo? voidInfo { get; set; }
    public WicInfo? wicInfo { get; set; }
    
}
using AAS_BSL.Domain.Canonical.Transaction;

namespace AAS_BSL.Domain.Canonical;

public class Canonical
{
    public string id { get; set; } // + 
    public DateWithOffset businessDay { get; set; } //+
    public DateWithOffset closeDateTimeUtc { get; set; } // +
    public CustomProperty? customProperties { get; set; }
    public string dataProviderName { get; set; } // +
    public string dataProviderVersion { get; set; } // +
    public bool isTrainingMode { get; set; } // +
    public bool isUpdated { get; set; } // +
    public IEnumerable<LinkedTransaction> linkedTransactions { get; set; } // +
    public int modelVersion { get; set; } // +
    public DateWithOffset openDateTimeUtc { get; set; } // +
    public SiteInfo siteInfo { get; set; } // +
    public string? touchPointGroup { get; set; }
    public string? touchPointId { get; set; } // +
    public string? touchPointType { get; set; }
    public string? transactionCategory { get; set; } // +
    public string? transactionNumber { get; set; } // +
    public string? transactionReason { get; set; }
    public long transactionVersion { get; set; } // +
    public DateWithOffset updateDateTimeUtc { get; set; } // +
    public TLog tlog { get; set; } //+
}

public class DateWithOffset
{
    public DateTime dateTime { get; set; }
    public string? originalOffset { get; set; }
}

public class Property
{
    public string type { get; set; }
    public string description { get; set; }
    public string example { get; set; }
}

public class CustomProperty
{
    public string type { get; set; }
    public string description { get; set; }
    public string example { get; set; }
    public Property additionalProperties { get; set; }
}

public class LinkedTransaction
{
    public DateWithOffset businessDate { get; set; }
    public string reasonCode { get; set; } // Enum?
    public string reasonCodeLabel { get; set; }
    public string transactionId { get; set; }
}

public class ParticipatingTouchPoint
{
    public string group { get; set; }
    public string id { get; set; }
    public string referenceId { get; set; }
    public string type { get; set; }
}

public class SiteInfo
{
    public string id { get; set; }
    public string name { get; set; }
    public string? referenceId { get; set; }
    public SiteTimeZone? siteTimeZone { get; set; }
}

public class SiteTimeZone
{
    public string timeZone { get; set; }
}

public class TLog
{
    public string checkOutType { get; set; } // +
    public IEnumerable<Coupon> coupons { get; set; } // +
    public CustomProperty? customProperties { get; set; }
    public Customer? customer { get; set; } // +
    public int customerCount { get; set; } // +
    public IEnumerable<CustomerProgram> customerPrograms { get; set; } //+
    public IEnumerable<Person> employees { get; set; } //+
    public bool isDeleted { get; set; } // +
    public bool isOpen { get; set; } // +
    public bool isRecalled { get; set; } // +
    public bool isResumed { get; set; } // +
    public bool isSuspended { get; set; } // +
    public bool isVoided { get; set; } // +
    public IEnumerable<Item> items { get; set; } // +
    public Currency? localCurrency { get; set; }
    public Location? location { get; set; } // +
    public List<CustomerProgram>? loyaltyAccount { get; set; } //+
    public List<OperatorBypassApproval>? operatorBypassApprovals { get; set; } //+
    public List<Order>? orders { get; set; } // +
    public ReceiptDeliveryInfo? receiptDeliveryInfo { get; set; }
    public string receiptId { get; set; } // +
    public ReceiptInfo? receiptInfo { get; set; }
    public List<Surcharge> surcharges { get; set; } // +
    public string? suspendedReason { get; set; }
    public List<Tender> tenders { get; set; } // +
    public List<Tax> totalTaxes { get; set; } // +
    public Totals totals { get; set; } //+
    public List<Discount> transactionDiscounts { get; set; } // +
    public List<Promotion> transactionPromotions { get; set; } //+
    public string transactionType { get; set; } // +
    public VoidInfo? voidInfo { get; set; }
}
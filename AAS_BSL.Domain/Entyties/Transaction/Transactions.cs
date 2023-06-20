using System.ComponentModel.DataAnnotations.Schema;

namespace AAS_BSL.Domain.Entyties.Transaction;

[Table("TDM_Transaction")]
public class Transactions
{
    public string TDMTransactionID { get; set; }
    public DateTime BusinessDay { get; set; }
    public DateTime CloseDate { get; set; }
    public DateTime OpenDate { get; set; }
    public bool IsTraining { get; set; }
    public string SiteInfoId { get; set; }
    public string SiteInfoName { get; set; }
    public string SiteInfoTimeZone { get; set; }
    public string EmployeeName { get; set; }
    public string Employees { get; set; }
    public string EmployeeShiftId { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsOpen { get; set; }
    public bool IsVoided { get; set; }
    public string LocalCurrency { get; set; }
    public string Location { get; set; }
    public string LocationId { get; set; }
    public string ReceiptId { get; set; }
    public string TransactionType { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Batched { get; set; }
    public bool ToRemove { get; set; }
    public double TotalDiscount { get; set; }

    public virtual IList<Item.Item> Items { get; set; }
    public virtual IList<Discount.Discount> Discounts { get; set; }
}
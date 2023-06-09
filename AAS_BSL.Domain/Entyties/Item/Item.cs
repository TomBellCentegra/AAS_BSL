using System.ComponentModel.DataAnnotations.Schema;

namespace AAS_BSL.Domain.Entyties.Item;

[Table("TDM_Item")]
public class Item
{
    public int ItemID { get; set; }
    public double Discount { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string Measurement { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
    public int ParentItemId { get; set; }
    public string TDMTransactionID { get; set; }
}
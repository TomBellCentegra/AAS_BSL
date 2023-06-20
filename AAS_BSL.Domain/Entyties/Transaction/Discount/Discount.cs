using System.ComponentModel.DataAnnotations.Schema;

namespace AAS_BSL.Domain.Entyties.Transaction.Discount;

[Table("TDM_Discount")]
public class Discount
{
    public int TDMDiscountId { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
    public string Type { get; set; }
}
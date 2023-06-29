using System.ComponentModel.DataAnnotations.Schema;

namespace AAS_BSL.Domain.Entyties.Item.Tax;

[Table("TDM_Item_Taxes")]
public class Tax
{
    public int TDMItemTaxesId { get; set; }
    public string Name { get; set; }
    public int ExternalId { get; set; }
    public string Type { get; set; }
    public double TaxableAmount { get; set; }
    public double Amount { get; set; }
    public int ItemId { get; set; }
}
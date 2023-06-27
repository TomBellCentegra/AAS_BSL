namespace AAS_BSL.Domain.Entyties.Payment;

public class Totals
{
    public int TotalsID { get; set; }
    public double GrandAmount { get; set; }
    public double NetAmount { get; set; }
    public double GrossAmount { get; set; }
    public double VoidsAmount { get; set; }
    public double DiscountAmount { get; set; }
    public double TaxExclusive { get; set; }
    public string TDMTransactionID { get; set; }
}
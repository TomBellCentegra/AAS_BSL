using System.ComponentModel.DataAnnotations.Schema;

namespace AAS_BSL.Domain.Entyties.Payment;

[Table("TDM_Payment")]
public class Payment
{
    public int PaymentID { get; set; }
    public int ExternalPaymentID { get; set; }
    public string Type { get; set; }
    public double Amount { get; set; }
    public string Currency { get; set; }
    public string TDMTransactionID { get; set; }
}
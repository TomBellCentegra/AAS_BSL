using System.ComponentModel.DataAnnotations.Schema;

namespace AAS_BSL.Domain.Entyties.Transaction;

[Table("TDM_Transaction_Payload")]
public class TransactionPayload
{
    public string TDMTransactionsID { get; set; }
    public string Payload { get; set; }
}
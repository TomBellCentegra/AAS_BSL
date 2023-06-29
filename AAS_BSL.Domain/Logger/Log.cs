using System.ComponentModel.DataAnnotations.Schema;

namespace AAS_BSL.Domain.Logger;

[Table("TDM_Logging")]
public class Log
{
    public int TDMLogID { get; set; }
    public string TDMTransactionID { get; set; }
    public string Raw { get; set; }
    public DateTime DateStamp { get; set; }

    public Log()
    {
    }

    public Log(string transactionId, string raw)
    {
        TDMTransactionID = transactionId;
        Raw = raw;
        DateStamp = DateTime.Now;
    }
}
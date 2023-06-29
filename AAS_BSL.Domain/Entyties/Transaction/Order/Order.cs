namespace AAS_BSL.Domain.Entyties.Transaction.Order;

public class Order
{
    public int OrderID { get; set; }
    public string Channel { get; set; }
    public string Number { get; set; }
    public string Source { get; set; }
    public string ReferenceId { get; set; }
    public string ModeId { get; set; }
    public string ModeName { get; set; }
    public string TDMTransactionID { get; set; }
}
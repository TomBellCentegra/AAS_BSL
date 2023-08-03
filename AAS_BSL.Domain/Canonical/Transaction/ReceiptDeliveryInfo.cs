namespace AAS_BSL.Domain.Canonical.Transaction;

public class ReceiptDeliveryInfo
{
    public string email { get; set; }
    public bool isEmail { get; set; }
    public bool isPrinter { get; set; }
    public bool isSms { get; set; }
    public string sms { get; set; }
}
namespace AAS_BSL.Domain.Canonical.Transaction;

public class Promotion
{
    public Amount amount { get; set; }
    public string couponId { get; set; }
    public string id { get; set; }
    public string internalId { get; set; }
    public string message { get; set; }
    public string name { get; set; }
    public int percentage { get; set; }
    public int points { get; set; }
    public string rewardType { get; set; }//Enum?
}
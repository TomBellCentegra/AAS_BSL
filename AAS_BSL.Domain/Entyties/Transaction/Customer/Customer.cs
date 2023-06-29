namespace AAS_BSL.Domain.Entyties.Transaction.Customer;

public class Customer
{
    public int CustomerID { get; set; }
    public string CustomerType { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public string TDMTransactionID { get; set; }
}
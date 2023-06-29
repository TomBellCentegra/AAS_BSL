namespace AAS_BSL.Domain.Canonical.Transaction;

public class Person
{
    public string? action { get; set; }
    public CustomProperty? customProperties { get; set; }
    public string id { get; set; }
    public bool isTippableEmployee { get; set; }
    public string name { get; set; }
    public string? roleId { get; set; }
    public string? roleName { get; set; }
    public string? shiftId { get; set; }
}
namespace AAS_BSL.Domain.Entyties.Transaction.Emploee;

public class Employee
{
    public int EmployeeID { get; set; }
    public string Name { get; set; }
    public string RoleName { get; set; }
    public int RoleId { get; set; }
    public int ShiftId { get; set; }
    public string TDMTransactionID { get; set; }
}
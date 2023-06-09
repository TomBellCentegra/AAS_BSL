using System.Text.Json.Serialization;

namespace AAS_BSL.Domain.Canonical.Transaction;

public class OperatorBypassApproval
{
    public string actionType { get; set; }
    public DateWithOffset approvalDateTime { get; set; }
    public string description { get; set; }
    public string entryMethod { get; set; } // Enum?
    public string entryMethodLabel { get; set; }
    public string inputData { get; set; }
    public bool isApproved { get; set; }
    public bool isDelayedApproval { get; set; }
    [JsonPropertyName("operator")]
    public Person Operator { get; set; }

    public string policyName { get; set; }
    public string sequenceNumber { get; set; }
    
}
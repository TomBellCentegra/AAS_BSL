namespace AAS_BSL.Domain.Canonical.Transaction;

public class PromptAcknowledgment
{
    public bool isSkippable { get; set; }
    public string name { get; set; }
    public string requiredPromptType { get; set; } //Enum?
}
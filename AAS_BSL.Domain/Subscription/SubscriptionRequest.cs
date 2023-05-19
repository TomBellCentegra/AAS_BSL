namespace AAS_BSL.Domain.Subscription;

public class SubscriptionRequest
{
    public AuthenticationCredential authenticationCredentials { get; set; }
    public string description { get; set; }
    public Endpoint endpoint { get; set; }
    public IEnumerable<Dictionary<string,string>> messageAttributePatterns { get; set; }
    public string name { get; set; }
    public bool payloadDelivered { get; set; }
    public TdmTopicIdData topicId { get; set; }
    
}
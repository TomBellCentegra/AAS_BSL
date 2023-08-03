namespace AAS_BSL.Domain.Dtos;

public class Request
{
    public Topic topicId { get; set; }
    public string correlationId { get; set; }
    public string messageId { get; set; }
    public long receiptTimeMs { get; set; }
    public IEnumerable<KeyValuePair<string,string>> attributes { get; set; }
}

public class Topic
{
    public string name { get; set; }
}
using AAS_BSL.Domain.Enums;

namespace AAS_BSL.Domain.Dtos;

public class StatusResult
{
    public Status Status { get; set; }
    public string Message { get; set; }
    public override string ToString()
    {
        return $"status: {Status.ToString()}\nmessage: {Message}";
    }
}
using AAS_BSL.Domain.Logger;

namespace AAS_BSL.Services.Logger;

public interface ILoggerService
{
    Task Save(Log log);
    Task BatchSave(List<Log> logs);
}
using AAS_BSL.Domain.Canonical;

namespace AAS_BSL.Services.Order;

public interface IOrderService
{
    Task Process(Canonical canonical);
}
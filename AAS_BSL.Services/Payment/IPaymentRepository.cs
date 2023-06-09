namespace AAS_BSL.Services.Payment;

public interface IPaymentRepository
{
    Task Add(Domain.Entyties.Payment.Payment payment);
    Task Delete(string transactionId);
}
namespace AAS_BSL.Services.Item.Tax;

public interface ITaxRepository
{
    Task BatchAdd(IEnumerable<Domain.Entyties.Item.Tax.Tax> taxes);
}
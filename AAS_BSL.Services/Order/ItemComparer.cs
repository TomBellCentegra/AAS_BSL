namespace AAS_BSL.Services.Order;

public class ItemComparer: IEqualityComparer<Domain.Entyties.Item.Item>
{
    public int GetHashCode(Domain.Entyties.Item.Item item)
    {
        return item.ProductId.GetHashCode();
    }

    public bool Equals(Domain.Entyties.Item.Item f1, Domain.Entyties.Item.Item f2)
    {
        if (ReferenceEquals(f1, f2))
        {
            return true;
        }
        if (ReferenceEquals(f1, null) ||
            ReferenceEquals(f2, null))
        {
            return false;
        }
        return f1.ProductId == f2.ProductId;
    }
}
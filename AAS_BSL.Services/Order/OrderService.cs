using AAS_BSL.Domain.Canonical;
using AAS_BSL.Domain.Canonical.Transaction;
using AAS_BSL.Domain.Dtos.Transaction;
using AAS_BSL.Domain.Entyties.Transaction;
using AAS_BSL.Domain.Logger;
using AAS_BSL.Infrastructure.Mapper;
using AAS_BSL.Services.Item;
using AAS_BSL.Services.Logger;
using AAS_BSL.Services.Payment;
using AAS_BSL.Services.Transaction;
using AAS_BSL.Services.Transaction.Discount;
using AAS_BSL.Services.TransactionPayload;
using Newtonsoft.Json;

namespace AAS_BSL.Services.Order;

public class OrderService : IOrderService
{
    private readonly ITransactionService _transactionService;
    private readonly IItemRepository _itemRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly ITransactionPayloadService _transactionPayloadService;
    private readonly ILoggerService _loggerService;
    private readonly IItemService _itemService;
    private readonly IDiscountRepository _discountRepository;

    public OrderService(
        ITransactionService transactionService,
        ITransactionPayloadService transactionPayloadService,
        IPaymentRepository paymentRepository,
        IItemRepository itemRepository,
        ILoggerService loggerService,
        IItemService itemService,
        IDiscountRepository discountRepository)
    {
        _transactionService = transactionService;
        _transactionPayloadService = transactionPayloadService;
        _paymentRepository = paymentRepository;
        _itemRepository = itemRepository;
        _loggerService = loggerService;
        _itemService = itemService;
        _discountRepository = discountRepository;
    }

    public async Task Process(Canonical canonical)
    {
        try
        {
            await _loggerService.Save(new Log(canonical.id, "Get transaction process start"));

            await _transactionPayloadService.Add(new Domain.Entyties.Transaction.TransactionPayload
                { TDMTransactionsID = canonical.id, Payload = JsonConvert.SerializeObject(canonical) });

            var transaction = await _transactionService.Get(canonical.id);

            if (transaction == null)
            {
                await _loggerService.Save(new Log(canonical.id, $"Transaction with {canonical.id} is new"));

                var transactionId = await _transactionService.Add(CreateTransactionEntity(canonical));

                await _loggerService.Save(new Log(canonical.id, $"Transaction created"));

                await _transactionService.SetBatched(transactionId, 0);

                await _loggerService.Save(new Log(canonical.id, $"Transaction set batched to 0"));

                await _loggerService.Save(new Log(canonical.id, $"Transaction add items process start"));

                var setItems = GetCanonicalItems(canonical);
                await _itemService.AddList(setItems);

                await _loggerService.Save(new Log(canonical.id, $"Transaction add items process end"));

                var discounts = canonical.tlog.transactionDiscounts.Select(x => x.ToEntity());
                var resDiscounts = discounts.Select(x =>
                {
                    x.TDMTransactionID = canonical.id;
                    return x;
                });
                await _discountRepository.BatchAdd(resDiscounts);

                await _loggerService.Save(new Log(canonical.id, $"Transaction add payment process start"));

                var payment = createPayment(canonical.tlog.tenders, canonical.tlog.totals);
                payment.TDMTransactionID = transactionId;

                await _paymentRepository.Add(payment);

                await _loggerService.Save(new Log(canonical.id, $"Transaction add payment process end"));

                await _transactionService.SetBatched(transactionId, 1);

                await _loggerService.Save(new Log(canonical.id, $"Transaction processed sucessfully end p"));

                return;
            }

            await ProcessUpdateTransaction(transaction, canonical);
        }
        catch (Exception e)
        {
            await _loggerService.Save(new Log(canonical.id, $"Failed with message {e.Message}"));
        }
    }

    public async Task ProcessCancellation(string transactionId)
    {
        await _loggerService.Save(new Log(transactionId, "Transaction cancel process start"));

        await _transactionService.SetRemove(transactionId);

        await _loggerService.Save(new Log(transactionId, "Transaction cancel process end"));
    }

    private TransactionDto CreateTransactionEntity(Canonical canonical)
    {
        return new TransactionDto
        {
            TransactionID = canonical.id,
            BusinessDay = canonical.businessDay.dateTime,
            CloseDate = canonical.closeDateTimeUtc.dateTime,
            OpenDate = canonical.openDateTimeUtc.dateTime,
            IsTraining = canonical.isTrainingMode,
            SiteInfoId = canonical.siteInfo.id,
            SiteInfoName = canonical.siteInfo.name,
            SiteInfoTimeZone = canonical.siteInfo.siteTimeZone?.timeZone ?? null,
            IsDeleted = canonical.tlog.isDeleted,
            IsOpen = canonical.tlog.isOpen,
            IsVoided = canonical.tlog.isVoided,
            LocalCurrency = canonical.tlog.localCurrency?.code ?? null,
            Location = canonical.tlog.location?.location ?? null,
            LocationId = canonical.tlog.location?.locationId ?? null,
            ReceiptId = canonical.tlog.receiptId,
            TransactionType = canonical.tlog.transactionType,
            CreatedDate = DateTime.Now,
            TotalDiscount = canonical.tlog.totals.discountAmount?.amount ?? 0
        };
    }

    private Domain.Entyties.Payment.Payment createPayment(IEnumerable<Tender> tenders, Totals totals)
    {
        if (!tenders.Any())
        {
            return null;
        }

        var tendersList = tenders.ToList();

        var paymentTenders = tendersList.Where(x => x.usage.Equals("PAYMENT", StringComparison.OrdinalIgnoreCase));
        var changeTenders = tendersList.Where(x => x.usage.Equals("CHANGE", StringComparison.OrdinalIgnoreCase));

        return new Domain.Entyties.Payment.Payment
        {
            ExternalPaymentID = tendersList.First().id,
            Type = tendersList.First().type,
            Amount = paymentTenders.Sum(x => x.tenderAmount.amount),
            Change = changeTenders.Sum(x => x.tenderAmount.amount),
            Currency = tendersList.First().currency.code,
            GrandAmount = totals.grandAmount?.amount ?? 0,
            NetAmount = totals.netAmount?.amount ?? 0,
            GrossAmount = totals.grossAmount?.amount ?? 0,
            VoidsAmount = totals.voidsAmount?.amount ?? 0,
            DiscountAmount = totals.discountAmount?.amount ?? 0,
            TaxExclusive = totals.taxExclusive?.amount ?? 0
        };
    }

    private async Task ProcessUpdateItems(IEnumerable<Domain.Entyties.Item.Item> incomeItems,
        IEnumerable<Domain.Entyties.Item.Item> currentItems)
    {
        var comparer = new ItemComparer();

        var newItems =
            currentItems.Except(incomeItems, comparer).ToList();

        if (newItems.Any())
        {
            await _itemService.AddList(newItems);
        }

        var toBeDeleted = incomeItems.Except(currentItems, comparer).ToList();

        if (toBeDeleted.Any())
        {
            await _itemRepository.BatchDelete(toBeDeleted);
        }

        var toBeUpdated = incomeItems.Where(x => currentItems.Any(y =>
            x.ProductId == y.ProductId &&
            (x.Quantity != y.Quantity ||
             x.ProductName != y.ProductName ||
             x.Discount != y.Discount ||
             x.ParentItemId != y.ParentItemId
            ))).ToList();

        if (toBeUpdated.Any())
        {
            await _itemRepository.BatchUpdate(toBeUpdated, currentItems);
        }
    }

    private async Task ProcessUpdatePayment(IEnumerable<Tender> tenders, Totals totals, string transactionId)
    {
        await _paymentRepository.Delete(transactionId);

        var payment = createPayment(tenders, totals);

        payment.TDMTransactionID = transactionId;

        await _paymentRepository.Add(payment);
    }

    private async Task ProcessUpdateTransaction(Transactions transaction, Canonical canonical)
    {
        await _loggerService.Save(new Log(canonical.id, $"Transaction update processing start"));

        await _transactionService.SetBatched(transaction.TDMTransactionID, 0);

        await _loggerService.Save(new Log(canonical.id, $"Transaction update items process start"));

        await ProcessUpdateItems(transaction.Items, GetCanonicalItems(canonical));

        await _loggerService.Save(new Log(canonical.id, $"Transaction update items process end"));

        await _loggerService.Save(new Log(canonical.id, $"Transaction update payment process start"));

        await ProcessUpdatePayment(canonical.tlog.tenders, canonical.tlog.totals, canonical.id);

        await _loggerService.Save(new Log(canonical.id, $"Transaction update payment process end"));

        await _transactionService.SetBatched(transaction.TDMTransactionID, 1);

        await _loggerService.Save(new Log(canonical.id, $"Transaction update processing end"));
    }

    private IEnumerable<Domain.Entyties.Item.Item> GetCanonicalItems(Canonical canonical)
    {
        var items = canonical.tlog.items.Select(x => x.ToEntity());

        return items.Select(x =>
        {
            x.TDMTransactionID = canonical.id;
            return x;
        });
    }
}
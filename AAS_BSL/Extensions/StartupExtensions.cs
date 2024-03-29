﻿using AAS_BSL.Infrastructure.TypeSearcher;
using AAS_BSL.Services.Company;
using AAS_BSL.Services.Secret;
using AAS_BSL.Infrastructure.Database;
using AAS_BSL.Infrastructure.Mapper;
using AAS_BSL.Services.HttpClient;
using AAS_BSL.Services.Item;
using AAS_BSL.Services.Item.Tax;
using AAS_BSL.Services.Logger;
using AAS_BSL.Services.Order;
using AAS_BSL.Services.Payment;
using AAS_BSL.Services.Profiles;
using AAS_BSL.Services.Subsription;
using AAS_BSL.Services.Transaction;
using AAS_BSL.Services.Transaction.Customer;
using AAS_BSL.Services.Transaction.Discount;
using AAS_BSL.Services.Transaction.Employee;
using AAS_BSL.Services.Transaction.Order;
using AAS_BSL.Services.TransactionPayload;
using AutoMapper;

namespace AAS_BSL.Extensions;

public static class StartupExtensions
{
    private static void InitAutoMapper(ITypeSearcher typeSearcher)
    {
        var mapperConfigurations = typeSearcher.ClassesOfType<IAutoMapperProfile>();

        var instances = mapperConfigurations
            .Select(mapperConfiguration => (IAutoMapperProfile)Activator.CreateInstance(mapperConfiguration));

        var config = new MapperConfiguration(cfg =>
        {
            foreach (var instance in instances)
            {
                if (instance != null)
                {
                    cfg.AddProfile(instance.GetType());
                }
            }
        });

        AutoMapperConfig.Init(config);
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var typeSearcher = new TypeSearcher();
        services.AddSingleton<ITypeSearcher>(typeSearcher);

        InitAutoMapper(typeSearcher);

        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ISecretService, SecretService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<ITransactionPayloadService, TransactionPayloadService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<ITaxRepository, TaxRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ITotalsRepository, TotalsRepository>();
        services.AddScoped<ILoggerService, LoggerService>();

        services.AddHttpClient<IBslHttpClient, BslHttpClient>();

        services.AddSingleton<CentegraProcessingDbContext>();
    }
}
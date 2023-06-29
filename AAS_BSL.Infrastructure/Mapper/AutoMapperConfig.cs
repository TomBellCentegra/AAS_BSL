using AutoMapper;

namespace AAS_BSL.Infrastructure.Mapper;

public static class AutoMapperConfig
{
    public static IMapper Mapper { get; private set; }
    
    public static void Init(MapperConfiguration config)
    {
        Mapper = config.CreateMapper();
    }
}
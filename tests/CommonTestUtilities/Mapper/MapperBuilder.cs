using AutoMapper;
using CashFlow.Application.AutoMapper;

public class MapperBuilder
{
    public static IMapper Build()
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddProfile(new AutoMapping());
        });
        
        return mapperConfig.CreateMapper();
    }
}
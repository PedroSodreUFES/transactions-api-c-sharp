using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore()); // ignora o campo senha na hora de mapear

        CreateMap<RequestExpenseJson, Expense>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Distinct()));  // garante que não envie tags repetidas.

        CreateMap<Communication.Enums.Tag, Tag>()
            .ForMember(dest => dest.Value, config => config.MapFrom(source => source));
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseExpenseJson>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Select(tag => tag.Value)));
        CreateMap<Expense, ResponseRegisteredExpenseJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<User, ResponseUserProfileJson>();
    }
}
using AutoMapper;
using ZurichInterview.Application.Dtos.Client;
using ZurichInterview.Domain.Entities;

namespace ZurichInterview.Application.Mapping;

public class PolicyProfile : Profile
{
    public PolicyProfile()
    {
        CreateMap<Policy, PolicyDto>().ReverseMap();
    }
}
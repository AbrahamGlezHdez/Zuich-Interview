using AutoMapper;
using ZurichInterview.Application.Dtos.Client;
using ZurichInterview.Domain.Entities;

namespace ZurichInterview.Application.Mapping;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientDto, Client>();
        CreateMap<Client, ClientDto>();
    }
}
using AutoMapper;
using MessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.API.Application.CQRS.Commands;
using NotificationMessageSender.API.DTOs.Requests.Company;
using NotificationMessageSender.API.DTOs.Requests.User;
using NotificationMessageSender.API.DTOs.Responses.Company;
using NotificationMessageSender.API.DTOs.Responses.User;

namespace NotificationMessageSender.API.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserEntity, CreateUserResponse>();
            CreateMap<CreateUserCommand, UserEntity>();
            CreateMap<CreateCompanyRequest, CreateCompanyCommand>();
            CreateMap<CreateCompanyCommand, CompanyEntity>();
            CreateMap<CompanyEntity, CreateCompanyResponse>();
            CreateMap<UserEntity, GetUserResponse>();
        }
    }
}

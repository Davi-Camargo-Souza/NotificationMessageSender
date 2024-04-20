using AutoMapper;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.API.DTOs.Requests.Company;
using NotificationMessageSender.API.DTOs.Requests.User;
using NotificationMessageSender.API.DTOs.Responses.Company;
using NotificationMessageSender.API.DTOs.Responses.User;
using NotificationMessageSender.API.Application.CQRS.Commands.User;
using NotificationMessageSender.API.Application.CQRS.Commands.Company;
using NotificationMessageSender.API.DTOs.Responses;
using NotificationMessageSender.API.Application.CQRS.Commands.Notification;

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
            CreateMap<UserEntity, LoginResponse>();
            CreateMap<CreateNotificationCommand, SendNotificationCommand>();
            CreateMap<SendNotificationCommand, CreateNotificationCommand>();
        }
    }
}

using AutoMapper;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.Core.Models;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Request, RequestAdminDto>()
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
             .ForMember(dest => dest.RequesterId, opt => opt.MapFrom(src => src.CreatedBy))
             .ForMember(dest => dest.RequesterName, opt => opt.MapFrom(src => src.Requester.FullName))
             .ForMember(dest => dest.AssignedStaffId, opt => opt.MapFrom(src => src.AssignedStaffId))
             .ForMember(dest => dest.AssignedStaffName, opt => opt.MapFrom(src =>
                 src.AssignedStaff != null ? src.AssignedStaff.FullName : "Unassigned"))
             .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryItem.CategoryId))
             .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryItem.Category.Name))
             .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.CategoryItem.Name))
             .ReverseMap();


        CreateMap<Request, RequestForEmployeeDto>()
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
             .ForMember(dest => dest.AssignedStaffName, opt => opt.MapFrom(src =>
                 src.AssignedStaff != null ? src.AssignedStaff.FullName : "Unassigned"))
             .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryItem.Category.Name))
             .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.CategoryItem.Name))
             .ReverseMap();


        CreateMap<Request, RequestForStaffDto>()
     .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
     .ForMember(dest => dest.RequesterName, opt => opt.MapFrom(src => src.Requester.FullName))
     .ForMember(dest => dest.RequesterPhone, opt => opt.MapFrom(src => src.Requester.PhoneNumber))
     .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryItem.Category.Name))
     .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.CategoryItem.Name));

        CreateMap<UpdateEmployeeRequestDto, Request>()
    .ForMember(dest => dest.CategoryItemId, opt => opt.MapFrom(src => src.CategoryItemId))
    .ForMember(dest => dest.Id, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
            .ReverseMap();
    }
}
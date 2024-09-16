using AutoMapper;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using EntityLayer.DTOs.MessageDTOs;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.StatusDTOs;
using EntityLayer.DTOs.TravelDTOs;
using EntityLayer.StaffDTOs;


namespace TravelStaffAPI.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Staff, GetStaffDto>().ReverseMap();
            CreateMap<Staff, UpdateStaffDto>().ReverseMap();
            CreateMap<Staff, CreateStaffDto>().ReverseMap();
            CreateMap<Staff, DeleteStaffDto>().ReverseMap();
            //CreateMap<Staff, LoginStaffDto>();
            CreateMap<Status, CreateStatusDto>().ReverseMap();
            CreateMap<Status, UpdateStatusDto>().ReverseMap();
            CreateMap<Status, DeleteStatusDto>().ReverseMap();
            CreateMap<Status, GetStatusDto>().ReverseMap();
            CreateMap<Travel, CreateTravelDto>().ReverseMap();
            CreateMap<Travel, UpdateTravelDto>().ReverseMap();
            CreateMap<Travel, DeleteTravelDto>().ReverseMap();
            CreateMap<Travel, GetTravelDto>().ReverseMap();
            CreateMap<Message, GetMessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();
            CreateMap<Travel, TravelMessageLayoutDto>()
           .ForMember(dest => dest.getTravelDto, opt => opt.MapFrom(src => src))
           .ForMember(dest => dest.getMessageDto, opt => opt.Ignore());
            //CreateMap<Staff, GetStaffDto>()
            //.ForMember(dest => dest.Travels, opt => opt.MapFrom(src => src.Travels));
        }
    }
}

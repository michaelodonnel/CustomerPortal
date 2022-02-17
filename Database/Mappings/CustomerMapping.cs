using AutoMapper;
using Database.Entities;
using DataTransfer;

namespace Database.Mappings
{
    public sealed class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<Customer, CustomerDetails>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.ReferenceNumber, opt => opt.MapFrom(src => src.ReferenceNumber))
                .ForMember(dest => dest.DOB, opt => opt.MapFrom(src => src.DOB))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}

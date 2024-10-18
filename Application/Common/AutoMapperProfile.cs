using Application.Contacts.Model;
using AutoMapper;
using Domain.Entity;

namespace Application.Common;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Contact, ContactResponse>()
            .ForMember(
                dest => dest.Category,
                opt => 
                    opt.MapFrom(src => src.Category.Name))
            .ForMember(
                dest => dest.Subcategory,
                opt => 
                    opt.MapFrom(src => src.Subcategory != null ? src.Subcategory.Name : null));
    }   
}
using AutoMapper;
using ContactService.DTO;
using ContactService.Model;

namespace ContactService.Mapper
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDTO>().ReverseMap();
        }
    }
}

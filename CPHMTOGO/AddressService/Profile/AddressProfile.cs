using AddressService.Domain;
using AddressService.Domain.Dto;

namespace AddressService.Profile
{
    public class AddressProfile: AutoMapper.Profile
    {

        public AddressProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
        }

    }
}

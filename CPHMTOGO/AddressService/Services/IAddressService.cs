using Core.Service;
using AddressService.Domain;
using AddressService.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressService.Services.Interfaces;

public interface IAddressService : IBaseService<Address, AddressDto>
{
    
    public Task<AddressDto> CreateAsync(String street, String streetNr, String zipCode);

}
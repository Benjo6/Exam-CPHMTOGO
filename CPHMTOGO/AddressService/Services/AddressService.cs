using AutoMapper;
using Core.Service;
using AddressService.Domain;
using AddressService.Domain.Dto;
using AddressService.Repositories.Interfaces;
using AddressService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AddressService.Services;

class AddressService : BaseService<Address, AddressDto>, IAddressService
{
    public AddressService(IAddressRepository repository, IMapper mapper) : base(repository, mapper)

    {
       
    }

    public override Task<AddressDto> Create(AddressDto entityDto)
    {
        return base.Create(entityDto);
    }



}
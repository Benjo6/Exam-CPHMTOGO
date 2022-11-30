using AutoMapper;
using Core.Service;
using Address.Domain;
using Address.Domain.Dto;
using Address.Repositories.Interfaces;
using Address.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AddressService.Services;

class AddressService : BaseService<Address, AddressDto>, IAddressService
{
    public Address(IAddressRepository repository, IMapper mapper) : base(repository, mapper)

    {
       
    }

    public override 



}
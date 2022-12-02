using AutoMapper;
using Core.Service;
using AddressService.Domain;
using AddressService.Domain.Dto;
using AddressService.Repositories.Interfaces;
using AddressService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Emit;
using Newtonsoft.Json.Linq;

namespace AddressService.Services;

class AddressService : BaseService<Address, AddressDto>, IAddressService
{
    IHttpClientFactory _factory;
    private HttpClient _client;
    IAddressRepository _repository;
    public AddressService(IAddressRepository repository, IMapper mapper, IHttpClientFactory factory) : base(repository, mapper)

    {
        _repository = repository;
        _factory = factory;
        _client = _factory.CreateClient("Address");
    }

    public async Task<AddressDto> CreateAsync(String street, String streetNr, String zipCode)
    {
        Address address = new Address();
        address.Street = street;
        address.StreetNr = streetNr;
        address.Zipcode= zipCode;

        

        var item = await ValidAddress(street, streetNr, zipCode);
        address.Latitude = item.geometry.coordinates[1];
        address.Longitude = item.geometry.coordinates[0];


        _repository.Create(address);
        return _mapper.Map<Address, AddressDto>(address);
    }

   

    public override async Task<AddressDto> Update(AddressDto entityDto)
    {
       
       

        var item = await ValidAddress(entityDto.Street, entityDto.StreetNr, entityDto.ZipCode);
        entityDto.Latitude = item.geometry.coordinates[1];
        entityDto.Longitude = item.geometry.coordinates[0];


        _repository.Update(_mapper.Map<AddressDto, Address>(entityDto));
        return entityDto;


    }

   private async Task<dynamic> ValidAddress (String street, String streetNr, String zipCode)
    {
        try
        {
            HttpResponseMessage response = _client.GetAsync($"adresser?vejnavn={street}&postnr={zipCode}&husnr={streetNr}").Result;
            
            String content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
               dynamic address = JValue.Parse(content);

                return address;
            }
            return null;
        }

        catch(HttpRequestException e)
        {
            throw new HttpRequestException(e.Message);
        }

    }



}
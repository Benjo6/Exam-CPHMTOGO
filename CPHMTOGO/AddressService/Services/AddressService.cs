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

    public AddressService(IAddressRepository repository, IMapper mapper, IHttpClientFactory factory) : base(repository,
        mapper)

    {
        _repository = repository;
        _factory = factory;
        _client = _factory.CreateClient("Address");
    }

    public async Task<AddressDto> CreateAsync(string street, string streetNr, string zipCode)
    {
        try
        {
            Address address = new Address();
            address.Street = street;
            address.StreetNr = streetNr;
            address.Zipcode = zipCode;
            address.CityId = Guid.NewGuid(); //Remove please from Database



            var item = await ValidateAddress(street, streetNr, zipCode);
            if (item != null)
            {
                address.Latitude = item.adgangsadresse.adgangspunkt.koordinater[1];
                address.Longitude = item.adgangsadresse.adgangspunkt.koordinater[0];
                if (address.Latitude != null && address.Longitude != null)
                {
                    _repository.Create(address);
                    return _mapper.Map<Address, AddressDto>(address);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return null;
    }

    public async Task<string> AutoCompleteAdresser(string query)
    {
        try
        {
            string url = "adresser/autocomplete" + (query.Length == 0 ? "" : "?") + query;
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            dynamic adresser = JArray.Parse(content);

            foreach (var adresse in adresser)
            {
                return FormatAdresseAutocomplete(adresse);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return "No items with these requirements";
    }

    private async Task<dynamic> ValidateAddress(String street, String streetNr, String zipCode)
    {
        try
        {
            string url = $"adresser?vejnavn={street}&husnr={streetNr}&postnr={zipCode}";
            Console.WriteLine("GET " + url);
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic adresser = JArray.Parse(responseBody);

            foreach (dynamic adresse in adresser)
            {
                return adresse;
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Message :{0} ", e.Message);
        }

        return null;
    }

    private string FormatAdresseAutocomplete(dynamic a)
    {
        return string.Format("{0}", a.tekst);
    }
}

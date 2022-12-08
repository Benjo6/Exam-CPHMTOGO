using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Core.Controller;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using AddressService.Domain;
using AddressService.Domain.Dto;
using AddressService.Services.Interfaces;

namespace AddressService.Controllers;

[Route("api/[controller]")]
public class AddressController : BaseController<Address, AddressDto>
{
    private readonly IAddressService _baseService;

    public AddressController(IAddressService baseService) : base(baseService)
    {
        _baseService = baseService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return await GetListAsync();
    }

    [HttpGet("{id}")] 

    public async Task<IActionResult> Get(Guid id)
    {
        return await GetByIdAsync(id);
    }

    [HttpGet("Adresser/{query}")]
    public async Task<string> GetAutoComplete(string query)
    {
        return await _baseService.AutoCompleteAdresser(query);
    }


    [HttpPost]
    public async Task<AddressDto> CreateAddress(string street, string streetNr, string zipCode)
    {
        return await _baseService.CreateAsync(street, streetNr, zipCode);
    }
    
    [HttpDelete]
    public Task<bool> Delete(Guid id)
    {
        return DeleteAsync(id);
    }


}
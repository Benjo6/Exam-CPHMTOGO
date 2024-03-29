﻿using System.Collections.Generic;
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

    public AddressController(IAddressService baseService, ILogger<AddressController> logger) : base(baseService,logger)
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
    public async Task<List<string>> GetAutoComplete(string query)
    {
        return await _baseService.AutoCompleteAdresser(query);
    }


    [HttpPost("createaddress/{street}/{streetNr}/{zipCode}")]
    public async Task<AddressDto> CreateAddress(string street, string streetNr, string zipCode,string? etage,string? door)
    {
        return await _baseService.CreateAsync(street, streetNr, zipCode,etage,door);
    }
    
    [HttpDelete("{id}")]
    public Task<IActionResult> Delete(Guid id)
    {
        return DeleteAsync(id);
    }


}
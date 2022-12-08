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

    public AddressController(IAddressService baseService) : base(baseService)
    {
        _baseService = baseService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return await GetListAsync();
    }


    [HttpPost]
    public async Task<AddressDto> CreateAddress(Guid address, Guid customerId, Guid restaurantId, [FromBody] List<CreateAddressItemDto> AddressDtos)
    {
        return await _baseService.CreateAddressTask(new AddressDto { Address = address, RestaurantId = restaurantId, CustomerId = customerId }, AddressDtos);
    }

    [HttpPut]
    public async Task<IActionResult> Put(AddressDto dto)
    {
        return await UpdateAsync(dto);
    }

    [HttpDelete]
    public Task<bool> Delete(Guid id)
    {
        return DeleteAsync(id);
    }
}
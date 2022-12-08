using Core.Repository;
using Microsoft.EntityFrameworkCore;
using AddressService.Domain;
using AddressService.Infrastructure;
using AddressService.Repositories.Interfaces;

namespace AddressService.Repositories;

public class AddressRepository : RepositoryBase<Address>, IAddressRepository
{
    public AddressRepository(AddressDbContext dbContext) : base(dbContext)
    {



    }


    


}
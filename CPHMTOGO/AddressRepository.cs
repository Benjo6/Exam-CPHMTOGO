using Core.Repository;
using Microsoft.EntityFrameworkCore;
using AddressService.Domain;
using AddressService.Infrastructure;
using AddressService.Repositories.Interfaces;

namespace Address.Repositories;

public class AddressRepository : RepositoryBase<Address>, IAddressRepository
{
    public AddressRepository(RepositoryContext dbContext) : base(dbContext)
    {



    }


    


}
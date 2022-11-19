using Core.Repository;
using OrderService.Domain;

namespace OrderService.Repositories.Interfaces;

public interface IReceiptRepository : IAsyncRepository<Receipt>
{
}
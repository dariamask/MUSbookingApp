
using DAL.Data.Entities;
using System.Data;

namespace DAL.Repository.OrderLineRepo
{
    public interface IOrderLineRepository
    {
        Task CreateOrderLineAsync(List<OrderLine> orders, CancellationToken cancellationToken);
    }
}

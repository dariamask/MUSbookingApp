using DAL.Data.Entities;

namespace DAL.Repository.OrderLineRepo
{
    public interface IOrderlineRepository
    {
        Task CreateOrderLineAsync(OrderLine orderline, CancellationToken cancellationToken);
        Task DeleteOrderlineAsync(OrderLine orderline, CancellationToken cancellationToken);
    }
}

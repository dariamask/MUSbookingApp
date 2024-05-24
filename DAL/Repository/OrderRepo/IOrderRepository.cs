using DAL.Data.Entities;

namespace DAL.Repository.OrderRepo
{
    public interface IOrderRepository
    { 
        Task<Order?> GetOrderAsync(Guid orderId, CancellationToken cancellationToken);
        Task<List<Order?>> GetOrderPaginationAsync(CancellationToken cancellationToken);
        Task CreateOrderAsync(Order order, CancellationToken cancellationToken);
        Task UpdateOrderAsync(Order order, CancellationToken cancellationToken);
        Task DeleteOrderAsync(Order order, CancellationToken cancellationToken);       
    }
}

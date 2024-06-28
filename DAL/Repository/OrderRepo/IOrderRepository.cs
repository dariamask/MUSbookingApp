using DAL.Data.Entities;
using System.Data;

namespace DAL.Repository.OrderRepo
{
    public interface IOrderRepository
    { 
        Task<Order?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken);
        Task<List<Order?>> GetOrderPaginationAsync(CancellationToken cancellationToken);
        Task CreateOrderAsync(Order order, CancellationToken cancellationToken);
        Task UpdateOrderAsync(Order order, CancellationToken cancellationToken);
        Task DeleteOrderAsync(Order order, CancellationToken cancellationToken);
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}
 
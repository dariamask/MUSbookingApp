using DAL.Data;
using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace DAL.Repository.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext context)
        {
            _context = context;
        }
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            var transaction = _context.Database.BeginTransaction(isolationLevel);
            return transaction.GetDbTransaction();
        }

        public async Task<int> GetTotalNumberOfOrders(CancellationToken cancellationToken)
        {
            return await _context.Orders.CountAsync(cancellationToken);
        }

        public async Task CreateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            _context.Add(order);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteOrderAsync(Order order, CancellationToken cancellationToken)
        {
            _context.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);
        }

        public async Task<List<Order>> GetOrderPaginationByCreatedDateAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            return await _context.Orders.AsQueryable()
                .OrderBy(o => o.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            _context.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

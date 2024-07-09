using DAL.Data;
using DAL.Data.Entities;

namespace DAL.Repository.OrderLineRepo
{
    public class OrderlineRepository : IOrderlineRepository
    {
        private readonly DataContext _context;
        public OrderlineRepository(DataContext context)
        {
            _context = context;
        }   
        public async Task CreateOrderLineAsync(OrderLine orderline, CancellationToken cancellationToken)
        {
            _context.Add(orderline);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateOrderLineAsync(OrderLine orderline, CancellationToken cancellationToken)
        {
            _context.Update(orderline);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteOrderlineAsync(OrderLine orderline, CancellationToken cancellationToken)
        {
            _context.Remove(orderline);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

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

        public async Task CreateOrderLineAsync(List<OrderLine> orders, CancellationToken cancellationToken)
        {
            orders.ForEach(orderline => _context.Add(orderline));
            await _context.SaveChangesAsync();
        }
    }
}

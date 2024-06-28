using DAL.Data;
using DAL.Data.Entities;

namespace DAL.Repository.OrderLineRepo
{
    public class OrderLineRepository : IOrderLineRepository
    {
        private readonly DataContext _context;
        public OrderLineRepository(DataContext context)
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

using DAL.Data;
using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            foreach (var orderLine in orders)
            {
                _context.Add(orderLine);
            }
            await _context.SaveChangesAsync();
        }
    }
}

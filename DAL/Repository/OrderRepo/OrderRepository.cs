﻿using DAL.Data;
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

        public Task<List<Order?>> GetOrderPaginationAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            _context.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

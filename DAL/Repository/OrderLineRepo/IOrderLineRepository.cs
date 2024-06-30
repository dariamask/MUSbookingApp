using DAL.Data.Entities;

namespace DAL.Repository.OrderLineRepo
{
    public interface IOrderlineRepository
    {
        Task CreateOrderLineAsync(OrderLine order, CancellationToken cancellationToken); 
    }
}

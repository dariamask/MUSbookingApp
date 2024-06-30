using DAL.Data.Entities;

namespace DAL.Repository.OrderLineRepo
{
    public interface IOrderlineRepository
    {
        Task CreateOrderLineAsync(List<OrderLine> orders, CancellationToken cancellationToken);
        
    }
}

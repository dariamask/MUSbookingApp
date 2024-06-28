using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.EntitiesConfiguration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Navigation(x => x.OrderLine).AutoInclude();

            builder.HasMany<Equipment>()
                .WithMany()
                .UsingEntity<OrderLine>(cfg => cfg
                .HasKey(joinEntity => new { joinEntity.OrderId, joinEntity.EquipmentId }));
        }
    }
}
using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DAL.Data.EntitiesConfiguration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Navigation(x => x.OrderLines).AutoInclude();

            builder.Property(x => x.Description).IsRequired()
                .HasMaxLength(100)
                .IsRequired(); 

            builder.HasMany<Equipment>()
                .WithMany()
                .UsingEntity<OrderLine>(cfg => cfg
                .HasKey(joinEntity => new { joinEntity.OrderId, joinEntity.EquipmentId }));
        }
    }
}

using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.EntitiesConfiguration
{
    public class OrderEquipmentConfiguration : IEntityTypeConfiguration<OrderEquipment>
    {
        public void Configure(EntityTypeBuilder<OrderEquipment> builder)
        {
            builder.HasKey(x => new
                    {
                        x.OrderId,
                        x.EquipmentId
                    });

            builder.HasOne(x => x.Order)
                   .WithMany(x => x.OrderEquipment)
                   .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Equipment)
                   .WithMany(x => x.OrderEquipment)
                   .HasForeignKey(x => x.EquipmentId);
        }
    }
}

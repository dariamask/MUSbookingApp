
using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.EntitiesConfiguration
{
    public class OrderEquipmentConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(x => new
                    {
                        x.OrderId,
                        x.EquipmentId
                    });

            builder.HasOne(x => x.Order)
                   .WithMany(x => x.OrderLine)
                   .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Equipment)
                   .WithMany(x => x.OrderLine)
                   .HasForeignKey(x => x.EquipmentId);
        }
    }
}

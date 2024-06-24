using BAL.Dto.EquipmentDtos;
using BAL.Dto.OrderDtos;
using BAL.Services.EquipmentServices;
using BAL.Services.OrderServices;
using BAL.Validation;
using DAL.Data;
using DAL.Repository.EquipmentRepo;
using DAL.Repository.OrderLineRepo;
using DAL.Repository.OrderRepo;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace MUSbookingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            builder.Services.AddScoped<IOrderLineRepository, OrderLineRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IEquipmentService, EquipmentService>();

            builder.Services.AddScoped<IValidator<EquipmentCreateDto>, EquipmentCreateDtoValidator>();
            builder.Services.AddScoped<IValidator<OrderCreateDto>, OrderCreateDtoValidator>();
            builder.Services.AddScoped<IValidator<OrderUpdateDto>, OrderUpdateDtoValidator>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            builder.Services.AddSerilog(logger);


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

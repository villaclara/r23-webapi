using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ICandleCategoryRepository, CandleCategoryRepository>();
builder.Services.AddScoped<ICandleItemRepository, CandleItemRepository>();
builder.Services.AddScoped<ICandleIngredientRepository, CandleIngredientRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// TO DO
//
// CORS to connect blazor wasm to api 
var allow = "allow";
builder.Services.AddCors(opt =>
{
	opt.AddPolicy(name: allow,
		builder =>
		{
			builder.AllowAnyOrigin().AllowAnyOrigin();
		});
});

var app = builder.Build();

// allow connect blazor wasm to api
app.UseCors(allow);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

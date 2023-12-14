using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ICandleCategoryRepository, CandleCategoryRepository>();
builder.Services.AddScoped<ICandleItemRepository, CandleItemRepository>();
builder.Services.AddScoped<ICandleIngredientRepository, CandleIngredientRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
builder.Services.AddScoped<IReceiverRepository, ReceiverRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();	


builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
	//options.UseSqlServer(builder.Configuration["FreeAspHostingConnection"]);
	//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
	options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"));
});


// Add CORS Policies for different set ups to allow/deny different things from different applications
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "AllowEveryone",
		builder =>
		{
			builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "POST", "PUT", "DELETE");
		});
	options.AddPolicy(name: "AllowLocalhost7263",
		builder => builder
		.WithOrigins("https://localhost:7263")
		.AllowAnyHeader()
		.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS"));

	options.AddPolicy(name: "AllowAdmin",
		builder =>
		{
			builder.WithOrigins("https://r23admin.azurewebsites.net").AllowAnyHeader().WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
		});
	options.AddPolicy(name: "AllowEveryoneGet",
			builder =>
			{
				builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET");
			});
});

var app = builder.Build();

// allow swagger in Production Environment
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}



// using CORS Policy set above with Name
//app.UseCors("AllowEveryoneGet");
app.UseCors("AllowAdmin");
app.UseCors("AllowLocalhost7263");
app.UseCors("AllowEveryoneGet");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
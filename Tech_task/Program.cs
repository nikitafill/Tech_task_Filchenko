using Microsoft.EntityFrameworkCore;
using Tech_task.Data;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<WarehouseAPIDbContext>(options => options.UseInMemoryDatabase("WarehouseDb"));
builder.Services.AddDbContext<WarehouseAPIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WarehouseAPIConnectionsString")));
var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
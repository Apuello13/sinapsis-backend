using Microsoft.EntityFrameworkCore;
using sinapsis_back.Context;

var builder = WebApplication.CreateBuilder(args);
var cors = "allowAll";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));
builder.Services
    .AddCors(options => options.AddPolicy(name: cors,
    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(cors);

app.UseAuthorization();

app.MapControllers();

app.Run();

using Application.Interfaces;
using Application.Services;
using Arch.EntityFrameworkCore.UnitOfWork;
using Data.Context;
using Microsoft.EntityFrameworkCore;

var corsPolicy = "myCors";

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(option =>
{
    option.UseSqlServer(connectionString);
});


builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddUnitOfWork<DataContext>();
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(corsPolicy);

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

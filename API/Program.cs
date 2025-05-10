using CQRSDDDDataRest.Application.Commands;
using CQRSDDDDataRest.Infrastructure.Persistence;
using CQRSDDDDataRest.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PagosCQRSDDDDataRest.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pagos API CQRS-DDD-DataRest", Version = "v1" });
});


builder.Services.AddDbContext<SqlServerPagosDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ISqlServerPagoRepository, SqlServerPagoRepository>();
builder.Services.AddScoped<PagarCommandHandler>();

builder.Services.AddDbContext<PostgresPagosDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddScoped<IPostgresPagoRepository, PostgresPagoRepository>();
builder.Services.AddScoped<PagarCommandHandler>();

builder.Services.Configure<PostgRESTSettings>(builder.Configuration.GetSection("PostgRESTSettings"));
builder.Services.AddHttpClient<PostgresPagoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pagos API CQRS-DDD-DataRest v1"));
}

app.MapControllers();
app.Run();
using AOProject.API.DbContexts;
using AOProject.EntityDataModels;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(opt => 
opt.AddRouteComponents("odata",new AOEntityDataModel().GetEntityDataModel(),
new DefaultODataBatchHandler())
.Select()
.Expand()
.OrderBy()
.SetMaxTop(10)
.Count()
.Filter());

builder.Services.AddDbContext<AODbContext>(options =>
{
    options.UseSqlServer(
        @"Server=(localdb)\mssqllocaldb;Database=AOData;Trusted_Connection=True;")
    .EnableSensitiveDataLogging();
});

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

app.UseHttpsRedirection();

app.UseODataBatching();

app.UseAuthorization();

app.MapControllers();

app.Run();

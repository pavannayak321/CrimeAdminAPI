using Azure.Storage.Blobs;
using CrimeAdminAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
string azureStorageConnectionString = builder.Configuration.GetSection("AzureStorage:ConnectionString").Value;
builder.Services.AddSingleton(new BlobServiceClient(azureStorageConnectionString));

builder.Services.AddCors(p => p.AddPolicy("corsPolicy", build =>
{
    build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
    
builder.Services.AddDbContext<CrimeDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("CrimeDbConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

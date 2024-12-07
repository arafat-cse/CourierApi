using CourierApi.Data;
using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);
//// Add services to the container
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp", builder =>
//    {
//        builder.WithOrigins("http://localhost:4200") // Angular origin
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//    });
//});

//// Add services to the container.
//builder.Services.AddDbContext<CourierDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("AppCon")));
////Add Code
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//    });


//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//// Configure the HTTP request pipeline
//app.UseCors("AllowAngularApp");
//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CourierDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppCon") ?? throw new InvalidOperationException("Connection string 'AppCon' not found.")));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Angular origin
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

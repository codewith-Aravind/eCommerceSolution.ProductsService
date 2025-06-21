using eCommerceSolution.ProductsService.BussinessLogicLayer;
using eCommerceSolution.ProductsService.DataAccessLayer;
using FluentValidation.AspNetCore;
using eCommerce.ProductsMicroService.API.Middleware;
using eCommerce.ProductsMicroService.API.APIEndpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add DAL and BAL services

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBussinessLogicLayer();


builder.Services.AddControllers();

//FluentValidation
builder.Services.AddFluentValidationAutoValidation();

// Add Model binder to read values from json
builder.Services.ConfigureHttpJsonOptions(_ =>
{
    _.SerializerOptions.Converters.Add(new JsonStringEnumConverter());

});

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Products Microservice API", Version = "v1" });
});

//Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

//Exception Middleware
app.UseExceptionHandlingMiddleware();
app.UseRouting();

//Cors 
app.UseCors();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products Microservice API v1"));
}


//Auth
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapProductEndpoints(); // Assuming this is defined in ProductAPIEndpoints.cs

app.Run();

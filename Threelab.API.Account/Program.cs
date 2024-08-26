using Microsoft.OpenApi.Models;
using Threelab.API.Account.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Threelab API", Version = "v1" });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabase(builder);
builder.Services.AddServices();

//App Run
var app = builder.Build();
DependencyExtension.ConfigApp(app);
using Threelab.API.User.Extensions;
using Threelab.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabase(builder);
var app = builder.Build();
DependencyExtension.ConfigApp(app);

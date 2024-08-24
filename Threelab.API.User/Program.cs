using Threelab.API.User.Extensions;
using Threelab.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
var app = builder.Build();
DependencyExtension.ConfigApp(app);
// Application/Extensions/ServiceCollectionExtensions.cs
using Application.Data;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddDbContext<TodoDbContext>(options =>
            options.UseInMemoryDatabase("TodoDb"));
            
        services.AddScoped<ITodoService, TodoService>();

        return services;
    }
}
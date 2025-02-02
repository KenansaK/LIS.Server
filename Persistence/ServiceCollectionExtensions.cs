using Kernal.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Implementation;

namespace Persistence;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterPersistence(this IServiceCollection services)
    {

        return services
             .AddTransient(typeof(IRepository<>), typeof(Repository<>))
             .AddTransient<IUnitOfWork, UnitOfWork>();
    }
}


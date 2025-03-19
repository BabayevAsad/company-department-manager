using System.Reflection;
using Company_Expense_Tracker.Services.AuthService;

namespace Company_Expense_Tracker.Services.RegisterServices;

public static class InjectServices
{
    public static void RegisterServices(this IServiceCollection services)
    {
        {
            var aa = Assembly.GetExecutingAssembly().GetExportedTypes()
                .Where(type => type.IsClass
                               && !type.IsAbstract
                               && type.GetInterfaces().Any(i =>
                                   i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseService<,,,>)))
                .ToList();

            aa.ForEach(type =>
            {
                var nestedInterface = type.GetInterfaces().First(i =>
                    !i.IsGenericType && i.GetInterfaces().Any(e =>
                        e.IsGenericType && e.GetGenericTypeDefinition() == typeof(IBaseService<,,,>)));
                services.AddScoped(nestedInterface, type);
            });
        }
        services.AddScoped<IUserService, UserService>(); 
        services.AddScoped<TokenService.TokenService>(); 
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VideoRayan.Application;
using VideoRayan.Application.Contract.AccountAgg.Contracts;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;
using VideoRayan.Domain.AccountAgg.Contracts;
using VideoRayan.Domain.CustomerAgg.Contracts;
using VideoRayan.Infrastructure.EfCore;
using VideoRayan.Infrastructure.EfCore.Repositories.AccountAgg;
using VideoRayan.Infrastructure.EfCore.Repositories.CustomerAgg;

namespace VideoRayan.Infrastructure.Configuration;
public static class VideoRayanBootstrapper
{
    public static void Configure(IServiceCollection services , string connectionString)
    {
        #region ConfigContext
        
        services.AddDbContext<VideoRayanContext>(o => o.UseSqlServer(connectionString));

        #endregion

        #region Account

        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IRoleApplication, RoleApplication>();

        services.AddTransient<IOperatorRepository, OperatorRepository>();
        services.AddTransient<IOperatorApplication, OperatorApplication>();

        services.AddTransient<IPermissionRepository, PermissionRepository>();
        services.AddTransient<IPermissionApplication, PermissionApplication>();

        services.AddTransient<IRolePermissionRepository, RolePermissionRepository>();
        services.AddTransient<IRolePermissionApplication, RolePermissionApplication>();

        #endregion

        #region Customer

        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<ICustomerApplication, CustomerApplication>();

        #endregion
    }
}
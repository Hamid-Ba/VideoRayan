﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VideoRayan.Application;
using VideoRayan.Application.Contract.AccountAgg.Contracts;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;
using VideoRayan.Application.Contract.PlanAgg.Contracts;
using VideoRayan.Domain.AccountAgg.Contracts;
using VideoRayan.Domain.CustomerAgg.Contracts;
using VideoRayan.Domain.MeetingAgg.Repositories;
using VideoRayan.Domain.PlanAgg.Contracts;
using VideoRayan.Infrastructure.EfCore;
using VideoRayan.Infrastructure.EfCore.Repositories;
using VideoRayan.Infrastructure.EfCore.Repositories.AccountAgg;
using VideoRayan.Infrastructure.EfCore.Repositories.CustomerAgg;
using VideoRayan.Infrastructure.EfCore.Repositories.MeetingAgg;
using VideoRayan.Infrastructure.EfCore.Repositories.PlanAgg;

namespace VideoRayan.Infrastructure.Configuration;
public static class VideoRayanBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
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

        services.AddTransient<IAudienceRepository, AudienceRepository>();
        services.AddTransient<IAudienceApplication, AudienceApplication>();

        #endregion

        #region Subscriptions

        services.AddTransient<IPlanRepository, PlanRepository>();
        services.AddTransient<IPlanApplication, PlanApplication>();

        #endregion

        #region Meeting

        services.AddTransient<IMeetingRepository, MeetingRepository>();
        services.AddTransient<IMeetingApplication, MeetingApplication>();

        services.AddTransient<IFaceToFaceRepository, FaceToFaceRepository>();
        services.AddTransient<IFaceToFaceApplication, FaceToFaceApplication>();

        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<ICategoryApplication, CategoryApplication>();

        #endregion

        services.AddTransient<IAudienceMeetingRepository, AudienceMeetingRepository>();
        services.AddTransient<IAudienceMeetingApplication, AudienceMeetingApplication>();

        services.AddTransient<IAudienceFaceToFaceRepository, AudienceFaceToFaceRepository>();
        services.AddTransient<IAudienceFaceToFaceApplication, AudienceFaceToFaceApplication>();
    }
}
using Infrastructure.DB;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;

namespace Infrastructure.DependencyInjection;

public static class DI
{
    public static WebApplicationBuilder AddInfraDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LoanDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));
        return builder;
    }

    public static WebApplicationBuilder AddDbServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ILoanRepository, LoanRepository>();
        builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
        return builder;
    }

    public static async Task<WebApplication> CreateMigration(this WebApplication app, AsyncServiceScope scope)
    {

        using var appContext = scope.ServiceProvider.GetRequiredService<LoanDbContext>();
        await appContext.Database.MigrateAsync();

        return app;
    }
}

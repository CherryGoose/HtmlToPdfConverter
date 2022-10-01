using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Html.Converter.Data;
using Volo.Abp.DependencyInjection;

namespace Html.Converter.EntityFrameworkCore;

public class EntityFrameworkCoreConverterDbSchemaMigrator
    : IConverterDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreConverterDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ConverterDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ConverterDbContext>()
            .Database
            .MigrateAsync();
    }
}

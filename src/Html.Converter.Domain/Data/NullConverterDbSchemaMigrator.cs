using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Html.Converter.Data;

/* This is used if database provider does't define
 * IConverterDbSchemaMigrator implementation.
 */
public class NullConverterDbSchemaMigrator : IConverterDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

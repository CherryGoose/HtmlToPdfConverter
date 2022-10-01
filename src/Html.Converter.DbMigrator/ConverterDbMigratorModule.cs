using Html.Converter.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Html.Converter.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ConverterEntityFrameworkCoreModule),
    typeof(ConverterApplicationContractsModule)
    )]
public class ConverterDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}

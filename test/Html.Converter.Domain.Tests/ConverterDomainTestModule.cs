using Html.Converter.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Html.Converter;

[DependsOn(
    typeof(ConverterEntityFrameworkCoreTestModule)
    )]
public class ConverterDomainTestModule : AbpModule
{

}

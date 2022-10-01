using Volo.Abp.Modularity;

namespace Html.Converter;

[DependsOn(
    typeof(ConverterApplicationModule),
    typeof(ConverterDomainTestModule)
    )]
public class ConverterApplicationTestModule : AbpModule
{

}

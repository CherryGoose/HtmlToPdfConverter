using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Html.Converter.Web;

[Dependency(ReplaceServices = true)]
public class ConverterBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Converter";
}

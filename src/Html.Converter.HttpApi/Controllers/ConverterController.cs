using Html.Converter.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Html.Converter.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ConverterController : AbpControllerBase
{
    protected ConverterController()
    {
        LocalizationResource = typeof(ConverterResource);
    }
}

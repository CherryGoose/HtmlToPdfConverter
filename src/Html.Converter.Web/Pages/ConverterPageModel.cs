using Html.Converter.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Html.Converter.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ConverterPageModel : AbpPageModel
{
    protected ConverterPageModel()
    {
        LocalizationResourceType = typeof(ConverterResource);
    }
}

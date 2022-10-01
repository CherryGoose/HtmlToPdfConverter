using System;
using System.Collections.Generic;
using System.Text;
using Html.Converter.Localization;
using Volo.Abp.Application.Services;

namespace Html.Converter;

/* Inherit your application services from this class.
 */
public abstract class ConverterAppService : ApplicationService
{
    protected ConverterAppService()
    {
        LocalizationResource = typeof(ConverterResource);
    }
}

﻿using Volo.Abp.Settings;

namespace Html.Converter.Settings;

public class ConverterSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ConverterSettings.MySetting1));
    }
}

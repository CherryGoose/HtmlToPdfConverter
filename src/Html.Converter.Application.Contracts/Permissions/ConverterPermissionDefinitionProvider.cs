using Html.Converter.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Html.Converter.Permissions;

public class ConverterPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ConverterPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ConverterPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ConverterResource>(name);
    }
}

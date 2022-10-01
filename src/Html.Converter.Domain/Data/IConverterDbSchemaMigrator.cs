using System.Threading.Tasks;

namespace Html.Converter.Data;

public interface IConverterDbSchemaMigrator
{
    Task MigrateAsync();
}

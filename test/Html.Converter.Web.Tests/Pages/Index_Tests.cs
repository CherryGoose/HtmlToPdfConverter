using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Html.Converter.Pages;

public class Index_Tests : ConverterWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}

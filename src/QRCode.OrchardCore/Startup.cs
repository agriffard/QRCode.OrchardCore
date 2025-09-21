using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using QRCode.OrchardCore.Drivers;
using QRCode.OrchardCore.Models;
using QRCode.OrchardCore.Services;

namespace QRCode.OrchardCore
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Register the QR code service
            services.AddScoped<IQRCodeService, QRCodeService>();

            services.AddContentPart<QRCodePart>()
                .UseDisplayDriver<QRCodePartDisplayDriver>();
            services.AddDataMigration<Migrations>();
        }
    }
}

using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using QRCode.OrchardCore.Models;
using QRCode.OrchardCore.ViewModels;
using System.Threading.Tasks;

namespace QRCode.OrchardCore.Drivers
{
    public class QRCodePartDisplayDriver : ContentPartDisplayDriver<QRCodePart>
    {
        public override IDisplayResult Display(QRCodePart part, BuildPartDisplayContext context)
        {
            return Initialize<QRCodePartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
                .Location("Detail", "Content:20")
                .Location("Summary", "Content:20");
        }

        public override IDisplayResult Edit(QRCodePart part, BuildPartEditorContext context)
        {
            return Initialize<QRCodePartViewModel>(GetEditorShapeType(context), model =>
            {
                model.ValueSource = part.ValueSource;
                model.CustomValue = part.CustomValue;
                model.Size = part.Size;
                model.ForegroundColor = part.ForegroundColor;
                model.BackgroundColor = part.BackgroundColor;
                model.ErrorCorrection = part.ErrorCorrection;
                model.Format = part.Format;
                model.ContentItem = part.ContentItem;
                model.QRCodePart = part;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(QRCodePart model, UpdatePartEditorContext context)
        {
            await context.Updater.TryUpdateModelAsync(model, Prefix, 
                t => t.ValueSource,
                t => t.CustomValue,
                t => t.Size,
                t => t.ForegroundColor,
                t => t.BackgroundColor,
                t => t.ErrorCorrection,
                t => t.Format);

            return Edit(model, context);
        }

        private Task BuildViewModel(QRCodePartViewModel model, QRCodePart part, BuildPartDisplayContext context)
        {
            model.ValueSource = part.ValueSource;
            model.CustomValue = part.CustomValue;
            model.Size = part.Size;
            model.ForegroundColor = part.ForegroundColor;
            model.BackgroundColor = part.BackgroundColor;
            model.ErrorCorrection = part.ErrorCorrection;
            model.Format = part.Format;
            model.ContentItem = part.ContentItem;
            model.QRCodePart = part;

            return Task.CompletedTask;
        }
    }
}
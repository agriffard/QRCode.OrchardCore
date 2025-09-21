using OrchardCore.ContentManagement;
using QRCode.OrchardCore.Models;

namespace QRCode.OrchardCore.ViewModels
{
    public class QRCodePartViewModel
    {
        public string ValueSource { get; set; } = "ContentUrl";
        public string CustomValue { get; set; } = "";
        public int Size { get; set; } = 250;
        public string ForegroundColor { get; set; } = "#000000";
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public string ErrorCorrection { get; set; } = "Q";
        public string Format { get; set; } = "png";

        public QRCodePart QRCodePart { get; set; }
        public ContentItem ContentItem { get; set; }
    }
}
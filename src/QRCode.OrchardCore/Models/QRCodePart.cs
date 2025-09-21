using OrchardCore.ContentManagement;

namespace QRCode.OrchardCore.Models
{
    public class QRCodePart : ContentPart
    {
        // Source of the text to encode. Examples: "ContentUrl", "Field:MyField", "Custom"
        public string ValueSource { get; set; } = "ContentUrl";

        // If ValueSource == "Custom" this value is used.
        public string CustomValue { get; set; }

        // Size in pixels (square)
        public int Size { get; set; } = 250;

        // Foreground color in hex, e.g. #000000
        public string ForegroundColor { get; set; } = "#000000";

        // Background color in hex
        public string BackgroundColor { get; set; } = "#FFFFFF";

        // Error correction level: L,M,Q,H
        public string ErrorCorrection { get; set; } = "Q";

        // Output format: png or svg
        public string Format { get; set; } = "png";
    }
}

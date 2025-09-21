# QRCode.OrchardCore

An OrchardCore module that provides QR code generation functionality for your content items.

## Features

- **QRCodePart**: A content part that can be attached to any content type to generate QR codes
- **Flexible Value Sources**: Generate QR codes from content URLs, custom fields, or custom values
- **Customizable Appearance**: Configure size, colors (foreground/background), and error correction levels
- **Multiple Output Formats**: Support for PNG and SVG formats
- **Easy Integration**: Simple Razor view for displaying QR codes in your templates

## Installation

1. Clone or download this module to your OrchardCore application's `Modules` folder
2. Build the solution
3. Enable the "QRCode" module in the OrchardCore admin dashboard

## Usage

### Adding QRCode Part to Content Types

1. Go to **Content Definition** > **Content Types** in the admin dashboard
2. Edit your desired content type (e.g., Page, BlogPost)
3. Add the **QRCodePart** to the content type
4. Configure the part settings as needed

### Configuration Options

The QRCodePart provides the following configuration options:

| Property | Description | Default Value | Options |
|----------|-------------|---------------|---------|
| **ValueSource** | Source of text to encode | `"ContentUrl"` | `"ContentUrl"`, `"Field:FieldName"`, `"Custom"` |
| **CustomValue** | Custom text when ValueSource is "Custom" | `""` | Any string |
| **Size** | QR code size in pixels (square) | `250` | Any positive integer |
| **ForegroundColor** | Foreground color in hex format | `"#000000"` | Hex color (e.g., `#FF0000`) |
| **BackgroundColor** | Background color in hex format | `"#FFFFFF"` | Hex color (e.g., `#F0F0F0`) |
| **ErrorCorrection** | Error correction level | `"Q"` | `"L"`, `"M"`, `"Q"`, `"H"` |
| **Format** | Output format | `"png"` | `"png"`, `"svg"` |

### Value Source Options

- **ContentUrl**: Generates QR code for the content item's URL
- **Field:FieldName**: Uses the value from a specific field (replace `FieldName` with actual field name)
- **Custom**: Uses the value specified in the `CustomValue` property

### Error Correction Levels

- **L**: Low (~7% recovery)
- **M**: Medium (~15% recovery)
- **Q**: Quartile (~25% recovery) - **Default**
- **H**: High (~30% recovery)

## Display in Templates

The module includes a default Razor view (`Views/Parts/QRCodePart.cshtml`) that automatically renders the QR code as an HTML `<img>` element with base64-encoded data.

### Example Usage in Liquid Templates

```liquid
{% assign qrCodePart = Model.ContentItem | content_part: "QRCodePart" %}
{% if qrCodePart %}
    {{ qrCodePart | shape_render }}
{% endif %}
```

### Example Usage in Razor Templates

```razor
@{
    var qrCodePart = Model.ContentItem.As<QRCodePart>();
}

@if (qrCodePart != null)
{
    @await DisplayAsync(await New.QRCodePart(ContentPart: qrCodePart, ContentItem: Model.ContentItem))
}
```

## Technical Details

### Dependencies

- **.NET 8**: Target framework
- **OrchardCore.ContentManagement**: For content part functionality
- **QRCoder**: QR code generation library
- **System.Drawing.Common**: For color handling

### Architecture

The module consists of:

- **QRCodePart**: Content part model with configuration properties
- **IQrCodeService/QrCodeService**: Service for QR code generation
- **Startup**: Module registration and dependency injection setup
- **QRCodePart.cshtml**: Default Razor view for rendering QR codes

### Code Generation

The QR code generation supports:
- PNG format using `BitmapByteQRCode` from QRCoder library
- SVG format with custom SVG generation logic
- Configurable colors via hex color codes
- Multiple error correction levels
- Customizable sizing

## Examples

### Basic QR Code for Content URL
```csharp
var qrCodePart = new QRCodePart
{
    ValueSource = "ContentUrl",
    Size = 200,
    ForegroundColor = "#000000",
    BackgroundColor = "#FFFFFF",
    ErrorCorrection = "M",
    Format = "png"
};
```

### QR Code from Custom Field
```csharp
var qrCodePart = new QRCodePart
{
    ValueSource = "Field:ContactInfo",
    Size = 300,
    ForegroundColor = "#0066CC",
    BackgroundColor = "#F8F8F8",
    ErrorCorrection = "H",
    Format = "svg"
};
```

### Custom QR Code
```csharp
var qrCodePart = new QRCodePart
{
    ValueSource = "Custom",
    CustomValue = "https://example.com/special-link",
    Size = 150,
    ForegroundColor = "#FF6600",
    BackgroundColor = "#FFFFFF",
    ErrorCorrection = "Q",
    Format = "png"
};
```

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues on the GitHub repository.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For questions, issues, or feature requests, please use the GitHub issue tracker.
using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "QRCode",
    Author = "Antoine Griffard",
    Website = "https://github.com/agriffard/QRCode.OrchardCore",
    Version = "0.0.1",
    Description = "Provides QR code generation functionality for content items.",
    Category = "Content",
    Dependencies = new[] { "OrchardCore.ContentManagement" }
)]
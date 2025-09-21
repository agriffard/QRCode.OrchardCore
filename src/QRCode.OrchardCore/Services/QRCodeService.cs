using QRCoder;
using System;
using System.Threading.Tasks;

namespace QRCode.OrchardCore.Services
{
    public class QRCodeService : IQRCodeService
    {
        public Task<byte[]> GenerateAsync(string text, int size = 250, string fg = "#000000", string bg = "#FFFFFF", string ecc = "Q", string format = "png")
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("text must not be empty", nameof(text));

            QRCodeGenerator.ECCLevel eccLevel = ecc?.ToUpper() switch
            {
                "L" => QRCodeGenerator.ECCLevel.L,
                "M" => QRCodeGenerator.ECCLevel.M,
                "Q" => QRCodeGenerator.ECCLevel.Q,
                "H" => QRCodeGenerator.ECCLevel.H,
                _ => QRCodeGenerator.ECCLevel.Q
            };

            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(text, eccLevel);

            if (format?.ToLower() == "svg")
            {
                // For SVG, we'll generate a simple SVG string
                var svg = GenerateSvg(qrData, size, fg, bg);
                return Task.FromResult(System.Text.Encoding.UTF8.GetBytes(svg));
            }

            // PNG by default
            var png = GeneratePngBytes(qrData, size, fg, bg);
            return Task.FromResult(png);
        }

        private byte[] GeneratePngBytes(QRCodeData qrData, int size, string fg, string bg)
        {
            // Convert hex colors to byte arrays (RGB)
            var foregroundColor = HexToByteArray(fg);
            var backgroundColor = HexToByteArray(bg);
            
            using var qrCode = new BitmapByteQRCode(qrData);
            int pixelsPerModule = Math.Max(1, size / 25);
            return qrCode.GetGraphic(pixelsPerModule, foregroundColor, backgroundColor);
        }

        private string GenerateSvg(QRCodeData qrData, int size, string fg, string bg)
        {
            // Simple SVG generation - this is a basic implementation
            // For production, you might want to use a more sophisticated SVG generator
            var modules = qrData.ModuleMatrix;
            var moduleSize = size / modules.Count;
            
            var svg = $@"<svg width=""{size}"" height=""{size}"" xmlns=""http://www.w3.org/2000/svg"">
<rect width=""{size}"" height=""{size}"" fill=""{bg}""/>
<g fill=""{fg}"">";

            for (int row = 0; row < modules.Count; row++)
            {
                for (int col = 0; col < modules[row].Count; col++)
                {
                    if (modules[row][col])
                    {
                        var x = col * moduleSize;
                        var y = row * moduleSize;
                        svg += $@"<rect x=""{x}"" y=""{y}"" width=""{moduleSize}"" height=""{moduleSize}""/>";
                    }
                }
            }

            svg += "</g></svg>";
            return svg;
        }

        private byte[] HexToByteArray(string hex)
        {
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);
            
            if (hex.Length != 6)
                throw new ArgumentException("Invalid hex color format");

            return new byte[]
            {
                Convert.ToByte(hex.Substring(0, 2), 16), // R
                Convert.ToByte(hex.Substring(2, 2), 16), // G
                Convert.ToByte(hex.Substring(4, 2), 16)  // B
            };
        }
    }
}

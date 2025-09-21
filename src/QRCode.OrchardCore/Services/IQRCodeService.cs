using System.IO;
using System.Threading.Tasks;

namespace QRCode.OrchardCore.Services
{
    public interface IQRCodeService
    {
        Task<byte[]> GenerateAsync(string text, int size = 250, string fg = "#000000", string bg = "#FFFFFF", string ecc = "Q", string format = "png");
    }
}

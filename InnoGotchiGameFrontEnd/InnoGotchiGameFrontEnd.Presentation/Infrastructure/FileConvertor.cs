using Microsoft.AspNetCore.Components.Forms;

namespace InnoGotchiGameFrontEnd.Presentation.Infrastructure
{
    public class FileConvertor
    {
        private IConfiguration _config;
        public FileConvertor(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<byte[]> ConvertImageToByteArrayAsync(IBrowserFile file, string format)
        {
            int imgSize = int.Parse(_config.GetSection("ImageSizePx").Value);
            file = await file.RequestImageFileAsync(format, imgSize, imgSize);

            var stream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(stream);
            return stream.ToArray();
        }
    }
}

using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.Model.Authorize;

namespace InnoGotchiGameFrontEnd.Web.Middleware
{
    public class BasePetViewInitializer
    {
        private RequestDelegate _next;
        private bool _isInitialize = false;

        public BasePetViewInitializer(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, PictureManager manager)
        {
            if(!_isInitialize)
            {
                if ((await manager.GetAllPictures("petView-nose")).Count() == 0)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        await AddImageToDb($"wwwroot/Resourses/Bodies/body{i}.svg", "petView-body", manager);
                    }
                    for (int i = 1; i < 7; i++)
                    {
                        await AddImageToDb($"wwwroot/Resourses/Eyes/eyes{i}.svg", "petView-eyes", manager);
                    }
                    for (int i = 1; i < 6; i++)
                    {
                        await AddImageToDb($"wwwroot/Resourses/Mouths/mouth{i}.svg", "petView-mouth", manager);
                    }
                    for (int i = 1; i < 7; i++)
                    {
                        await AddImageToDb($"wwwroot/Resourses/Noses/nose{i}.svg", "petView-nose", manager);
                    }
                }
                _isInitialize = true;
            }
            await _next.Invoke(httpContext);
        }

        private async Task AddImageToDb(string path, string nameTemplate, PictureManager manager)
        {
            using var stream = new MemoryStream(File.ReadAllBytes(path).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", path.Split(@"\").Last());
            await manager.Create(formFile, nameTemplate);
        }
    }
}

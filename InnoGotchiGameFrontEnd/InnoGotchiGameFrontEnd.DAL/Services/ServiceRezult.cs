using InnoGotchiGameFrontEnd.Domain;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class ServiceResult : IServiceResult
    {
        public bool IsComplete => Errors.Count() == 0;
        public List<string> Errors { get; }

        public ServiceResult()
        {
            Errors = new List<string>();
        }
    }
}

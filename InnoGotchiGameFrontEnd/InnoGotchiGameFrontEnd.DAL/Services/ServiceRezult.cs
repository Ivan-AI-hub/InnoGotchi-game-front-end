using InnoGotchiGameFrontEnd.Domain;

namespace InnoGotchiGameFrontEnd.DAL.Services
{
    public class ServiceRezult : IServiceRezult
    {
        public bool IsComplete => Errors.Count() == 0;
        public List<string> Errors { get; }

        public ServiceRezult()
        {
            Errors = new List<string>();
        }
    }
}

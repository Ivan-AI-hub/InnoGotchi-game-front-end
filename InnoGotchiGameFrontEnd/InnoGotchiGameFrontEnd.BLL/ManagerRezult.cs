using FluentValidation.Results;

namespace InnoGotchiGameFrontEnd.BLL
{
    public class ManagerRezult
    {
        public bool IsComplete => Errors.Count() == 0;
        public List<string> Errors { get; }

        public ManagerRezult(params string[] errors)
        {
            Errors = errors.ToList();
        }

        public ManagerRezult(ValidationResult validationResult)
        {
            Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
        }
    }
}

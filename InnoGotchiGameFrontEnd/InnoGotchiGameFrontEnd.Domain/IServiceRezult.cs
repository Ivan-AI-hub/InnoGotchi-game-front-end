namespace InnoGotchiGameFrontEnd.Domain
{
    public interface IServiceRezult
    {
        List<string> Errors { get; }
        bool IsComplete { get; }
    }
}
namespace InnoGotchiGameFrontEnd.Domain
{
    public interface IServiceResult
    {
        List<string> Errors { get; }
        bool IsComplete { get; }
    }
}
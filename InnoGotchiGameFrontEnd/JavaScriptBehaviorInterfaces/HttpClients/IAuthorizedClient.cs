namespace AuthorizationInfrastructure.HttpClients
{
    public interface IAuthorizedClient
    {
        Uri? BaseAddress { get; }
        Task<HttpClient> GetHttpClientAsync();
    }
}

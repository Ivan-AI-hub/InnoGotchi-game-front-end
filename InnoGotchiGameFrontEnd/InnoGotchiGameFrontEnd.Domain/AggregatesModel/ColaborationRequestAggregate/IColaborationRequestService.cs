namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.ColaborationRequestAggregate
{
    public interface IColaborationRequestService
    {
        Task<IServiceRezult> ConfirmAsync(int requestId, CancellationToken cancellationToken = default);
        Task<IServiceRezult> CreateAsync(int recipientId, CancellationToken cancellationToken = default);
        Task<IServiceRezult> DeleteByIdAsync(int requestId, CancellationToken cancellationToken = default);
        Task<IServiceRezult> RejectAsync(int requestId, CancellationToken cancellationToken = default);
    }
}
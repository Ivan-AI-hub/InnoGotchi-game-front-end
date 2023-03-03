namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.ColaborationRequestAggregate
{
    public interface IColaborationRequestService
    {
        Task<IServiceResult> ConfirmAsync(int requestId, CancellationToken cancellationToken = default);
        Task<IServiceResult> CreateAsync(int recipientId, CancellationToken cancellationToken = default);
        Task<IServiceResult> DeleteByIdAsync(int requestId, CancellationToken cancellationToken = default);
        Task<IServiceResult> RejectAsync(int requestId, CancellationToken cancellationToken = default);
    }
}
namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate
{
    public interface IPictureService
    {
        Task<IServiceResult> CreateAsync(Picture picture, CancellationToken cancellationToken = default);
        Task<IEnumerable<Picture>?> GetAsync(PictureFiltrator filtrator, CancellationToken cancellationToken = default);
        Task<Picture?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IServiceResult> UpdateAsync(int updatedId, Picture picture, CancellationToken cancellationToken = default);
    }
}
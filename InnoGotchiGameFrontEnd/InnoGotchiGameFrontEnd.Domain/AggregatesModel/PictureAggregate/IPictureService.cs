namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate
{
    public interface IPictureService
    {
        Task<IServiceRezult> CreateAsync(Picture picture, CancellationToken cancellationToken = default);
        Task<IEnumerable<Picture>?> GetAsync(PictureFiltrator filtrator, CancellationToken cancellationToken = default);
        Task<Picture?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IServiceRezult> UpdateAsync(int updatedId, Picture picture, CancellationToken cancellationToken = default);
    }
}
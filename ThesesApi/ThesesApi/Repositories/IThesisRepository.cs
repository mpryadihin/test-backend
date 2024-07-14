using ThesesApi.Models;

namespace ThesesApi;

public interface IThesisRepository
{
    Task<Thesis?> getByIdAsync(long id);
    Task<IEnumerable<Thesis>> getAllTheseAsync();
    Task addAsync(Thesis thesis);
    Task updateAsync(Thesis thesis);
    Task deleteAsync(Thesis thesis);
}

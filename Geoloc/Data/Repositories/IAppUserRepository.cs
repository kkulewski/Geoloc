using Geoloc.Models.Entities;

namespace Geoloc.Data.Repositories
{
    public interface IAppUserRepository
    {
        AppUser Get(string id);
    }
}
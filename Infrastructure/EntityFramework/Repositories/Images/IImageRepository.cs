using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IImageRepository
{
    DbImage Create(DbImage image);
    bool Delete(DbImage image);
    DbImage FetchByPath(string path);
}
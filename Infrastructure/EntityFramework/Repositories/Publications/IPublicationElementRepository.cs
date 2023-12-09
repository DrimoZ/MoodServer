using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Publications;

public interface IPublicationElementRepository
{
    DbPublicationElement Create(DbPublicationElement publication);
    bool Delete(int id);
    DbPublicationElement FetchById(int elementId);
    IEnumerable<DbPublicationElement> FetchElementsByPublicationId(int id);
}
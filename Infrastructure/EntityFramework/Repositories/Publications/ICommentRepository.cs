using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Publications;

public interface ICommentElementRepository
{
    DbComment Create(DbComment comment);
    bool Delete(int commentId);
    DbComment FetchById(int commId);
    IEnumerable<DbComment> FetchCommentsByPublicationId(int pubId);
}
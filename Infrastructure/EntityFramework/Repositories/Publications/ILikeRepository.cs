using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Publications;

public interface ILikeRepository
{
    DbLike Create(DbLike like);
    bool Delete(int likeId);
    DbLike FetchById(int likeId);
    IEnumerable<DbLike> FetchLikesByPublicationId(int pubId);
    int FetchLikeCountByPublicationId(int pubId);
}
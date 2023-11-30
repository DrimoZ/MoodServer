using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public class PublicationRepository:IPublicationRepository
{
    private readonly MoodContext _context;

    public PublicationRepository(MoodContext context)
    {
        _context = context;
    }

    public Publication? Get(int id)
    {
        throw new NotImplementedException();
    }

    public Publication Create(DbPublication publication)
    {
        throw new NotImplementedException();
    }

    public bool Update(DbPublication publication)
    {
        throw new NotImplementedException();
    }

    public bool Delete(string id)
    {
        throw new NotImplementedException();
    }

    public List<DbPublication> FetchByIdUser(string userId)
    {
        return _context.Publications
            .Select(history => history)
            .Where(history => history.UserId == userId)
            .ToList();
    }
}
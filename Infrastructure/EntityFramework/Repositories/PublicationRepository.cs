using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public class PublicationRepository:IPublicationRepository
{
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

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<DbPublication> FetchByIdUser(int userId)
    {
        throw new NotImplementedException();
    }
}
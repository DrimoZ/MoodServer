using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface ICommunicationRepository
{
    public DbCommunication Create(DbCommunication com);
}
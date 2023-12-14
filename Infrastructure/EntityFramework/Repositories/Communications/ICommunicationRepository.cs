using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Communications;

public interface ICommunicationRepository
{
    public DbCommunication Create(DbCommunication com);
    public DbCommunication GetById(int id);
}
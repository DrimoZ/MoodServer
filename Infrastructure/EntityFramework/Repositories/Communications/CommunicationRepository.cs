using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Communications;

public class CommunicationRepository:ICommunicationRepository
{
    private readonly MoodContext _context;

    public CommunicationRepository(MoodContext context)
    {
        _context = context;
    }

    public DbCommunication Create(DbCommunication com)
    {
        _context.Communications.Add(com);
        return com;
    }

    public DbCommunication GetById(int id)
    {
       var entity =  _context.Communications.FirstOrDefault(communication => communication.Id == id);
       if (entity == null) throw new KeyNotFoundException("Com not found");
       return entity;
    }
}
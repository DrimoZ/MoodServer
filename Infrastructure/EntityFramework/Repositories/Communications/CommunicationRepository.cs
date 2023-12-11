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
}
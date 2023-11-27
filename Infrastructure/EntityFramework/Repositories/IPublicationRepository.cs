using Domain;

namespace Infrastructure.EntityFramework.Repositories;

public interface IPublicationRepository
{
    Publication? Get(int id);
    Publication Create(Publication publication);
    bool Update(Publication publication);
    bool Delete(int id);
}
using Application.UseCases.Utils;

namespace Application.UseCases.Publications;

public class UseCaseSetPublicationDeleted:IUseCaseParameterizedWriter<bool, string, bool>
{
    public bool Execute(string id, bool isDeleted)
    {
        
    }
}
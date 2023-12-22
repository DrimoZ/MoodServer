namespace Domain.Exception;

[Serializable]
public class DeletedUserException: System.Exception
{
    public DeletedUserException()
    {
    }

    public DeletedUserException(string message)
        : base(message)
    {
    }

    public DeletedUserException(string message, System.Exception inner)
        : base(message, inner)
    {
    }
}
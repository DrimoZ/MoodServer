namespace Application.UseCases.Utils;

public interface IUseCaseQuery<out TOutput>
{
    TOutput Execute();
}
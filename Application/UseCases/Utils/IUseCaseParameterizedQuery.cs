namespace Application.UseCases.Utils;

public interface IUseCaseParameterizedQuery<out TOutput, in TParam>
{
    TOutput Execute(TParam param);
}

public interface IUseCaseParameterizedQuery<out TOutput, in TParam1, in TParam2>
{
    TOutput Execute(TParam1 connectedUserId, TParam2 profileRequestUserId);
}
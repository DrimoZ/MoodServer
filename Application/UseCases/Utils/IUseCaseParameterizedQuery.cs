namespace Application.UseCases.Utils;

public interface IUseCaseParameterizedQuery<out TOutput, in TParam>
{
    TOutput Execute(TParam param);
}

public interface IUseCaseParameterizedQuery<out TOutput, in TParam1, in TParam2>
{
    TOutput Execute(TParam1 param1, TParam2 param2);
}
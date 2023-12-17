namespace Application.UseCases.Utils;

public interface IUseCaseWriter<out TOutput, in TInput>
{
    TOutput Execute(TInput input);
}

public interface IUseCaseWriter<out TOutput, in TInput1, in TInput2>
{
    TOutput Execute(TInput1 input1, TInput2 input2);
}
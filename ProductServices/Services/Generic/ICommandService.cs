namespace ProductServices.Services.Generic;

public interface ICommandService<TCommand>
{
    void Execute(TCommand command);
}

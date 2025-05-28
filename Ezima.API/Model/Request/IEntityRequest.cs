namespace Ezima.API.Model.Request;

public interface IEntityRequest<T>
{
    T ToEntity();
}
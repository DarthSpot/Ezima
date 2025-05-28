namespace Ezima.API.Model.Request;

public class ChildCreationRequest : IEntityRequest<Child>
{
    public string Name { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }

    public Child ToEntity()
    {
        return new Child()
        {
            Birthday = Birthday,
            Name = Name
        };
    }
}
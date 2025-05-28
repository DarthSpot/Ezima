using Ezima.API.Model;

namespace Ezima.API.Repository;

public class UserRepository : IRepository<User>
{
    public Task<IEnumerable<User?>> FindAll()
    {
        throw new NotImplementedException();
    }

    public Task<User?> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> Save(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> Update(User entity)
    {
        throw new NotImplementedException();
    }

    public Task Clear()
    {
        throw new NotImplementedException();
    }

    public Task<int> Count()
    {
        throw new NotImplementedException();
    }
}
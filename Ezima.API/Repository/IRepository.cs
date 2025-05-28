namespace Ezima.API.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity?>> FindAll();
    Task<TEntity?> FindById(int id);
    Task<TEntity?> Save(TEntity entity);
    Task<bool> Delete(TEntity entity);
    Task<bool> DeleteById(int id);
    Task<TEntity?> Update(TEntity entity);
    Task Clear();
    Task<int> Count();
}
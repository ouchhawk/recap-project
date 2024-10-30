using _Core.Entities;
using System.Linq.Expressions;

namespace _Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        bool Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
    }
}

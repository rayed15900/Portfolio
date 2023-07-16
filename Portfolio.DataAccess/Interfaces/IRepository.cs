using Portfolio.Models.Base;
using System.Linq.Expressions;

namespace Portfolio.DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseModel
	{
		Task CreateAsync(T entity);
		Task<List<T>> GetAllAsync();
		Task<T> FindAsync(object id);
		void Update(T entity, T oldEntity);
		void Remove(T entity);
	}
}

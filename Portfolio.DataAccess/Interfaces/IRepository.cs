using Portfolio.Models;
using System.Linq.Expressions;

namespace Portfolio.DataAccess.Interfaces
{
	public interface IRepository<T> where T : BaseEntity
	{
		Task CreateAsync(T entity);
		Task<List<T>> GetAllAsync();
		Task<T> FindAsync(object id);
		void Update(T entity, T unchanged);
		void Remove(T entity);
		void RemoveRange(List<T> entities);
	}
}

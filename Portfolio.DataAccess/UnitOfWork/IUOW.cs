using Portfolio.DataAccess.Interfaces;
using Portfolio.Models;

namespace Portfolio.DataAccess.UnitOfWork
{
	public interface IUOW
	{
		IRepository<T> GetRepository<T>() where T : BaseEntity;
		Task SaveChangesAsync();
	}
}

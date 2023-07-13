using Portfolio.DataAccess.Interfaces;
using Portfolio.Models.Base;

namespace Portfolio.DataAccess.UnitOfWork
{
    public interface IUOW
	{
		IRepository<T> GetRepository<T>() where T : BaseModel;
		Task SaveChangesAsync();
	}
}

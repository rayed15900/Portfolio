using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.Context;
using Portfolio.DataAccess.Interfaces;
using Portfolio.Models.Base;
using System.Linq.Expressions;

namespace Portfolio.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
	{
		private readonly PortfolioContext _context;

		public Repository(PortfolioContext context)
		{
			_context = context;
		}

		#region Create

		public async Task CreateAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
		}

		#endregion

		#region Read

		public async Task<List<T>> GetAllAsync()
		{
			return await _context.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<T> FindAsync(object id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		#endregion

		#region Update

		public void Update(T entity, T oldEntity)
		{
			_context.Entry(oldEntity).CurrentValues.SetValues(entity);
		}

		#endregion

		#region Delete

		public void Remove(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		#endregion
	}
}

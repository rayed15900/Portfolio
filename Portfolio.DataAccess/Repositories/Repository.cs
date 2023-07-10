using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.Context;
using Portfolio.DataAccess.Interfaces;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DataAccess.Repositories
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
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

		public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false)
		{
			return !asNoTracking ? await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter) : await _context.Set<T>().SingleOrDefaultAsync(filter);
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

		public void RemoveRange(List<T> entities)
		{
			_context.Set<T>().RemoveRange(entities);
		}

		#endregion
	}
}

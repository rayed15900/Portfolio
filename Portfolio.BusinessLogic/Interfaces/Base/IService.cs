using Portfolio.BusinessLogic.DTOs.Interfaces;
using Portfolio.Models;
using Portfolio.Utility;

namespace Portfolio.BusinessLogic.Interfaces.Base
{
	public interface IService<CreateDTO, UpdateDTO, ListDTO, T>
		where CreateDTO : class, IDTO, new()
		where UpdateDTO : class, IUpdateDTO, new()
		where ListDTO : class, IDTO, new()
		where T : BaseEntity
	{
		Task<IResponse<CreateDTO>> CreateAsync(CreateDTO dto);
		Task<IResponse<List<ListDTO>>> GetAllAsync();
		Task<IResponse<IDTO>> GetByIdAsync<IDTO>(int id);
		Task<IResponse<UpdateDTO>> UpdateAsync(UpdateDTO dto);
		Task<IResponse> RemoveAsync(int id);
	}
}

using Portfolio.BusinessLogic.DTOs.Interfaces;
using Portfolio.Models.Base;

namespace Portfolio.BusinessLogic.Interfaces.Base
{
    public interface IService<CreateDTO, UpdateDTO, ListDTO, T>
        where CreateDTO : class, IDTO, new()
        where UpdateDTO : class, IUpdateDTO, new()
        where ListDTO : class, IDTO, new()
        where T : BaseModel
    {
        Task<CreateDTO> CreateAsync(CreateDTO dto);
        Task<List<ListDTO>> GetAllAsync();
        Task<IDTO> GetByIdAsync<IDTO>(int id);
        Task<UpdateDTO> UpdateAsync(UpdateDTO dto);
        Task<bool> RemoveAsync(int id);
    }
}

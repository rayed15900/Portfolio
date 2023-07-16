using AutoMapper;
using FluentValidation;
using Portfolio.BusinessLogic.DTOs.Interfaces;
using Portfolio.BusinessLogic.Extensions;
using Portfolio.BusinessLogic.Interfaces.Base;
using Portfolio.DataAccess.UnitOfWork;
using Portfolio.Models.Base;
using Portfolio.Utility;
using System.Security.Principal;

namespace Portfolio.BusinessLogic.Services.Base
{
    public class Service<CreateDTO, UpdateDTO, ListDTO, T> : IService<CreateDTO, UpdateDTO, ListDTO, T>
		where CreateDTO : class, IDTO, new()
		where UpdateDTO : class, IUpdateDTO, new()
		where ListDTO : class, IDTO, new()
		where T : BaseModel
	{
		private readonly IMapper _mapper;
		private readonly IUOW _uow;
		private readonly IValidator<CreateDTO> _createDtoValidator;
		private readonly IValidator<UpdateDTO> _updateDtoValidator;

		public Service(IMapper mapper, IUOW uow, IValidator<CreateDTO> createDtoValidator, IValidator<UpdateDTO> updateDtoValidator)
		{
			_mapper = mapper;
			_uow = uow;
			_createDtoValidator = createDtoValidator;
			_updateDtoValidator = updateDtoValidator;
		}

		public async Task<IResponse<CreateDTO>> CreateAsync(CreateDTO dto)
		{
			var result = _createDtoValidator.Validate(dto);
			if (result.IsValid)
			{
				var createdEntity = _mapper.Map<T>(dto);
				await _uow.GetRepository<T>().CreateAsync(createdEntity);
				await _uow.SaveChangesAsync();
				return new Response<CreateDTO>(ResponseType.Success, dto);
			}
			return new Response<CreateDTO>(dto, result.ConvertToCustomValidationError());
		}

		public async Task<IResponse<List<ListDTO>>> GetAllAsync()
		{
			var data = await _uow.GetRepository<T>().GetAllAsync();
			var dto = _mapper.Map<List<ListDTO>>(data);
			return new Response<List<ListDTO>>(ResponseType.Success, dto);
		}

		public async Task<IResponse<IDTO>> GetByIdAsync<IDTO>(int id)
		{
			var data = await _uow.GetRepository<T>().FindAsync(id);
			if (data == null)
				return new Response<IDTO>(ResponseType.NotFound, $"Id: {id} is not found");
			var dto = _mapper.Map<IDTO>(data);
			return new Response<IDTO>(ResponseType.Success, dto);
		}

		public async Task<IResponse<UpdateDTO>> UpdateAsync(UpdateDTO dto)
		{
			var result = _updateDtoValidator.Validate(dto);
			if (result.IsValid)
			{
				var oldEntity = await _uow.GetRepository<T>().FindAsync(dto.Id);
				if (oldEntity == null)
					return new Response<UpdateDTO>(ResponseType.NotFound, $"Id: {dto.Id} is not found");
				var entity = _mapper.Map<T>(dto);
				_uow.GetRepository<T>().Update(entity, oldEntity);
				await _uow.SaveChangesAsync();
				return new Response<UpdateDTO>(ResponseType.Success, dto);
			}
			return new Response<UpdateDTO>(dto, result.ConvertToCustomValidationError());
		}

        public async Task<IResponse> RemoveAsync(int id)
        {
            var data = await _uow.GetRepository<T>().FindAsync(id);
            if (data == null)
                return new Response(ResponseType.NotFound, $"Id: {id} is not found");
            _uow.GetRepository<T>().Remove(data);
            await _uow.SaveChangesAsync();
            return new Response(ResponseType.Success);
        }
    }
}

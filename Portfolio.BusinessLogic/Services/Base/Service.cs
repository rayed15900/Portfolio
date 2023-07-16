﻿using AutoMapper;
using FluentValidation;
using Portfolio.BusinessLogic.DTOs.Interfaces;
using Portfolio.BusinessLogic.Interfaces.Base;
using Portfolio.DataAccess.UnitOfWork;
using Portfolio.Models.Base;

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

        public async Task<CreateDTO> CreateAsync(CreateDTO dto)
        {
            var result = _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var createdEntity = _mapper.Map<T>(dto);
                await _uow.GetRepository<T>().CreateAsync(createdEntity);
                await _uow.SaveChangesAsync();
                return _mapper.Map<CreateDTO>(createdEntity);
            }
            return null;
        }

        public async Task<List<ListDTO>> GetAllAsync()
        {
            var data = await _uow.GetRepository<T>().GetAllAsync();
            var dto = _mapper.Map<List<ListDTO>>(data);
            return dto;
        }

        public async Task<IDTO> GetByIdAsync<IDTO>(int id)
        {
            var data = await _uow.GetRepository<T>().GetByIdAsync(id);
            var dto = _mapper.Map<IDTO>(data);
            return dto;
        }

        public async Task<UpdateDTO> UpdateAsync(UpdateDTO dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var oldEntity = await _uow.GetRepository<T>().GetByIdAsync(dto.Id);
                var entity = _mapper.Map<T>(dto);
                _uow.GetRepository<T>().Update(entity, oldEntity);
                await _uow.SaveChangesAsync();
                return dto;
            }
            return null;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var data = await _uow.GetRepository<T>().GetByIdAsync(id);
            if(data == null)
                return false;
            _uow.GetRepository<T>().Remove(data);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}

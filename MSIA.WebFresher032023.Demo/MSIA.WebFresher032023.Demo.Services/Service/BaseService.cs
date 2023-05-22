using AutoMapper;
using MSIA.WebFresher032023.Demo.BL_Services.DTO.Empl;
using MSIA.WebFresher032023.Demo.DL_Repositories.Repositories;

namespace MSIA.WebFresher032023.Demo.BL_Services.Service
{
    public class BaseService<TEntity, TEntityDto, TEntityUpdateDto, TEntityCreateDto> : IBaseService<TEntity, TEntityDto, TEntityUpdateDto, TEntityCreateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            if(entity == null)
            {
                throw new Exception("Không tìm thấy bản ghi");
            }
            var entityDto = _mapper.Map<TEntityDto>(entity);
            return entityDto;
        }

        public virtual async Task<List<TEntityDto>> GetListAsync(int pageNumber, int pageLimit, string filterName)
        {
            var entities = await _baseRepository.GetListAsync(pageNumber, pageLimit, filterName);
            if(entities.Count() <= 0)
            {
                throw new Exception("Không tìm thấy danh sách bản ghi");
            }
            List<TEntityDto> entityDtos = new List<TEntityDto>();
            foreach (var entity in entities)
            {
                entityDtos.Add(_mapper.Map<TEntityDto>(entity));
            }
            return entityDtos;
        }

        public virtual async Task<bool> PostAsync(TEntityCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> PutAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            if(entity == null)
            {
                throw new Exception("Không tìm thấy bản ghi");
            }
            var result = await _baseRepository.DeleteAsync(id);
            return result;
        }
    }
}

using MSIA.WebFresher032023.Demo.BL_Services.DTO.Empl;
namespace MSIA.WebFresher032023.Demo.BL_Services.Service
{
    public interface IBaseService<TEntity, TEntityDto, TEntityUpdateDto, TEntityCreateDto>
    {
        Task<TEntityDto> GetAsync(Guid id);
        Task<bool> PostAsync(TEntityCreateDto entityCreateDto);
        Task<bool> PutAsync(Guid id, TEntityUpdateDto entityUpdateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<List<TEntityDto>> GetListAsync(int pageNumber, int pageLimit, string filterName);
    }
}

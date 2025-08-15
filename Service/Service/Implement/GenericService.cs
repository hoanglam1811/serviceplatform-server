using AutoMapper;
using Repository.Repository.Interface;
using Service.Service.Interface;

namespace Service.Service.Implement;
public class GenericService<T, CT, UT, VT> : IGenericService<T, CT, UT, VT> where T : class where CT : class where UT : class where VT : class
{
    protected readonly IGenericRepository<T> _genericRepository;
    protected readonly IMapper _mapper;

    public GenericService(IGenericRepository<T> genericRepository,
        IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }

    public async Task<VT> AddAsync(CT dto)
    {
        var entity = _mapper.Map<T>(dto);
        var result = await _genericRepository.AddAsync(entity);
        return _mapper.Map<VT>(result);
    }

    public async Task<VT?> DeleteAsync(params object[] keys)
    {
        var entity = await _genericRepository.GetByIdAsync(keys);
        if(entity == null) return null;
        await _genericRepository.DeleteAsync(entity);
        return _mapper.Map<VT>(entity);
    }

    public async Task<IEnumerable<VT>> GetAllAsync()
    {
        var result = await _genericRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<VT>>(result);
    }

    public async Task<VT?> GetByIdAsync(params object[] keys)
    {
        var result = await _genericRepository.GetByIdAsync(keys);
        if (result == null) return null;
        return _mapper.Map<VT>(result);
    }

    public async Task<VT> UpdateAsync(UT entity)
    {
        var result = await _genericRepository.UpdateAsync(_mapper.Map<T>(entity));
        return _mapper.Map<VT>(result);
    }
}

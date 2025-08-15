namespace Service.Service.Interface;

public interface IGenericService<T, CT, UT, VT>
{
    Task<IEnumerable<VT>> GetAllAsync();
    Task<VT?> GetByIdAsync(params object[] keys);
    Task<VT> AddAsync(CT entity);
    Task<VT> UpdateAsync(UT entity);
    Task<VT?> DeleteAsync(params object[] keys);
}

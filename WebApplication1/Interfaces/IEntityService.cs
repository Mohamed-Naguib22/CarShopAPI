using CarShopAPI.Models;

namespace CarShopAPI.Interfaces
{
    public interface IEntityService<T>
    {
        Task<int> GetIdByNameAsync(string name);
        Task<List<string>> GetAllAsync();
    }
}
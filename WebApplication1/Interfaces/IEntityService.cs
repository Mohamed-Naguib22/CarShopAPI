using CarShopAPI.Models;

namespace CarShopAPI.Interfaces
{
    public interface IEntityService<T> where T : class
    {
        Task<int> GetIdByNameAsync(string name);
        Task<List<string>> GetAllAsync();
    }
}
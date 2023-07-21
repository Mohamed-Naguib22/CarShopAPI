using CarShopAPI.Models;

namespace CarShopAPI.Interfaces
{
    public interface IEntityService<T>
    {
        void GetId(Car car);
        List<string> GetAll();
    }
}
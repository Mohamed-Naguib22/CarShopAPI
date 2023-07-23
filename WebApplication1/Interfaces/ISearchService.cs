using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Interfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<CarDto>> Filter(CarFilter filter, int pageNumber);
    }
}

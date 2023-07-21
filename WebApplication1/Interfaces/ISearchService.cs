using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Interfaces
{
    public interface ISearchService
    {
        Task<object> Filter(CarFilter filter);
    }
}

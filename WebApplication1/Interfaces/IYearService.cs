using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Interfaces
{
    public interface IYearService
    {
        IQueryable<int> GetYears();
    }
}

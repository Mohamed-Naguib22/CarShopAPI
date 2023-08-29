using CarShopAPI.Data;
using CarShopAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Services
{
    public class YearService : BaseService, IYearService
    {
        public YearService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public IQueryable<int> GetYears()
        {
            var years =  _dbContext.Cars.Select(x => x.Year).Distinct();
            return years;
        }
    }
}

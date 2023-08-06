using CarShopAPI.Data;
using CarShopAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Services
{
    public class YearService : IYearService
    {
        private readonly ApplicationDbContext _dbContext;
        public YearService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<int> GetYears()
        {
            var years =  _dbContext.Cars.Select(x => x.Year).Distinct();
            return years;
        }
    }
}

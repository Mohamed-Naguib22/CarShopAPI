using CarShopAPI.Data;
using CarShopAPI.Extensions;
using CarShopAPI.Helpers;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Implementation
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext _dbContext;
        public SearchService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Filter(CarFilter filter = null)
        {
            var query = _dbContext.Cars
            .Include(c => c.BodyType)
            .Include(c => c.Manufacturer)
            .Include(c => c.State)
            .Select(car => new
            {
                car.CarId,
                car.Model,
                car.Description,
                car.Year,
                car.Price,
                car.IsNew,
                car.SellerId,
                BodyType = car.BodyType.Name,
                State = car.State.Name,
                Manufacturer = car.Manufacturer.Name
            });

            if (filter != null)
            {
                if (filter.Year.HasValue)
                {
                    query = query.Where(car => car.Year == filter.Year.Value);
                }

                if (!string.IsNullOrEmpty(filter.Model))
                {
                    query = query.Where(car => car.Model.ToLower() == filter.Model.ToLower());
                }

                if (!string.IsNullOrEmpty(filter.BodyType))
                {
                    query = query.Where(car => car.BodyType.ToLower() == filter.BodyType.ToLower());
                }

                if (!string.IsNullOrEmpty(filter.State))
                {
                    query = query.Where(car => car.State.ToLower() == filter.State.ToLower());
                }

                if (!string.IsNullOrEmpty(filter.Manufacturer))
                {
                    query = query.Where(car => car.Manufacturer.ToLower() == filter.Manufacturer.ToLower());
                }

                if (filter.IsNew.HasValue)
                {
                    query = query.Where(car => car.IsNew == filter.IsNew);
                }

                if (filter.Price.HasValue)
                {
                    query = query.Where(car => car.Price <= filter.Price);
                }
            }
            var filteredCars = await query.ToListAsync();
            return filteredCars;
        }
    }
}

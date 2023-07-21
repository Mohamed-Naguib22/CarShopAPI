using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Implementation.Interfaces
{
    public interface IImageService<T> where T : class
    {
        public void SetImage(T model);
        public void UpdateImage(T model);
        public void DeleteImage(T model);
    }
}

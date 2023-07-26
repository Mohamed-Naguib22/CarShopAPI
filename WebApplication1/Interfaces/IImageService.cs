﻿using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Implementation.Interfaces
{
    public interface IImageService<T> where T : class
    {
        public string GetImageUrl(T model);
        public void SetImage(T model, IFormFile? imgFile);
        public void DeleteImage(T model);
    }
}

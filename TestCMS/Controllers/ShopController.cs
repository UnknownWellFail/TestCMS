using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TestCMS.Models;
using TestCMS.Services;

namespace TestCMS.Controllers
{
    public class ShopController : Controller
    {
        private ShopService shopService;

        public ShopController(IConfiguration configuration, ShopService service)
        {
            shopService = service;
        }

        //Get all favorites for user
        public string Favorites(int id)
        {
            return JsonConvert.SerializeObject(shopService.GetFavorites(id).GetAwaiter().GetResult());
        }

        //Get all shops
        public string AllShops()
        {
            return JsonConvert.SerializeObject(shopService.GetShops().GetAwaiter().GetResult());
        }


        //Get shop by category
        public string AllShopCategory(string category)
        {
            return JsonConvert.SerializeObject(shopService.GetShopCategory(category).GetAwaiter().GetResult());
        }

        //Get shop by location
        public string AllShopPlace(double x, double y)
        {
            return JsonConvert.SerializeObject(shopService.GetShopPlace(x, y).GetAwaiter().GetResult());
        }

        //Update shop
        public string PutShop([FromBody] Shop shop)
        {
            if (ModelState.IsValid)
            {
                shopService.UpdateShop(shop);
                return "Successful";
            }

            return "Error";
        }

        //Delete shop
        public string DeleteShop(int id)
        {
            shopService.RemoveShop(id);
            return "Successful";
        }


        //Add shop
        public string PostShop([FromBody] Shop shop)
        {
            if (ModelState.IsValid)
            {
                shopService.AddShop(shop);
                return "Successful";
            }

            return "Error";
        }

        //Add favorites for user
        public string PostFavorite([FromBody] KeyValuePair<int, int> favorites)
        {
            return shopService.AddFavorite(favorites.Key, favorites.Value);
        }
    }
}